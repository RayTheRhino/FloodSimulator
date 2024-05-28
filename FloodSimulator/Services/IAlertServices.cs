using FloodSimulator.Model;

namespace FloodSimulator.Services
{
    public interface IAlertServices
    {
        public Task<List<Alert>> GetAllAlerts();
        public Task<Alert> GetAlertByLocation(double lat, double lang);

    }
}
