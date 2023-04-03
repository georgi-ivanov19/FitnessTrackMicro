using FitnessTrackMicro.Models;
using System.Net.Http.Json;

namespace FitnessTrackMicro.Services.DashboardService
{
    public class DashboardService : IDashboardService
    {
        private readonly HttpClient _http;

        public DashboardService(HttpClient http){
            _http = http;
        }
        public async Task<DashboardResults> GetDashboardData(string userId, DateTime date)
        {
            var result = await _http.GetFromJsonAsync<DashboardResults>($"https://dashboard-api.salmonisland-f0d5c65e.northeurope.azurecontainerapps.io/api/Dashboard?userId={userId}&Date={date}");
            if (result == null)
                throw new Exception("No results found");
            return result;
        }
    }
}
