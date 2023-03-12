using Blazored.LocalStorage;
using FitnessTrackMicro.Pages;
using FitnessTrackMicro.Models;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using System.Diagnostics.Metrics;

namespace FitnessTrackMicro.Services.WorkoutService
{
    public class WorkoutService : IWorkoutService
    {
        private readonly HttpClient _http;
        private readonly NavigationManager _navManager;
        private ILocalStorageService _localStorage;
        public List<Workout> Workouts { get; set; } = new List<Workout>();

        public WorkoutService(HttpClient http, NavigationManager navManager, ILocalStorageService localStorage)
        {
            _http = http;
            _navManager = navManager;
            _localStorage = localStorage;
        }
        public async Task CreateWorkout(Workout workout)
        {
            var result = await _http.PostAsJsonAsync("http://localhost:8081/api/workouts", workout);
            var response = await result.Content.ReadFromJsonAsync<Workout>();
            // TODO: null check
            Workouts.Add(response);
           // await _localStorage.SetItemAsync("Workouts", Workouts);
            _navManager.NavigateTo($"workout/{response.Id}");
        }

        public async Task DeleteWorkout(int id)
        {
            await _http.DeleteAsync($"http://localhost:8081/api/workouts/{id}");
            Workouts.RemoveAt(Workouts.FindIndex(r => r.Id == id));
            //await _localStorage.SetItemAsync("Workouts", Workouts);
        }

        public async Task<Workout> GetSingleWorkout(int id)
        {
            //var workoutsInLocalStorage = await _localStorage.ContainKeyAsync("Workouts");
            Workout? result;
            //if (workoutsInLocalStorage)
            //{
            //    var workouts = await _localStorage.GetItemAsync<List<Workout>>("Workouts");
            //    result = workouts.First(w => w.Id == id);
            //}
            //else
            //{
                result = await _http.GetFromJsonAsync<Workout>($"http://localhost:8081/api/workouts/GetWorkout/{id}");
            //}

            if (result != null)
            {
                return result;
            }
            throw new Exception("Workout not found");
        }

        public async Task GetWorkouts(string userId)
        {
            List<Workout>? result;
            //var workoutsInLocalStorage = await _localStorage.ContainKeyAsync("Workouts");
            //if (workoutsInLocalStorage)
            //{
            //    result = await _localStorage.GetItemAsync<List<Workout>>("Workouts");
            //} else
            //{
                result = await _http.GetFromJsonAsync<List<Workout>>($"http://localhost:8081/api/workouts?userId={userId}");
            //    await _localStorage.SetItemAsync<List<Workout>>("Workouts", result);
            //}

            if (result != null)
            {
                this.Workouts = result;
            }
        }

        public async Task UpdateWorkout(Workout workout, bool fromForm)
        {
            var result = await _http.PutAsJsonAsync($"http://localhost:8081/api/workouts/{workout.Id}", workout);
            var response = await result.Content.ReadFromJsonAsync<Workout>();
            // TODO: null check
            int index = Workouts.FindIndex(w => w.Id == workout.Id);
            if (index != -1)
            {
                Workouts[index] = workout;
                // await _localStorage.SetItemAsync("Workouts", Workouts);
            }
            Console.WriteLine($"updating workout {workout.Id}, From form {fromForm}");

            if (fromForm)
            {
                _navManager.NavigateTo("workouts");
            }
            else
            {
                _navManager.NavigateTo($"workout/{workout.Id}");
            }
        }

        public List<Workout> UpcomingWorkouts()
        {
            var listToReturn = new List<Workout>();
            var current = DateTime.Today;
            var end = DateTime.Today.AddDays(6);

            while (current <= end)
            {
                if (listToReturn.Count >= 3)
                    return listToReturn;
                var workoutsForTheDay = this.Workouts.FindAll(w => w.DayOfWeek == current.DayOfWeek.ToString());
                if (workoutsForTheDay.Count > 0)
                    listToReturn.AddRange(workoutsForTheDay);
                current = current.AddDays(1);
            }

            return listToReturn;
        }
    }
}
