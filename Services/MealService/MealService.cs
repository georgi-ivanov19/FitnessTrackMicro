using System.Diagnostics.Metrics;
using System.Net.Http.Json;
using Blazored.LocalStorage;
using Blazored.LocalStorage.StorageOptions;
using FitnessTrackMicro.Pages;
using FitnessTrackMicro.Models;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using System.Text;
using Microsoft.AspNetCore.Components.Authorization;

namespace FitnessTrackMicro.Services.MealService
{
    public class MealService : IMealService
    {
        private readonly HttpClient _http;
        private readonly NavigationManager _navManager;
        private ILocalStorageService _localStorage;
        private AuthenticationStateProvider _auth;

        public List<Meal> Meals { get; set; } = new List<Meal>();
        public MealService(HttpClient http, NavigationManager navManager, ILocalStorageService localStorage, AuthenticationStateProvider AuthenticationStateProvider)
        {
            _http = http;
            _navManager = navManager;
            _localStorage = localStorage;
            _auth = AuthenticationStateProvider;
        }
        public async Task GetMeals(string userId)
        {
            List<Meal>? result;
            // var mealsInLocalStorage = await _localStorage.ContainKeyAsync("Meals");
            // if (mealsInLocalStorage)
            // {
            //     result = await _localStorage.GetItemAsync<List<Meal>>("Meals");
            // }
            // else
            // {
                result = await _http.GetFromJsonAsync<List<Meal>>($"http://localhost:8085/api/Meals?applicationUserId={userId}");
                //await _localStorage.SetItemAsync<List<Meal>>("Meals", result);
            //}

            if (result != null)
            {
                this.Meals = result;
            }
        }

        public async Task CreateMeal(Meal meal)
        {
            var result = await _http.PostAsJsonAsync("http://localhost:8085/api/Meals", meal);
            var response = await result.Content.ReadFromJsonAsync<Meal>();
            Meals.Add(response);
            await _localStorage.SetItemAsync("Meals", Meals);
            _navManager.NavigateTo("meals");
        }

        public async Task DeleteMeal(int id)
        {
            var authenticationState = await _auth.GetAuthenticationStateAsync();
            var userId = authenticationState.User.FindFirst(c => c.Type == "sub")?.Value;
            //dynamic httpBody = new System.Dynamic.ExpandoObject();
            //httpBody.mealId = id;
            //httpBody.applicationUserId = userId;

            //var request = new HttpRequestMessage
            //{

            //    Method = HttpMethod.Delete,
            //    RequestUri = new Uri("https://localhost:49155/api/Meals"),
            //    Content = new StringContent(JsonConvert.SerializeObject(httpBody), Encoding.UTF8, "application/json")

            //};
            await _http.DeleteAsync($"http://localhost:8085/api/Meals?mealId={id}&applicationUserId={userId}");
            //await _http.SendAsync(request);

            Meals.RemoveAt(Meals.FindIndex(m => m.Id == id));
            // await _localStorage.SetItemAsync("Meals", Meals);
        }


        public async Task<Meal> GetSingleMeal(int id)
        {
            var authenticationState = await _auth.GetAuthenticationStateAsync();
            var userId = authenticationState.User.FindFirst(c => c.Type == "sub")?.Value;
            //dynamic httpBody = new System.Dynamic.ExpandoObject();

            //httpBody.applicationUserId = userId;
            //var request = new HttpRequestMessage
            //{
            //    Method = HttpMethod.Get,
            //    RequestUri = new Uri($"https://localhost:49155/api/Meals/{id}"),
            //    Content = new StringContent(JsonConvert.SerializeObject(httpBody), Encoding.UTF8, "application/json")
            //};
            //await _http.SendAsync(request);
            var response = await _http.GetFromJsonAsync<Meal>($"http://localhost:8085/api/Meals/{id}?applicationUserId={userId}");
            return response;
        }

        public async Task UpdateMeal(Meal meal)
        {
            var httpResult = await _http.PutAsJsonAsync($"http://localhost:8085/api/Meals/{meal.Id}", meal);
            var response = await httpResult.Content.ReadFromJsonAsync<Meal>();

            // TODO: null check
            int index = Meals.FindIndex(m => m.Id == meal.Id);
            if (index != -1)
            {
                Meals[index] = meal;
                await _localStorage.SetItemAsync("Meals", Meals);
            }
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
                totalCalories += (double)meal.TotalCalories;
                totalProtein += (double)meal.Protein;
                totalCarbohydrates += (double)meal.Carbohydrates;
                totalFats += (double)meal.Fats;
            }

            return new MealMacros(totalCalories, totalProtein, totalCarbohydrates, totalFats);
        }

        public async Task<List<AverageResults>> GetAverages(string userId, DateTime date)
        {
            var result = await _http.GetFromJsonAsync<List<AverageResults>>($"http://localhost:8087/api/Dashboard/GetMealsAverages?userId={userId}&date={date}");
            if (result == null)
                throw new Exception("No results found");
            return result;
        }
    }
}
