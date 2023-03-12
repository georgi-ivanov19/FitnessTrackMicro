using Blazored.LocalStorage;
using FitnessTrackMicro.Pages;
using FitnessTrackMicro.Models;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace FitnessTrackMicro.Services.TrackedWorkoutService
{
    public class TrackedWorkoutService : ITrackedWorkoutService
    {
        private readonly HttpClient _http;
        private ILocalStorageService _localStorage;


        public TrackedWorkoutService(HttpClient http, ILocalStorageService localStorage)
        {
            _http = http;
            _localStorage = localStorage;
        }
        public List<TrackedWorkout> TrackedWorkouts { get; set; } = new List<TrackedWorkout>();

        public async Task<TrackedWorkout> StartWorkout(Workout workout)
        {

            TrackedWorkout workoutToStart = new TrackedWorkout();
            workoutToStart.WorkoutId = workout.Id;
            workoutToStart.TotalVolume = 0;
            workoutToStart.StartTime = DateTime.Now;

            var result = await _http.PostAsJsonAsync($"http://localhost:8081/api/TrackedWorkouts", workoutToStart);
            var response = await result.Content.ReadFromJsonAsync<TrackedWorkout>();
            // TODO: null check
            TrackedWorkouts.Add(response);
            // await _localStorage.SetItemAsync($"TrackedWorkout{response.Id}", response);

            return response;
        }

        public async Task<TrackedWorkout> GetSingleWorkout(int id)
        {
            //var workoutInLocalStorage = await _localStorage.ContainKeyAsync($"TrackedWorkout{id}");
            TrackedWorkout? result;
            //if (workoutInLocalStorage)
            //{
            //    result = await _localStorage.GetItemAsync<TrackedWorkout>($"TrackedWorkout{id}");
            //}
            //else
            //{
                result = await _http.GetFromJsonAsync<TrackedWorkout>($"http://localhost:8081/api/TrackedWorkouts/GetWorkout/{id}");
            //}

            if (result != null)
            {
                return result;
            }
            throw new Exception("Workout not found");
        }

        public async Task<TrackedWorkout?> GetLatestCompleted(int parentWorkoutId)
        {

            //var workoutsInLocalStorage = await _localStorage.ContainKeyAsync("Workouts");
            TrackedWorkout? result;
            //if (workoutsInLocalStorage)
            //{
            //    var workouts = await _localStorage.GetItemAsync<List<Workout>>("Workouts");
            //    var parentWorkout = workouts.First(w => w.Id == parentWorkoutId);
            //    result = parentWorkout.TrackedWorkouts.Where(w => w.WorkoutId == parentWorkoutId && w.IsCompleted).OrderByDescending(w => w.EndTime).FirstOrDefault(w => w.WorkoutId == parentWorkoutId);
            //}
            //else
            //{
                result = await _http.GetFromJsonAsync<TrackedWorkout>($"http://localhost:8081/api/TrackedWorkouts/GetLatestCompleted/{parentWorkoutId}");
            //}

            if (result != null)
            {
                return result;
            }
            return null;
        }

        public async Task UpdateTrackedWorkout(TrackedWorkout workout, bool finish = false)
        {
            var result = await _http.PutAsJsonAsync($"http://localhost:8081/api/TrackedWorkouts/{workout.Id}", workout);
            var response = await result.Content.ReadFromJsonAsync<TrackedWorkout>();
            int index = TrackedWorkouts.FindIndex(w => w.Id == workout.Id);
            if (index != -1)
            {
                TrackedWorkouts[index] = workout;
                //await _localStorage.SetItemAsync($"TrackedWorkout{response.Id}", workout);
            }
            //if (finish)
            //{
            //    var workoutsInLocalStorage = await _localStorage.ContainKeyAsync("Workouts");
            //    if (workoutsInLocalStorage)
            //    {
            //        var workouts = await _localStorage.GetItemAsync<List<Workout>>("Workouts");
            //        workouts.First(w => w.Id == workout.WorkoutId).TrackedWorkouts.Add(workout);
            //        await _localStorage.SetItemAsync("Workouts", workouts);
            //    }
            //}
        }

        public async Task<List<TrackedWorkout>> GetCompletedTrackedWorkouts(int parentWorkoutId)
        {
            List<TrackedWorkout>? result;
            //var workoutsInLocalStorage = await _localStorage.ContainKeyAsync("Workouts");
            //if (workoutsInLocalStorage)
            //{
            //    var workouts = await _localStorage.GetItemAsync<List<Workout>>("Workouts");
            //    var parentWorkout = workouts.First(w => w.Id == parentWorkoutId);
            //    result = parentWorkout.TrackedWorkouts.Where(tw => tw.IsCompleted == true).ToList();
            //}
            //else
            //{
                result = await _http.GetFromJsonAsync<List<TrackedWorkout>>($"http://localhost:8081/api/TrackedWorkouts/{parentWorkoutId}");
                result = result.Where(tw => tw.IsCompleted == true).ToList();
            //}

            if (result != null)
            {
                return result;
            }
            else
            {
                return new List<TrackedWorkout>();
            }
        }

        public async Task<Dictionary<int, List<AverageResults>>> GetAverages(string userId, DateTime date)
        {
            var result = await _http.GetFromJsonAsync<Dictionary<int, List<AverageResults>>>($"http://localhost:8081/api/TrackedWorkouts/GetAverages?userId={userId}&Date={date}");
            if (result == null)
                throw new Exception("No results found");
            return result;
        }
    }
}
