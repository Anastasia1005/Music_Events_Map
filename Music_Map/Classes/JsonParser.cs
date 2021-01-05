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
            List <EventData> resultList = new List<EventData>();

            foreach (Root dataList in myDeserializedClass)
            {
                EventData eventData = new EventData(
                    artist:      myDeserializedClass[0].artist,
                    venue:       dataList.venue,
                    lineup:      dataList.lineup,
                    description: dataList.description,
                    date:        dataList.datetime
                    );

                resultList.Add(eventData);
            }

            return resultList;
        }
    }
}
