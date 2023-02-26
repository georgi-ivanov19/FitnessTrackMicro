using FitnessTrackMicro.Pages;
using FitnessTrackMicro.Models;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using System.Diagnostics.Metrics;

namespace FitnessTrackMicro.Services.ExerciseService
{
    public class ExerciseService : IExerciseService
    {
        private readonly HttpClient _http;
        private readonly NavigationManager _navManager;
        public List<Exercise> Exercises { get; set; } = new List<Exercise>();

        public ExerciseService(HttpClient http, NavigationManager navManager)
        {
            _http = http;
            _navManager = navManager;
        }

        public async Task CreateExercise(Exercise ex)
        {
            //var result = await _http.PostAsJsonAsync("api/exercises", ex);
            //var response = await result.Content.ReadFromJsonAsync<Exercise>();
            //// TODO: null check
            //Exercises.Add(response);
            Console.WriteLine($"creating exercise {ex.Id}");
            //TODO:
            _navManager.NavigateTo($"workouts");
        }

        public async Task DeleteExercise(int id)
        {
            //await _http.DeleteAsync($"api/Exercises/{id}");
            //Exercises.RemoveAt(Exercises.FindIndex(r => r.Id == id));
            Console.WriteLine($"deleting exercise {id}");
        }

        public async Task GetExercises(int workoutId)
        {
            //var result = await _http.GetFromJsonAsync<List<Exercise>>($"api/Exercises/GetExercises/{workoutId}");
            //if (result != null)
            //{
            //    this.Exercises = result;
            //}
            Console.WriteLine($"Getting exercises for workout {workoutId}");
        }

        public async Task GetSingleExercise(int id)
        {
            //var result = await _http.GetFromJsonAsync<Exercise>($"api/Exercises/GetExercise/{id}");
            //if (result != null)
            //{
            //    return result;
            //}
            //throw new Exception("Exercise not found");
            Console.WriteLine($"getting exercise {id}");
        }

        public async Task UpdateExercise(Exercise ex)
        {
            //var result = await _http.PutAsJsonAsync($"api/Exercises/{ex.Id}", ex);
            //var response = await result.Content.ReadFromJsonAsync<Exercise>();
            //// TODO: null check
            //int index = Exercises.FindIndex(e => e.Id == ex.Id);
            //if (index != -1)
            //    Exercises[index] = ex;
            //_navManager.NavigateTo($"workout/{response.WorkoutId}");
            Console.WriteLine($"ipdating exercise {ex.Id}");
        }
    }
}
