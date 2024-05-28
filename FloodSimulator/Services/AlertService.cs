using FloodSimulator.Model;

namespace FloodSimulator.Services
{
    public class AlertService : IAlertServices
    {
        private List<Alert> AllAlerts = new List<Alert>();
        private SemaphoreSlim _semaphore = new SemaphoreSlim(1,1);
        public async Task<List<Alert>> GetAllAlerts()
        {
            try
            {
                await _semaphore.WaitAsync();
                return AllAlerts;
            }
            catch (Exception)
            {

                throw;
            }finally 
            { 
                _semaphore.Release();
            }
        }

        public async Task<Alert> GetAlertByLocation(double lat, double lang)
        {
            try
            {
                await _semaphore.WaitAsync();
                foreach (Alert alert in AllAlerts)
                {
                    if (alert.Lat == lat  && alert.Long == lang)
                    {
                        return alert;
                    }
                    
                }
                return null;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                _semaphore.Release();
            }
        }
    }
}
