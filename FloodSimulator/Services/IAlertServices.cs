using FloodSimulator.Model;

namespace FloodSimulator.Services
{
    public interface IAlertServices
    {
        public Task<List<Alert>> GetAllAlerts();
        public Task<List<Alert>> GetAlertByLocation(string area);

    }
}
