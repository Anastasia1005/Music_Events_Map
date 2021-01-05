using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsPresentation;
using System.Device.Location;
using Music_Map.Classes;

namespace Music_Map
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary> Экземпляр класса ApiClient, который обращается к серверу и получает JSON-файл </summary>
        readonly ApiClient apiClient = new ApiClient();

        /// <summary> Список, хранящий все музыкальные события, в которых учавствует выбранный исполнитель </summary>
        List<EventData> eventList = new List<EventData>();
        /// <summary> Список, хранящий те музыкальные события исполнителя, которые отмечаются на карте </summary>
        List<EventData> actualEventList = new List<EventData>();

        /// <summary> Строка, которая представляет собой шаблон, для вывода
        /// подробной информации о событии, на метку которого нажал пользователь
        /// при форматировании этой строки вместо индексов будут подставляться значения для вывода</summary>
        private string textEventData = "Группа: {0}\n\n" +
                                       "Описание: {1}\n\n" +
                                       "Дата проведения: {2}\n\n" +
                                       "Место проведения: {3}\n\n";

        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary> Установки для карты </summary>
        private void Map_Loaded(object sender, RoutedEventArgs e)
        {
            // настройка доступа к данным
            GMaps.Instance.Mode = AccessMode.ServerAndCache;

            // установка провайдера карт
            Map.MapProvider = OpenStreetMapProvider.Instance;

            //установка провайдера карт
            Map.MapProvider = OpenStreetMapProvider.Instance;

            //установка зума карты
            Map.MinZoom = 2;
            Map.MaxZoom = 17;
            Map.Zoom = 3;
            //установка фокуса карты
            Map.Position = new PointLatLng(55.012823, 82.950359);

            //настройка взаимодействия с картой
            Map.MouseWheelZoomType = MouseWheelZoomType.MousePositionAndCenter;
            Map.CanDragMap = true;
            Map.DragButton = MouseButton.Left;
        }

        /// <summary> Метод обработки нажатия на карту </summary>
        private void Map_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // точка, в которую записываются координаты места, на которое нажал пользователь на карте
            PointLatLng point = Map.FromLocalToLatLng((int)e.GetPosition(Map).X, (int)e.GetPosition(Map).Y);

            // если список содержит хотя бы одно событие
            if (actualEventList.Count != 0)
            {
                // то список сортируется по увеличению расстоянию до точки, считанной с карты
                // и далее будет использоваться только первый элемент списка, т. к. он ближайший
                actualEventList.Sort((obj1, obj2) => obj1.GetDistance(point).CompareTo(obj2.GetDistance(point)));

                string descr;

                // если в событии указано описание, то для вывода будем использовать её
                if (actualEventList[0].Description != "")
                    descr = actualEventList[0].Description;
                // иначе заменяем описание на такую строку
                else
                    descr = "Организатор оказался очень ленивым и не предоставил описания :(";

                // выводим в текстовый блок отформатированную строку-шаблон (первый аргумент)
                eventDataTxtBlock.Text = string.Format(textEventData,
                                                    actualEventList[0].Artist.name,
                                                    descr,
                                                    actualEventList[0].Date.ToString(),
                                                    actualEventList[0].GetLocation());
            }
        }

        /// <summary> Метод обработки нажатия на кнопку поиска музыкальных событий указанного автора </summary>
        private void SearchDataBut_Click(object sender, RoutedEventArgs e)
        {
            // если в поле ввода имени исполнителя что-нибудь введено
            if (artistTextBox.Text != "")
            {
                // удаляем все маркеры на карте
                Map.Markers.Clear();
                // очищаем списки событий
                eventList.Clear();
                actualEventList.Clear();
                // очищаем текстовый блок, в который выводится описание события
                eventDataTxtBlock.Text = "";
                //это всё на случай, если ищем события не в первый раз

                // после очищения вызываем метод, в который передаём список найденных событий
                CreateEvents(apiClient.LoadEventData(artistTextBox.Text));
            }
            else
                MessageBox.Show("Перед поиском введите имя исполнителя.");
        }

        /// <summary> Метод, который создаёт метки событий на карте и заполняет списки событий </summary>
        private void CreateEvents(List<EventData> events)
        {
            // если в метод передан список содержащий хотя бы один элемент
            if (events.Count != 0)
            {
                // перебираем каждый элемент класса EventData в нём
                foreach (EventData eventData in events)
                {
                    // и добавляем текущий элемент в списки событий исполнителя и создаём их маркеры
                    eventList.Add(eventData);
                    actualEventList.Add(eventData);
                    Map.Markers.Add(eventData.GetMarker());
                }

                // центрируем карту на первом элементе переданного списка
                Map.Position = events[0].GeoPoint;
            }
        }

        /// <summary> Обработка нажатия на кнопку фильтрации музыкальных событий по городу и/или стране проведения </summary>
        private void VenueFiltrBut_Click(object sender, RoutedEventArgs e)
        {
            // если список со всеми событиями содержит хотя бы одно событие
            if (eventList.Count != 0)
            {
                // если поле ввода страны или города не содержит пустую строку (заполнено хотя бы одним символом)
                if (countryFiltrTxtBox.Text != "" || cityFiltrTxtBox.Text != "")
                {
                    // очищаем карту от меток, список событий, которые выводятся на карту и текстовый блок с подробным описанием события
                    Map.Markers.Clear();
                    actualEventList.Clear();
                    eventDataTxtBlock.Text = "";

                    // фильтруем события
                    EventsFiltrationByVenue();
                }
                else
                        MessageBox.Show("Перед сортировкой, введите названия страны и/или города, в которых вы хотите найти концерты с данным исполнителем.");
            }
            else
                MessageBox.Show("Перед нажатием этой кнопки найдите интересующего вас исполнителя.");
        }

        // метод для сортировки событий по городу и/или месту
        private void EventsFiltrationByVenue()
        {
            // если что-то написано в текстовом боксе для ввода города
            if (countryFiltrTxtBox.Text != "" && cityFiltrTxtBox.Text == "")
            {
                // перебираем все элементы типа EventData в списке всех событий
                foreach (EventData e in eventList)
                {
                    // если страна в данном событии (элементе) равен строке, введённой в текстовый бокс
                    if (e.Country == countryFiltrTxtBox.Text)
                    {
                        // добавляем это событие в список событий, которые будут выводиться на карту
                        actualEventList.Add(e);
                        // и сразу выводим его на карту
                        Map.Markers.Add(e.GetMarker());
                    }
                }
            }
            // если что-то написано в текстовом боксе для ввода города
            else if (countryFiltrTxtBox.Text == "" && cityFiltrTxtBox.Text != "")
            {
                // перебираем все элементы типа EventData в списке всех событий
                foreach (EventData e in eventList)
                {
                    // если город в данном событии (элементе) равен строке, введённой в текстовый бокс
                    if (e.City == cityFiltrTxtBox.Text)
                    {
                        // добавляем это событие в список событий, которые будут выводиться на карту
                        actualEventList.Add(e);
                        // и сразу выводим его на карту
                        Map.Markers.Add(e.GetMarker());
                    }
                }
            }
            // если что-то написано в обоих текстовых боксах
            else
            {
                // перебираем все элементы типа EventData в списке всех событий
                foreach (EventData e in eventList)
                {
                    // если страна и город в данном событии (элементе) равен строкам, введённым в текстовые боксы
                    if (e.Country == countryFiltrTxtBox.Text && e.City == cityFiltrTxtBox.Text)
                    {
                        // добавляем это событие в список событий, которые будут выводиться на карту
                        actualEventList.Add(e);
                        // и сразу выводим его на карту
                        Map.Markers.Add(e.GetMarker());
                    }
                }
            }
        }
    }
}