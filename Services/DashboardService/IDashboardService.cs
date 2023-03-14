using FitnessTrackMicro.Models;

namespace FitnessTrackMicro.Services.DashboardService
{
    public interface IDashboardService
    {
        public Task<DashboardResults> GetDashboardData(string userId, DateTime date);
    }
}
