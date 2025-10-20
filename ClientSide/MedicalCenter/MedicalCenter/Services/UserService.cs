using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using MedicalCenter.Models;

namespace MedicalCenter.Services
{
    public class UserService
    {
        private readonly HttpClient _httpClient;

        public UserService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://hopemedicalcenter.runasp.net/api/User/")
            };
        }

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

        // Get User By Id
        public async Task<UserOutputModel> getUserById(int Id)
        {
            SetAuthorizationHeader(); // Update token before request
            var response = await _httpClient.GetAsync($"getUserBy/{Id}");
            response.EnsureSuccessStatusCode();

            var user = await response.Content.ReadFromJsonAsync<UserOutputModel>();
            return user!;
        }

        // Get all Users 
        public async Task<List<UserOutputModel>> GetAllUserAysnc()
        {
            SetAuthorizationHeader(); // Update token before request
            var response = await _httpClient.GetAsync("GetAllUsers");
            response.EnsureSuccessStatusCode();
            var Users = await response.Content.ReadFromJsonAsync<List<UserOutputModel>>();
            return Users!;
        }

        //Add New User
        public async Task<UserOutputModel> AddNewUser(UserInputModel userInput)
        {
            SetAuthorizationHeader(); // Update token before request
            var response = await _httpClient.PostAsJsonAsync("AddNewUser", userInput);
            if (response.IsSuccessStatusCode)
            {
                var user = await response.Content.ReadFromJsonAsync<UserOutputModel>();
                return user!;
            }
            else
            {
                // Read detailed error message from server (validation, etc.)
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Server responded with {response.StatusCode}: {errorContent}");
            }
        }

        //Delete User by ID
        public async Task<bool> DeleteUserById(int UserID)
        {
            SetAuthorizationHeader(); // Update token before request
            var response = await _httpClient.DeleteAsync($"DeleteBy/{UserID}");
            return response.IsSuccessStatusCode;
        }

        // Delete User By National Id
        public async Task<bool> DeleteUserByNationalID(string NationalID)
        {
            SetAuthorizationHeader(); // Update token before request
            var response = await _httpClient.DeleteAsync($"DeleteByNationalID?nationalID={NationalID}");
            return response.IsSuccessStatusCode;
        }

        //Search for User
        public async Task<List<UserOutputModel>> Search(string Keyword)
        {
            SetAuthorizationHeader(); // Update token before request
            var response = await _httpClient.GetAsync($"SearchForUser?Keyword={Keyword}");
            response.EnsureSuccessStatusCode();
            var Users = await response.Content.ReadFromJsonAsync<List<UserOutputModel>>();
            return Users!;
        }

        // DeActivate User
        public async Task<bool> DeActivateUser(string NationalId)
        {
            SetAuthorizationHeader(); // Update token before request
            var response = await _httpClient.PutAsync($"deActivateUser?NationalId={NationalId}", null);
            return response.IsSuccessStatusCode;
        }

        // Activate user
        public async Task<bool> ActivateUser(string NationalId)
        {
            SetAuthorizationHeader(); // Update token before request
            var response = await _httpClient.PutAsync($"ActivateUser?NationalId={NationalId}", null);
            return response.IsSuccessStatusCode;
        }
    }
}