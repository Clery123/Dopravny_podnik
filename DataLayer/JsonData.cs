using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DataLayer
{
    public class JsonData
    {
        private const string filePath = @"C:\Users\andre\source\repos\DPRV\DataLayer\log.json";
        public string title { get; set; }
        public string action { get; set; }
        public string time { get; set; }
        public static void LogIt(string ttl, string actn )
        {
            //List<JsonData> dejta = new List<JsonData>();
            var jsn = System.IO.File.ReadAllText(filePath);
            var dejta = JsonConvert.DeserializeObject<List<JsonData>>(jsn)?? new List<JsonData>();
            dejta.Add(new JsonData()
            {
                title = ttl,
                action = actn,
                time = DateTime.Now.ToString()
            }) ;
            string json = JsonConvert.SerializeObject(dejta.ToArray());
            System.IO.File.WriteAllText(filePath, json);
        }
    }
}
