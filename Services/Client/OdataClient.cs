using System.Net.Http;
using System.Net.Http.Json;
using BusinessObjects.Models;
using DTOs.Response.Pet;
using Microsoft.Extensions.Options;
using Services.Client;

namespace Presentation.Client
{
	public class OdataClient
	{
		private readonly HttpClient _httpClient;
		private readonly OdataSettings _settings;

		public OdataClient(HttpClient httpClient, IOptions<OdataSettings> settings)
		{
			_httpClient = httpClient;
			_settings = settings.Value;
		}

		public async Task<List<PetResponse>> GetPetsAsync()
		{
			var url = $"{_settings.BaseUrl}{_settings.PetEndpoint}";
			return await _httpClient.GetFromJsonAsync<List<PetResponse>>(url);
		}

		public async Task<PetResponse> GetPetByIdAsync(int id)
		{
			var url = $"{_settings.BaseUrl}{_settings.PetEndpoint}/{id}";
			return await _httpClient.GetFromJsonAsync<PetResponse>(url);
		}

		public async Task<List<Doctor>> GetDoctorAsync()
		{
			var url = $"{_settings.BaseUrl}{_settings.DoctorEndpoint}";
			return await _httpClient.GetFromJsonAsync<List<Doctor>>(url);
		}

		public async Task<Doctor> GetDoctorByIdAsync(int id)
		{
			var url = $"{_settings.BaseUrl}{_settings.DoctorEndpoint}/{id}";
			return await _httpClient.GetFromJsonAsync<Doctor>(url);
		}
	}
}
