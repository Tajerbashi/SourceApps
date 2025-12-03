using System;
using System.IO;
using System.Net.Http;

namespace WCFApplication
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class ServiceGenerator : IServiceGenerator
    {
        public string Generator(int appointmentId, string filePath)
        {
            string url = $"https://localhost:7225/api/Appointment/Appointment/{appointmentId}";
            //string url = $"http://localhost:5000/api/Appointment/Appointment/{appointmentId}";

            using (HttpClient client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromSeconds(30);

                // Call API
                HttpResponseMessage response = client.GetAsync(url).Result;
                response.EnsureSuccessStatusCode();

                // Read the response as string
                string result = response.Content.ReadAsStringAsync().Result;

                // Ensure directory exists
                string directory = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                // Save to file
                File.WriteAllText(filePath, result);

                return filePath;
            }
        }

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
}
