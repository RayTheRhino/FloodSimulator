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

        public async Task<Alert> GetAlertByLocation(double lat, double lang)
        {
            try
            {
                if(!File.Exists(FilePath))
                {
                    Console.WriteLine("Cant get Alert");
                    return null;
                }
                await _semaphore.WaitAsync();
                var alerts = AllAlerts.Find(Alert => Alert.Lat == lat && Alert.Long == lang);
                if(alerts == null)
                {
                    Console.WriteLine("Invalid lat and log");
                    return null;
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
