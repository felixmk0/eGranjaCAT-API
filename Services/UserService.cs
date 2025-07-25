using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using nastrafarmapi.DTOs;
using nastrafarmapi.DTOs.Users;
using nastrafarmapi.Entities;
using nastrafarmapi.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace nastrafarmapi.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ILogger<UserService> logger;
        private readonly IConfiguration configuration;
        private readonly IMapper mapper;
        private readonly IEmailService emailService;

        public UserService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, ILogger<UserService> logger, IConfiguration configuration, IMapper mapper, IEmailService emailService)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.logger = logger;
            this.configuration = configuration;
            this.mapper = mapper;
            this.emailService = emailService;
        }

        public async Task<ServiceResult<AuthResponseDTO>> CreateUserAsync(CreateUserDTO userDTO)
        {
            var resultObj = new ServiceResult<AuthResponseDTO>();

            var user = mapper.Map<User>(userDTO);
            var result = await userManager.CreateAsync(user, userDTO.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    logger.LogError($"Error: {error.Description}");
                    resultObj.Errors.Add(error.Description);
                }
                resultObj.Success = false;
                return resultObj;
            }

            var roleName = userDTO.Role.ToString();
            var roleExists = await roleManager.RoleExistsAsync(roleName);

            if (!roleExists)
            {
                var roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
                if (!roleResult.Succeeded)
                {
                    foreach (var error in roleResult.Errors)
                    {
                        logger.LogError($"Error creando el rol: {error.Description}");
                        resultObj.Errors.Add(error.Description);
                    }
                    resultObj.Success = false;
                    return resultObj;
                }
            }

            var addToRoleResult = await userManager.AddToRoleAsync(user, roleName);
            if (!addToRoleResult.Succeeded)
            {
                foreach (var error in addToRoleResult.Errors)
                {
                    logger.LogError($"Error asignando rol: {error.Description}");
                    resultObj.Errors.Add(error.Description);
                }
                resultObj.Success = false;
                return resultObj;
            }

            var variables = new Dictionary<string, string>
            {
                { "Name", userDTO.Name },
                { "Email", userDTO.Email },
                { "Role", userDTO.Role.ToString() },
                { "RegistrationDate", DateTime.Now.ToString("dd/MM/yyyy") }
            };

            var emailResult = await emailService.SendEmailAsync(userDTO.Email, "Benvingut a la nostra plataforma!", "Benvinguda.html", variables);
            if (!emailResult.Success) logger.LogError("Error sending welcome email: " + string.Join(", ", emailResult.Errors));

            var tokenDTO = mapper.Map<TokenUserDTO>(user);

            var token = await BuildJwtToken(tokenDTO);
            resultObj.Data = token;
            resultObj.Success = true;
            return resultObj;
        }


        public async Task<ServiceResult<AuthResponseDTO>> LoginUserAsync(LoginUserDTO loginDTO)
        {
            var resultObj = new ServiceResult<AuthResponseDTO>();
            var user = await userManager.FindByEmailAsync(loginDTO.Email);
            if (user == null)
            {
                logger.LogError("User not found.");
                resultObj.Errors.Add("Invalid email or password.");
                resultObj.Success = false;
                return resultObj;
            }

            var passwordCheck = await userManager.CheckPasswordAsync(user, loginDTO.Password);
            if (!passwordCheck)
            {
                logger.LogError("Invalid password.");
                resultObj.Errors.Add("Invalid email or password.");
                resultObj.Success = false;
                return resultObj;
            }

            var tokenDTO = mapper.Map<TokenUserDTO>(user);
            var roles = await userManager.GetRolesAsync(user);
            tokenDTO.Role = roles.Any() && Enum.TryParse<UserRole>(roles.First(), out var role) ? role.ToString() : UserRole.User.ToString();

            var token = await BuildJwtToken(tokenDTO);
            resultObj.Data = token;
            resultObj.Success = true;
            return resultObj;
        }


        public async Task<ServiceResult<bool>> DeleteUserById(int id)
        {
            var resultObj = new ServiceResult<bool>();
            var user = await userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                logger.LogError($"User with ID {id} not found.");
                resultObj.Errors.Add($"User with ID {id} not found.");
                resultObj.Success = false;
                return resultObj;
            }
            var deleteResult = await userManager.DeleteAsync(user);
            if (!deleteResult.Succeeded)
            {
                foreach (var error in deleteResult.Errors)
                {
                    logger.LogError($"Error deleting user: {error.Description}");
                    resultObj.Errors.Add(error.Description);
                }
                resultObj.Success = false;
                return resultObj;
            }
            resultObj.Data = true;
            resultObj.Success = true;
            return resultObj;
        }

        public async Task<ServiceResult<List<GetUserDTO>>> GetUsersAsync()
        {
            var resultObj = new ServiceResult<List<GetUserDTO>>();
            var users = await userManager.Users.ToListAsync();
            var userDTOs = mapper.Map<List<GetUserDTO>>(users);

            if (userDTOs == null || !userDTOs.Any())
            {
                logger.LogInformation("No users found.");
                resultObj.Data = new List<GetUserDTO>();
                resultObj.Success = true;
                return resultObj;
            }

            resultObj.Data = userDTOs;
            resultObj.Success = true;

            return resultObj;
        }

        public async Task<ServiceResult<GetUserDTO?>> GetUserByIdAsync(int id)
        {
            var resultObj = new ServiceResult<GetUserDTO?>();
            var user = await userManager.FindByIdAsync(id.ToString());
            var userDTO = mapper.Map<GetUserDTO>(user);

            if (userDTO == null)
            {
                logger.LogError($"User with ID {id} not found.");
                resultObj.Errors.Add($"User with ID {id} not found.");
                resultObj.Success = false;
                return resultObj;
            }

            resultObj.Data = userDTO;
            resultObj.Success = true;
            return resultObj;
        }


        private async Task<AuthResponseDTO> BuildJwtToken(TokenUserDTO userDTO)
        {
            var claims = new List<Claim>
            {
                new Claim("id", userDTO.Id.ToString()),
                new Claim(ClaimTypes.Name, userDTO.Email),
                new Claim(ClaimTypes.Role, userDTO.Role.ToString())
            };

            var user = await userManager.FindByEmailAsync(userDTO.Email);
            var claimsDb = await userManager.GetClaimsAsync(user!);

            claims.AddRange(claimsDb);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtKey"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddDays(1);

            var secToken = new JwtSecurityToken(claims: claims, expires: expires, signingCredentials: creds);

            var token = new JwtSecurityTokenHandler().WriteToken(secToken);
            return new AuthResponseDTO { Token = token, Expiraton = expires, };
        }
    }
}