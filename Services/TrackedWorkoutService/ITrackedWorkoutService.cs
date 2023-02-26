using FitnessTrackMicro.Pages;
using FitnessTrackMicro.Models;

namespace FitnessTrackMicro.Services.TrackedWorkoutService
{
    public interface ITrackedWorkoutService
    {
        List<TrackedWorkout> TrackedWorkouts { get; set; }
        Task GetCompletedTrackedWorkouts(int parentWorkoutId);
        Task StartWorkout(Workout workout);
        Task GetSingleWorkout(int id);
        Task GetLatestCompleted(int parentWorkoutId);
        Task UpdateTrackedWorkout(TrackedWorkout workout, bool finish = false);
    }
}
