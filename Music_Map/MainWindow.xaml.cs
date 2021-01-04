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
        ApiClient apiClient = new ApiClient();

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
            Map.Zoom = 15;
            //установка фокуса карты
            Map.Position = new PointLatLng(55.012823, 82.950359);

            //настройка взаимодействия с картой
            Map.MouseWheelZoomType = MouseWheelZoomType.MousePositionAndCenter;
            Map.CanDragMap = true;
            Map.DragButton = MouseButton.Left;
        }

        // Проверяю метод, создающий метки
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PointLatLng point = new PointLatLng(0, 0);
            DateTime date = DateTime.Now;

            CreateEvent("Queen", "Моя свадьба", date, point);
        }

        // Метод, создающий событие на карте
        // Создаёт экземпляр класса Location и вызывает его метод GetMarker(), чтобы поставить метку на карте
        // Картинка метки лежит в папке Resources
        private void CreateEvent(string artist, string loc, DateTime date, PointLatLng geoPoint)
        {
            // Указываю класс так, потому что без "Classes.", визуалка думает что я создаю не класс, который создала я, а класс, который уже есть в системе
            Classes.Location locatoin = new Classes.Location(artist, loc, date, geoPoint);
            // Метка ставится на карту
            Map.Markers.Add(locatoin.GetMarker());
            // Карта фокусируется на метке
            Map.Position = locatoin.GetFocus();
        }

        private void SearchDataBut_Click(object sender, RoutedEventArgs e)
        {
            apiClient.LoadEventData(artistTextBox.Text);
        }
    }
}
