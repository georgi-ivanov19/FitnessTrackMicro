using System.Diagnostics.Metrics;
using System.Net.Http.Json;
using Blazored.LocalStorage;
using Blazored.LocalStorage.StorageOptions;
using FitnessTrackMicro.Pages;
using FitnessTrackMicro.Models;
using Microsoft.AspNetCore.Components;

namespace FitnessTrackMicro.Services.MealService
{
    public class MealService : IMealService
    {
        private readonly HttpClient _http;
        private readonly NavigationManager _navManager;
        private ILocalStorageService _localStorage;

        public List<Meal> Meals { get; set; } = new List<Meal>();
        public MealService(HttpClient http, NavigationManager navManager, ILocalStorageService localStorage)
        {
            _http = http;
            _navManager = navManager;
            _localStorage = localStorage;
        }
        public async Task GetMeals()
        {
            List<Meal>? result;
            var mealsInLocalStorage = await _localStorage.ContainKeyAsync("Meals");
            if (mealsInLocalStorage)
            {
                result = await _localStorage.GetItemAsync<List<Meal>>("Meals");
            }
            else
            {
                result = await _http.GetFromJsonAsync<List<Meal>>("sample-data/meals.json");
                await _localStorage.SetItemAsync<List<Meal>>("Meals", result);
            }

            if (result != null)
            {
                this.Meals = result;
            }
        }

        public async Task CreateMeal(Meal meal)
        {
            //var result = await _http.PostAsJsonAsync("api/meal", meal);
            //var response = await result.Content.ReadFromJsonAsync<Meal>();
            //// TODO: null check
            //Meals.Add(response);
            //await _localStorage.SetItemAsync("Meals", Meals);
            Console.WriteLine($"Creating a meal {meal.Id}, {meal}");
            _navManager.NavigateTo("meals");
        }

        public async Task DeleteMeal(int id)
        {

            //await _http.DeleteAsync($"api/meal/{id}");
            Console.WriteLine($"Deleting Meal {id}");
            Meals.RemoveAt(Meals.FindIndex(m => m.Id == id));
            await _localStorage.SetItemAsync("Meals", Meals);
        }


        public async Task GetSingleMeal(int id)
        {
            //var mealsInLocalStorage = await _localStorage.ContainKeyAsync("Meals");
            //Meal? result;
            //if (mealsInLocalStorage)
            //{
            //    var meals = await _localStorage.GetItemAsync<List<Meal>>("Meals");
            //    result = meals.First(m => m.Id == id);
            //}
            //else
            //{
            //    result = await _http.GetFromJsonAsync<Meal>($"api/meal/{id}");
            //}
            //if (result != null)
            //{
            //    return result;
            //}
            //throw new Exception("Meal not found");
            Console.WriteLine($"Getting Meal {id}");
        }

        public async Task UpdateMeal(Meal meal)
        {
            //var httpResult = await _http.PutAsJsonAsync($"api/meal/{meal.Id}", meal);
            //var response = await httpResult.Content.ReadFromJsonAsync<Meal>();

            //// TODO: null check
            //int index = Meals.FindIndex(m => m.Id == meal.Id);
            //if (index != -1)
            //{
            //    Meals[index] = meal;
            //    await _localStorage.SetItemAsync("Meals", Meals);
            //}
            Console.WriteLine($"Updating meal {meal.Id}");
            _navManager.NavigateTo("meals");
        }

        public IEnumerable<Meal> GetMealsByDate(DateTime date)
        {
            return Meals.Where(m => m.Date.Date == date.Date);
        }

        public MealMacros CalculateMacros(IEnumerable<Meal> meals)
        {
            double totalProtein = 0;
            double totalCalories = 0;
            double totalCarbohydrates = 0;
            double totalFats = 0;
            foreach (var meal in meals)
            {
                totalCalories += meal.TotalCalories;
                totalProtein += meal.Protein;
                totalCarbohydrates += meal.Carbohydrates;
                totalFats += meal.Fats;
            }

            return new MealMacros(totalCalories, totalProtein, totalCarbohydrates, totalFats);
        }

        public async Task<List<AverageResults>> GetAverages(DateTime date)
        {
            var result = await _http.GetFromJsonAsync<List<AverageResults>>($"sample-data/mealAverages.json");
            if (result == null)
                throw new Exception("No results found");
            return result;
        }
    }
}
