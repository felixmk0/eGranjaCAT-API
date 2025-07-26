﻿using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using nastrafarmapi.DTOs.GTR.Guies;

namespace nastrafarmapi.Services
{
    public class GTRService
    {
        private readonly ILogger<GTRService> logger;
        private readonly HttpClient gtrClient;

        public GTRService(ILogger<GTRService> logger)
        {
            this.logger = logger;
            gtrClient = new HttpClient
            {
                BaseAddress = new Uri("https://preproduccio.aplicacions.agricultura.gencat.cat/gtr/WSMobilitat/AppJava/")
            };
        }

        public async Task<ServiceResult<LoadDSTGuidesResponseDTO>> LoadAndGetDSTGuides(LoadDSTGuidesRequestDTO requestDTO)
        {
            var resultObj = new ServiceResult<LoadDSTGuidesResponseDTO>();

            try
            {
                var response = await gtrClient.PostAsJsonAsync("WSCarregaGuiesMobilitat", requestDTO);

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadFromJsonAsync<LoadDSTGuidesResponseDTO>();

                    if (responseData != null && responseData.Guias != null)
                    {
                        resultObj.Success = true;
                        resultObj.Data = responseData;
                    }
                    else
                    {
                        resultObj.Success = false;
                        resultObj.Errors.Add("Resposta buida o invàlida del servei GTR");
                    }
                }
                else
                {
                    resultObj.Success = false;
                    resultObj.Errors.Add($"Error HTTP: {response.StatusCode} - {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al cridar el servei GTR WSCarregaGuiesMobilitat");
                resultObj.Success = false;
                resultObj.Errors.Add("Error inesperat en cridar el servei GTR");
            }

            return resultObj;
        }


        public async Task<ServiceResult<bool>> UpdateDSTGuides(UpdateDSTGuidesRequest requestDTO)
        {
            var resultObj = new ServiceResult<bool>();

            try
            {
                var response = await gtrClient.PostAsJsonAsync("WSModificarGuiasMovilitat", requestDTO);

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadFromJsonAsync<GTRSuccessResponseDTO>();

                    if (responseData?.Codi == "OK")
                    {
                        resultObj.Success = true;
                    }
                    else
                    {
                        resultObj.Success = false;
                        resultObj.Errors.Add($"Resposta del servei GTR: {responseData?.Codi} - {responseData?.Descripcio ?? "Descripció buida"}");
                    }
                }
                else
                {
                    resultObj.Success = false;
                    resultObj.Errors.Add($"Error HTTP: {response.StatusCode} - {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al cridar el servei GTR WSModificarGuiasMovilitat");
                resultObj.Success = false;
                resultObj.Errors.Add("Error inesperat en cridar el servei GTR");
            }

            return resultObj;
        }
    }
}
