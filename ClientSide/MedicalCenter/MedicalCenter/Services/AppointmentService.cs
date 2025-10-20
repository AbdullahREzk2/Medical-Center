using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using MedicalCenter.Models;

namespace MedicalCenter.Services
{
    public class AppointmentService
    {
        private readonly HttpClient _httpClient;

        public AppointmentService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://hopemedicalcenter.runasp.net/api/Appointment/")
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

        // Add New appointment
        public async Task<appointmentOutputModel> addNewAppointment(appointmentInputModel appointmentInput)
        {
            SetAuthorizationHeader(); // Update token before request
            var response = await _httpClient.PostAsJsonAsync("Add", appointmentInput);
            response.EnsureSuccessStatusCode();
            var appointment = await response.Content.ReadFromJsonAsync<appointmentOutputModel>();
            return appointment!;
        }

        // GetAllAppointments
        public async Task<IEnumerable<appointmentOutputModel>> Getallappointments()
        {
            SetAuthorizationHeader(); // Update token before request
            var response = await _httpClient.GetAsync("GetAll");
            response.EnsureSuccessStatusCode();
            var appointments = await response.Content.ReadFromJsonAsync<IEnumerable<appointmentOutputModel>>();
            return appointments!;
        }

        // GetAppointmentById
        public async Task<appointmentOutputModel> getAppointmentById(int Id)
        {
            SetAuthorizationHeader(); // Update token before request
            var response = await _httpClient.GetAsync($"getBy/{Id}");
            response.EnsureSuccessStatusCode();
            var appointment = await response.Content.ReadFromJsonAsync<appointmentOutputModel>();
            return appointment!;
        }

        // GetAppointmentsByDoctorId
        public async Task<IEnumerable<appointmentOutputModel>> getDoctorAppointments(int doctorId)
        {
            SetAuthorizationHeader(); // Update token before request
            var response = await _httpClient.GetAsync($"getDoctorAppointments/{doctorId}");
            response.EnsureSuccessStatusCode();
            var appointments = await response.Content.ReadFromJsonAsync<IEnumerable<appointmentOutputModel>>();
            return appointments!;
        }

        // GetAppointmentsByPatientId
        public async Task<IEnumerable<appointmentOutputModel>> getPateintAppointments(int patientId)
        {
            SetAuthorizationHeader(); // Update token before request
            var response = await _httpClient.GetAsync($"getPatientAppointments/{patientId}");
            response.EnsureSuccessStatusCode();
            var appointments = await response.Content.ReadFromJsonAsync<IEnumerable<appointmentOutputModel>>();
            return appointments!;
        }

        // DeleteAppointment
        public async Task<bool> deleteAppointment(int Id)
        {
            SetAuthorizationHeader(); // Update token before request
            var response = await _httpClient.DeleteAsync($"deleteAppointment/{Id}");
            return response.IsSuccessStatusCode;
        }

        // UpdateAppointmentStatus
        public async Task<bool> updateStatus(int appointmentId, string status)
        {
            SetAuthorizationHeader(); // Update token before request
            var response = await _httpClient.PutAsync($"updateStatus?appointmentId={appointmentId}&status={status}", null);
            return response.IsSuccessStatusCode;
        }
    }
}