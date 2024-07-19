using System.Net.Http;
using System.Net.Http.Json;
using BusinessObjects.Models;
using DTOs.Response.Booking;
using DTOs.Response.Customer;
using DTOs.Response.Doctor;
using DTOs.Response.Pet;
using DTOs.Response.Schedule;
using DTOs.Response.Service;
using DTOs.Response.Staff;
using DTOs.Response.Transaction;
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

		public async Task<List<DoctorResponse>> GetDoctorAsync()
		{
			var url = $"{_settings.BaseUrl}{_settings.DoctorEndpoint}";
			return await _httpClient.GetFromJsonAsync<List<DoctorResponse>>(url);
		}

		public async Task<DoctorResponse> GetDoctorByIdAsync(int id)
		{
			var url = $"{_settings.BaseUrl}{_settings.DoctorEndpoint}/{id}";
			return await _httpClient.GetFromJsonAsync<DoctorResponse>(url);
		}

		public async Task<List<ScheduleResponse>> GetScheduleAsync()
		{
			var url = $"{_settings.BaseUrl}{_settings.ScheduleEndpoint}";
			return await _httpClient.GetFromJsonAsync<List<ScheduleResponse>>(url);
		}

		public async Task<ScheduleResponse> GetScheduleByIdAsync(int id)
		{
			var url = $"{_settings.BaseUrl}{_settings.ScheduleEndpoint}/{id}";
			return await _httpClient.GetFromJsonAsync<ScheduleResponse>(url);
		}

		public async Task<List<StaffResponse>> GetStaffAsync()
		{
			var url = $"{_settings.BaseUrl}{_settings.StaffEndpoint}";
			return await _httpClient.GetFromJsonAsync<List<StaffResponse>>(url);
		}

		public async Task<StaffResponse> GetStaffByIdAsync(int id)
		{
			var url = $"{_settings.BaseUrl}{_settings.StaffEndpoint}/{id}";
			return await _httpClient.GetFromJsonAsync<StaffResponse>(url);
		}

		public async Task<List<TransactionResponse>> GetTransactionsAsync()
		{
			var url = $"{_settings.BaseUrl}{_settings.TransactionEndpoint}";
			return await _httpClient.GetFromJsonAsync<List<TransactionResponse>>(url);
		}

		public async Task<TransactionResponse> GetTransactionByIdAsync(int id)
		{
			var url = $"{_settings.BaseUrl}{_settings.TransactionEndpoint}/{id}";
			return await _httpClient.GetFromJsonAsync<TransactionResponse>(url);
		}

		public async Task<List<BookingResponse>> GetBookingsAsync()
		{
			var url = $"{_settings.BaseUrl}{_settings.BookingEndpoint}";
			return await _httpClient.GetFromJsonAsync<List<BookingResponse>>(url);
		}

		public async Task<BookingResponse> GetBookingByIdAsync(int id)
		{
			var url = $"{_settings.BaseUrl}{_settings.BookingEndpoint}/{id}";
			return await _httpClient.GetFromJsonAsync<BookingResponse>(url);
		}

		public async Task<List<ServiceResponse>> GetServicesAsync()
		{
			var url = $"{_settings.BaseUrl}{_settings.ServiceEndpoint}";
			return await _httpClient.GetFromJsonAsync<List<ServiceResponse>>(url);
		}

		public async Task<ServiceResponse> GetServiceByIdAsync(int id)
		{
			var url = $"{_settings.BaseUrl}{_settings.ServiceEndpoint}/{id}";
			return await _httpClient.GetFromJsonAsync<ServiceResponse>(url);
		}

		public async Task<List<CustomerResponse>> GetCustomersAsync()
		{
			var url = $"{_settings.BaseUrl}{_settings.CustomerEndpoint}";
			return await _httpClient.GetFromJsonAsync<List<CustomerResponse>>(url);
		}

		public async Task<CustomerResponse> GetCustomerByIdAsync(int id)
		{
			var url = $"{_settings.BaseUrl}{_settings.CustomerEndpoint}/{id}";
			return await _httpClient.GetFromJsonAsync<CustomerResponse>(url);
		}
	}
}
