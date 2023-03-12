using FitnessTrackMicro.Models;

namespace FitnessTrackMicro.Services.MeasurementsService
{
    public interface IMeasurementsService
    {
        List<Measurement> Measurements { get; set; }

        Task GetMeasurements(string applicationUserId);
        Task<Measurement> GetSingleMeasurement(int id);
        Task CreateMeasurement(Measurement measurement);
        Task UpdateMeasurement(Measurement measurement);
        Task DeleteMeasurement(int id);
        IEnumerable<Measurement> GetMeasurementsByType(string type);
        Task<List<AverageResults>> GetAverages(string userId, DateTime date);
    }
}
