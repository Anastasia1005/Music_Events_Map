using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Music_Map.Classes
{
    /// <summary> Класс, который нужен для преобразования полученных из JSON-файла данных в список, удобный для чтения </summary>
    class JsonParser
    {
        /// <summary> Метод, который преобразует строку в список удобный для чтения  </summary>
        /// <param name="data"> JSON-файл, конвертированный в строку </param>
        /// <returns></returns>
        public List<EventData> ParseEventData(string data)
        {
            // первоначальный список, в который преобразуется строка
            List<Root> myDeserializedClass = JsonConvert.DeserializeObject<List<Root>>(data);
            // список, который будет возвращаться методом
            List <EventData> resultList = new List<EventData>();

            // перебираем все элементы в первоначальном списке
            foreach (Root dataList in myDeserializedClass)
            {
                // на каждый элемент первоначального списка создаём экземпляр класса события и передаём в него все необходимые данные
                EventData eventData = new EventData(
                    // во все события, которые будут созданы будет передаваться исполнитель из первого элемента первоначального списка
                    // это связано с особенностью получаемого JSON-файла
                    // там исполнитель указан только в первом событии
                    artist:      myDeserializedClass[0].artist,
                    venue:       dataList.venue,
                    description: dataList.description,
                    date:        dataList.datetime
                    );

                // созданное событие добавляем в список, который будем возвращать
                resultList.Add(eventData);
            }

            // возвращаем список событий
            return resultList;
        }
    }
}
