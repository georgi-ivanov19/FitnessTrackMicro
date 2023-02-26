using FitnessTrackMicro.Models;
    
namespace FitnessTrackMicro.Services.ExerciseSetService
{
    public interface IExerciseSetService
    {
        List<ExerciseSet> ExerciseSets { get; set; }

        Task CreateExerciseSetRange(Workout w, TrackedWorkout tw);
    }
}
