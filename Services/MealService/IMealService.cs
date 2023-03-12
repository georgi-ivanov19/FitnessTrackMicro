using FitnessTrackMicro.Models;

namespace FitnessTrackMicro.Services.MealService
{
  public interface IMealService
  {
    List<Meal> Meals { get; set; }

    Task GetMeals(string userId);
        Task<Meal> GetSingleMeal(int id);
        Task CreateMeal(Meal meal);
        Task UpdateMeal(Meal meal);
        Task DeleteMeal(int id);
        IEnumerable<Meal> GetMealsByDate(DateTime date);
        MealMacros CalculateMacros(IEnumerable<Meal> meals);
        Task<List<AverageResults>> GetAverages(string userId, DateTime date);
  }
}
