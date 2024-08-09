using FloodSimulator.Model;
using Newtonsoft.Json;

namespace FloodSimulator.Services
{
    public class AlertService : IAlertServices
    {
        private List<Alert> AllAlerts = new List<Alert>();
        public readonly string FilePath = "Floods.json";
        private SemaphoreSlim _semaphore = new SemaphoreSlim(1,1);

        public AlertService()
        {
            if(File.Exists(FilePath))
            {
                string json = File.ReadAllText(FilePath);
                AllAlerts = JsonConvert.DeserializeObject<List<Alert>>(json)?? new List<Alert>();
            }
            else
            {
                AllAlerts = new List<Alert>();
            }
        }

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

        public async Task<List<Alert>> GetAlertByLocation(string area)
        {
            try
            {
                if(!File.Exists(FilePath))
                {
                    Console.WriteLine("Cant get Alert");
                    return null;
                }
                await _semaphore.WaitAsync();
                var alerts = AllAlerts.FindAll(Alert => Alert.Areas.Equals(area,StringComparison.OrdinalIgnoreCase));
                if(alerts == null)
                {
                    Console.WriteLine("Area not found");
                }
                
                return alerts;
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
