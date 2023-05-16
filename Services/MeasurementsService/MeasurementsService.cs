using Blazored.LocalStorage;
using FitnessTrackMicro.Pages;
using FitnessTrackMicro.Models;
using Microsoft.AspNetCore.Components;
using System.Diagnostics.Metrics;
using System.Net.Http.Json;

namespace FitnessTrackMicro.Services.MeasurementsService
{
    public class MeasurementsService : IMeasurementsService
    {
        private readonly HttpClient _http;
        private readonly NavigationManager _navManager;
        private ILocalStorageService _localStorage;

        public List<Measurement> Measurements { get; set; } = new List<Measurement>();
        public MeasurementsService(HttpClient http, NavigationManager navManager, ILocalStorageService localStorage)
        {
            _http = http;
            _navManager = navManager;
            _localStorage = localStorage;
        }

        public async Task GetMeasurements(string applicationUserId)
        {
            List<Measurement>? result;
            // var measurementsInLocalStorage = await _localStorage.ContainKeyAsync("Measurements");
            // if (measurementsInLocalStorage)
            // {
            //     result = await _localStorage.GetItemAsync<List<Measurement>>("Measurements");
            // }
            // else
            // {
            result = await _http.GetFromJsonAsync<List<Measurement>>($"https://measurements-api.salmonisland-f0d5c65e.northeurope.azurecontainerapps.io/api/Measurements?userId={applicationUserId}");
                // await _localStorage.SetItemAsync<List<Measurement>>("Measurements", result);
            // }

            if (result != null)
            {
                this.Measurements = result;
            }
        }

        public IEnumerable<Measurement> GetMeasurementsByType(string type)
        {
            return Measurements.Where(x => x.Type == type).OrderByDescending(x => x.Date);
        }

        public async Task<Measurement> GetSingleMeasurement(int id)
        {
            Measurement? result;
            //var measurementsInLocalStorage = await _localStorage.ContainKeyAsync("Measurements");
            //if (measurementsInLocalStorage)
            //{
            //    var measurements = await _localStorage.GetItemAsync<List<Measurement>>("Measurements");
            //    result = measurements.First(m => m.Id == id);
            //}
            //else
            //{
                result = await _http.GetFromJsonAsync<Measurement>($"https://measurements-api.salmonisland-f0d5c65e.northeurope.azurecontainerapps.io/api/Measurements/{id}");
            //}

            //if (result != null)
            //{
            //    return result;
            //}
            //throw new Exception("Measurement not found");
            return result;
        }

        public async Task CreateMeasurement(Measurement measurement)
        {
            //var result = await _http.PostAsJsonAsync("api/measurement", measurement);
            //
            //// TODO: null check
            //
            //await _localStorage.SetItemAsync("Measurements", Measurements);
            var result = await _http.PostAsJsonAsync("https://measurements-api.salmonisland-f0d5c65e.northeurope.azurecontainerapps.io/api/Measurements", measurement);
            var response = await result.Content.ReadFromJsonAsync<Measurement>();
            Measurements.Add(response);
            _navManager.NavigateTo("measurements");
        }

        public async Task UpdateMeasurement(Measurement measurement)
        {
            var result = await _http.PutAsJsonAsync($"https://measurements-api.salmonisland-f0d5c65e.northeurope.azurecontainerapps.io/api/Measurements/{measurement.Id}", measurement);
            var response = await result.Content.ReadFromJsonAsync<Measurement>();
            // TODO: null check
            int index = Measurements.FindIndex(m => m.Id == measurement.Id);
            if (index != -1)
            {
                Measurements[index] = measurement;
            //    await _localStorage.SetItemAsync("Measurements", Measurements);
            }
            _navManager.NavigateTo("measurements");
        }

        public async Task DeleteMeasurement(int id)
        {
            await _http.DeleteAsync($"https://measurements-api.salmonisland-f0d5c65e.northeurope.azurecontainerapps.io/api/Measurements/{id}");
            Measurements.RemoveAt(Measurements.FindIndex(m => m.Id == id));
        }

        public async Task<List<AverageResults>> GetAverages(string userId, DateTime date)
        {
            var result = await _http.GetFromJsonAsync<List<AverageResults>>($"https://dashboard-api.salmonisland-f0d5c65e.northeurope.azurecontainerapps.io/api/Dashboard/GetMeasurementsAverages?userId={userId}&Date={date}"); // /GetAverages?userId-{x}
            if (result == null)
                throw new Exception("No results found");
            return result;
        }
    }
}
