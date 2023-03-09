using FitnessTrackMicro.Pages;
using FitnessTrackMicro.Models;

namespace FitnessTrackMicro.Services.TrackedWorkoutService
{
    public interface ITrackedWorkoutService
    {
        List<TrackedWorkout> TrackedWorkouts { get; set; }
        Task<List<TrackedWorkout>> GetCompletedTrackedWorkouts(int parentWorkoutId);
        Task<TrackedWorkout> StartWorkout(Workout workout);
        Task<TrackedWorkout> GetSingleWorkout(int id);
        Task<TrackedWorkout?> GetLatestCompleted(int parentWorkoutId);
        Task UpdateTrackedWorkout(TrackedWorkout workout, bool finish = false);
        Task<Dictionary<int, List<AverageResults>>> GetAverages(string userId, DateTime date);
    }
}
