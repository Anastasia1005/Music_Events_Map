using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using RestSharp;

namespace Music_Map.Classes
{
    class ApiClient
    {
        const string API_KEY = "d7642c0a498856436b4a6f5bec5e5042";
        const string URL = "https://rest.bandsintown.com/v4/artists/{0}/events/?app_id={1}";

        readonly RestClient client = null;
        readonly JsonParser jsonParser = new JsonParser();

        public ApiClient()
        {
            client = new RestClient(URL)
            {
                Timeout = -1
            };
        }

        public List<EventData> LoadEventData(string artist = "the lumineers")
        {
            string uri = string.Format(URL, artist, API_KEY);
            var request = new RestRequest(uri, Method.GET);
            var response = client.Execute(request);
            string data = response.Content;

            if (data.Contains("[NotFound] The artist was not found"))
            {
                MessageBox.Show("The artist was not found.");

                return new List<EventData>();
            }
            else
            {
                MessageBox.Show("The artist was found.");
                
                return jsonParser.ParseEventData(data);
            }
        }

    }
}
