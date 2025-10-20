using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using MedicalCenter.Models;

namespace MedicalCenter.Services
{
    public class DoctorService
    {
        private readonly HttpClient _httpClient;

        public DoctorService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://hopemedicalcenter.runasp.net/api/Doctor/")
            };
        }

        // CRITICAL FIX: This method updates the authorization header with the current token
        private void SetAuthorizationHeader()
        {
            if (!string.IsNullOrEmpty(SessionManager.AccessToken))
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", SessionManager.AccessToken);
            }
            else
            {
                _httpClient.DefaultRequestHeaders.Authorization = null;
            }
        }

        // Get All Doctors 
        public async Task<IEnumerable<DoctorOutputModel>> GetAllDoctors()
        {
            SetAuthorizationHeader(); // Update token before request
            var response = await _httpClient.GetAsync("GetAllDoctors");
            response.EnsureSuccessStatusCode();
            var doctors = await response.Content.ReadFromJsonAsync<IEnumerable<DoctorOutputModel>>();
            return doctors!;
        }

        // Add New Doctor 
        public async Task<DoctorOutputModel> AddNewDoctor(DoctorInputModel doctorInput)
        {
            SetAuthorizationHeader(); // Update token before request
            var response = await _httpClient.PostAsJsonAsync("addNewDoctor", doctorInput);
            if (response.IsSuccessStatusCode)
            {
                var doctors = await response.Content.ReadFromJsonAsync<DoctorOutputModel>();
                return doctors!;
            }
            else
            {
                var Error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error respond with error {response.StatusCode} : {Error}");
            }
        }

        // Delete a doctor
        public async Task<bool> DeleteDoctor(int Id)
        {
            SetAuthorizationHeader(); // Update token before request
            var response = await _httpClient.DeleteAsync($"DeleteDoctor/{Id}");
            return response.IsSuccessStatusCode;
        }

        // Search for a doctor
        public async Task<IEnumerable<DoctorOutputModel>> Search(string Keyword)
        {
            SetAuthorizationHeader(); // Update token before request
            var response = await _httpClient.GetAsync($"SearchForDoctor?Keyword={Keyword}");
            response.EnsureSuccessStatusCode();
            var doctors = await response.Content.ReadFromJsonAsync<IEnumerable<DoctorOutputModel>>();
            return doctors!;
        }
    }
}