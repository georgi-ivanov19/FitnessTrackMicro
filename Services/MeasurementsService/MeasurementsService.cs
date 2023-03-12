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
            result = await _http.GetFromJsonAsync<List<Measurement>>($"http://localhost:8083/api/Measurements?applicationUserId={applicationUserId}");
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
                result = await _http.GetFromJsonAsync<Measurement>($"http://localhost:8083/api/Measurements/{id}");
            //}

            //if (result != null)
            //{
            //    return result;
            //}
            //throw new Exception("Measurement not found");
            Console.WriteLine($"getting measurement {id}");
            return result;
        }

        public async Task CreateMeasurement(Measurement measurement)
        {
            //var result = await _http.PostAsJsonAsync("api/measurement", measurement);
            //
            //// TODO: null check
            //
            //await _localStorage.SetItemAsync("Measurements", Measurements);
            Console.WriteLine($"creating measurement {measurement.Id}, {measurement}");
            var result = await _http.PostAsJsonAsync("http://localhost:8083/api/Measurements", measurement);
            var response = await result.Content.ReadFromJsonAsync<Measurement>();
            Measurements.Add(response);
            Console.WriteLine(result.Content.ToString());
            _navManager.NavigateTo("measurements");
        }

        public async Task UpdateMeasurement(Measurement measurement)
        {
            var result = await _http.PutAsJsonAsync($"http://localhost:8083/api/Measurements/{measurement.Id}", measurement);
            var response = await result.Content.ReadFromJsonAsync<Measurement>();
            // TODO: null check
            int index = Measurements.FindIndex(m => m.Id == measurement.Id);
            if (index != -1)
            {
                Measurements[index] = measurement;
            //    await _localStorage.SetItemAsync("Measurements", Measurements);
            }
            //Console.WriteLine($"Updating Measurement {measurement.Id}, {measurement}");
            _navManager.NavigateTo("measurements");
        }

        public async Task DeleteMeasurement(int id)
        {
            Console.WriteLine($"Deleting Measurement {id}");
            await _http.DeleteAsync($"http://localhost:8083/api/Measurements/{id}");
            Measurements.RemoveAt(Measurements.FindIndex(m => m.Id == id));
        }

        public async Task<List<AverageResults>> GetAverages(string userId, DateTime date)
        {
            var result = await _http.GetFromJsonAsync<List<AverageResults>>($"http://localhost:8083/api/Measurements/GetAverages?userId={userId}&Date={date}"); // /GetAverages?userId-{x}
            if (result == null)
                throw new Exception("No results found");
            return result;
        }
    }
}
