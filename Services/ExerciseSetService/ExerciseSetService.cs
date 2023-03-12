using Blazored.LocalStorage;
using FitnessTrackMicro.Models;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace FitnessTrackMicro.Services.ExerciseSetService
{
    public class ExerciseSetService : IExerciseSetService
    {
        private readonly HttpClient _http;
        private ILocalStorageService _localStorage;
        public List<ExerciseSet> ExerciseSets { get; set; } = new List<ExerciseSet>();

        public ExerciseSetService(HttpClient http, ILocalStorageService localStorage)
        {
            _http = http;
            _localStorage = localStorage;
        }

        public async Task CreateExerciseSetRange(Workout w, TrackedWorkout tw)
        {
            List<ExerciseSet> list = new List<ExerciseSet>();
            foreach (var item in w.Exercises)
            {
                for (int i = 0; i < item.DefaultNumberOfSets; i++)
                {
                    list.Add(new ExerciseSet
                    {
                        Weight = null,
                        Reps = null,
                        ExerciseId = item.Id,
                        ExerciseName = item.Name,
                        TrackedWorkoutId = tw.Id,
                    });
                }
            }
            var result = await _http.PostAsJsonAsync($"http://localhost:8081/api/ExerciseSets/range", list);

            var response = await result.Content.ReadFromJsonAsync<List<ExerciseSet>>();
            // var trackedWorkout = await _localStorage.GetItemAsync<TrackedWorkout>($"TrackedWorkout{tw.Id}");
            ExerciseSets = response;
            //trackedWorkout.ExerciseSetsCompleted = ExerciseSets;
            //await _localStorage.SetItemAsync($"TrackedWorkout{tw.Id}", trackedWorkout);
        }
        public async Task<ExerciseSet> UpdateExerciseSet(ExerciseSet set)
        {
            var result = await _http.PutAsJsonAsync($"http://localhost:8081/api/ExerciseSets/{set.Id}", set);
            var response = await result.Content.ReadFromJsonAsync<ExerciseSet>();
            // TODO: null check
            int index = ExerciseSets.FindIndex(e => e.Id == set.Id);
            if (index != -1)
                ExerciseSets[index] = set;

            return response;
        }
    }
}
