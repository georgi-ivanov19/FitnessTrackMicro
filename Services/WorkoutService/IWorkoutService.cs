using FitnessTrackMicro.Models;

namespace FitnessTrackMicro.Services.WorkoutService
{
    public interface IWorkoutService
    {
        List<Workout> Workouts { get; set; }

        Task GetWorkouts();
        Task GetSingleWorkout(int id);
        Task CreateWorkout(Workout workout);
        Task UpdateWorkout(Workout workout, bool fromForm);
        Task DeleteWorkout(int id);
        List<Workout> UpcomingWorkouts();
        //TODO: get excercises
    }
}
