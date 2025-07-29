using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using nastrafarmapi.Data;
using nastrafarmapi.Entities;
using nastrafarmapi.Extensions.Cron;
using nastrafarmapi.Extensions.Cron.Jobs;
using nastrafarmapi.Interfaces;
using nastrafarmapi.Services;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(opts =>
    opts.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
}).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
{
    options.MapInboundClaims = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtKey"]!)),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,

        NameClaimType = ClaimTypes.Name,
        RoleClaimType = ClaimTypes.Role
    };
});


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Entrades", policy =>
        policy.RequireAssertion(context =>
            context.User.IsInRole("Admin") || context.User.HasClaim("Access", "Entrades")
        )
    );

    options.AddPolicy("Lots", policy =>
        policy.RequireAssertion(context =>
            context.User.IsInRole("Admin") || context.User.HasClaim("Access", "Lots")
        )
    );

    options.AddPolicy("Farms", policy =>
        policy.RequireAssertion(context =>
            context.User.IsInRole("Admin") || context.User.HasClaim("Access", "Farms")
        )
    );
});


builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<IFarmService, FarmService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<ILotService, LotService>();
builder.Services.AddTransient<IEntradaService, EntradaService>();
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddTransient<IExcelService, ExcelService>();
builder.Services.AddControllers();

builder.Services.AddScoped<IBackupService, BackupService>();
builder.Services.AddCronJob<BackupJob>(options =>
{
    options.TimeZone = TimeZoneInfo.Local;
    options.CronExpression = builder.Configuration["CronExpressions:BackupJob"]!;
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "NastrafarmSIGE - RESTful API",
        Version = "v1.0",
        Description = "Documentació oficial de la API per la gestio d'explotacions porcines catalanes.",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = " - Felix Montragull Kruse",
            Email = "fmontrakruse@gmail.com"
        }
    });

    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "NastrafarmSIGE - RESTful API v1.0");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();


app.Run();