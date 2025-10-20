using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using MedicalCenter.Models;
using Microsoft.VisualBasic.ApplicationServices;

namespace MedicalCenter.Services
{
    public class PatientsService
    {
        private readonly HttpClient _httpClient;

        public PatientsService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://hopemedicalcenter.runasp.net/api/Pateints/")
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

        // Get all patients
        public async Task<List<PatientOutputModel>> GetAllPateints()
        {
            SetAuthorizationHeader(); // Update token before request
            var response = await _httpClient.GetAsync("GetAllPatients");
            response.EnsureSuccessStatusCode();
            var pateints = await response.Content.ReadFromJsonAsync<List<PatientOutputModel>>();
            return pateints!;
        }

        // Add new patient
        public async Task<PatientOutputModel> AddNewPateint(PatientInputModel patientInput)
        {
            SetAuthorizationHeader(); // Update token before request
            var response = await _httpClient.PostAsJsonAsync("AddNewPateint", patientInput);
            if (response.IsSuccessStatusCode)
            {
                var Pateint = await response.Content.ReadFromJsonAsync<PatientOutputModel>();
                return Pateint!;
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Server responded with {response.StatusCode}: {errorContent}");
            }
        }

        // Delete patient
        public async Task<bool> DeletePateintById(int Id)
        {
            SetAuthorizationHeader(); // Update token before request
            var response = await _httpClient.DeleteAsync($"DeleteBy/{Id}");
            return response.IsSuccessStatusCode;
        }

        // Search
        public async Task<List<PatientOutputModel>> Search(string Keyword)
        {
            SetAuthorizationHeader(); // Update token before request
            var response = await _httpClient.GetAsync($"SearchPateints?keyword={Keyword}");
            response.EnsureSuccessStatusCode();
            var Patient = await response.Content.ReadFromJsonAsync<List<PatientOutputModel>>();
            return Patient!;
        }
    }
}