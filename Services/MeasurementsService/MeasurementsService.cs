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

        public async Task GetMeasurements()
        {
            List<Measurement>? result;
            var measurementsInLocalStorage = await _localStorage.ContainKeyAsync("Measurements");
            if (measurementsInLocalStorage)
            {
                result = await _localStorage.GetItemAsync<List<Measurement>>("Measurements");
            }
            else
            {
                result = await _http.GetFromJsonAsync<List<Measurement>>("sample-data/measurements.json");
                await _localStorage.SetItemAsync<List<Measurement>>("Measurements", result);
            }

            if (result != null)
            {
                this.Measurements = result;
            }
        }

        public IEnumerable<Measurement> GetMeasurementsByType(string type)
        {
            return Measurements.Where(x => x.Type == type).OrderByDescending(x => x.Date);
        }

        public async Task GetSingleMeasurement(int id)
        {
            //Measurement? result;
            //var measurementsInLocalStorage = await _localStorage.ContainKeyAsync("Measurements");
            //if (measurementsInLocalStorage)
            //{
            //    var measurements = await _localStorage.GetItemAsync<List<Measurement>>("Measurements");
            //    result = measurements.First(m => m.Id == id);
            //}
            //else
            //{
            //    result = await _http.GetFromJsonAsync<Measurement>($"api/measurement/{id}");
            //}

            //if (result != null)
            //{
            //    return result;
            //}
            //throw new Exception("Measurement not found");
            Console.WriteLine($"getting measurement {id}");
        }

        public async Task CreateMeasurement(Measurement measurement)
        {
            //var result = await _http.PostAsJsonAsync("api/measurement", measurement);
            //var response = await result.Content.ReadFromJsonAsync<Measurement>();
            //// TODO: null check
            //Measurements.Add(response);
            //await _localStorage.SetItemAsync("Measurements", Measurements);
            Console.WriteLine($"creating measurement {measurement.Id}, {measurement}");
            _navManager.NavigateTo("measurements");
        }

        public async Task UpdateMeasurement(Measurement measurement)
        {
            //var result = await _http.PutAsJsonAsync($"api/measurement/{measurement.Id}", measurement);
            //var response = await result.Content.ReadFromJsonAsync<Measurement>();
            //// TODO: null check
            //int index = Measurements.FindIndex(m => m.Id == measurement.Id);
            //if (index != -1)
            //{
            //    Measurements[index] = measurement;
            //    await _localStorage.SetItemAsync("Measurements", Measurements);
            //}
            Console.WriteLine($"Updating Measurement {measurement.Id}, {measurement}");
            _navManager.NavigateTo("measurements");
        }

        public async Task DeleteMeasurement(int id)
        {
            //await _http.DeleteAsync($"api/measurement/{id}");
            Console.WriteLine($"Deleting Measurement {id}");
            Measurements.RemoveAt(Measurements.FindIndex(m => m.Id == id));
            await _localStorage.SetItemAsync("Measurements", Measurements);
        }
    }
}
