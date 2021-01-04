using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Music_Map.Classes
{
    class JsonParser
    {
        public List<EventData> ParseEventData(string data)
        {
            List<Root> myDeserializedClass = JsonConvert.DeserializeObject<List<Root>>(data);

            return new List<EventData>();
        }
    }
}
