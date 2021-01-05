using System.Collections.Generic;
using System.Windows;
using RestSharp;

namespace Music_Map.Classes
{
    /// <summary> Класс, описывающий API клиент, который получает JSON-файл </summary>
    class ApiClient
    {
        /// <summary> Строка, содержащая ключ API </summary>
        const string API_KEY = "d7642c0a498856436b4a6f5bec5e5042";
        /// <summary> Строка, содержащая ссылку-шаблон в которую при форматировании будут подставляться:
        /// {0} - имя исполнителя
        /// {1} - ключ API </summary>
        const string URL = "https://rest.bandsintown.com/v4/artists/{0}/events/?app_id={1}";

        /// <summary> Клиент, через окторый будет идти обращение к серверу </summary>
        readonly RestClient client = null;
        /// <summary> Экземпляр класса, который будет преобразовывать JSON-файл в список событий, удобный для чтения </summary>
        readonly JsonParser jsonParser = new JsonParser();

        /// <summary> Конструктор класса </summary>
        public ApiClient()
        {
            /// <summary> Создаём клиент и передаём в него ссылку-шаблон </summary>
            client = new RestClient(URL)
            {
                // означает что клиент будет работать в реальном времени, без задержек
                Timeout = -1
            };
        }

        /// <summary> Метод для загрузки JSON-файла, в него передаём имя исполнителя, события с которым ищем </summary>
        public List<EventData> LoadEventData(string artist)
        {
            // форматируем строку-шаблон и подставляем в неё имя исполнителя и ключ API
            string uri = string.Format(URL, artist, API_KEY);
            // создаём новый запрос на получение JSON-файла
            var request = new RestRequest(uri, Method.GET);
            // обращаемся к серверу
            var response = client.Execute(request);
            // пеобразуем полученный JSON-файл в строку
            string data = response.Content;

            // если в строке было это
            if (data.Contains("[NotFound] The artist was not found"))
            {
                // выводим окошко с сообщением
                MessageBox.Show("Исполнитель не найден.");

                // и возвращаем пустой список
                return new List<EventData>();
            }
            else
            {
                MessageBox.Show("Исполнитель найден.");
                
                // иначе используем метод класса JSON-парсера и возвращаем список событий
                return jsonParser.ParseEventData(data);
            }
        }
    }
}