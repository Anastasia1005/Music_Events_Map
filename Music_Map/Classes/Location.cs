using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using GMap.NET;
using GMap.NET.WindowsPresentation;

namespace Music_Map.Classes
{
    /// <summary> Описывает метку на карте </summary>
    class Location
    {
        /// <summary> Имя исполнителя </summary>
        string Artist { get; set; }
        /// <summary> Место, в котором будет проводиться концерт </summary>
        string Venue { get; set; }
        /// <summary> Дата проведения концерта </summary>
        DateTime Date { get; set; }
        /// <summary> Координаты метки (соответственно и концерта) на карте </summary>
        PointLatLng GeoPoint { get; set; }

        /// <summary> Конструктор </summary>
        public Location(string artist, string loc, DateTime date, PointLatLng geoPoint)
        {
            this.Artist = artist;
            this.Venue = loc;
            this.Date = date;
            this.GeoPoint = geoPoint;
        }

        /// <summary> Возвращает позицию события в виде координат </summary>
        public PointLatLng GetFocus()
        {
            return this.GeoPoint;
        }

        /// <summary> Возвращает маркер события </summary>
        public GMapMarker GetMarker()
        {
            GMapMarker markerLocation = new GMapMarker(GeoPoint)
            {
                Shape = new Image
                {
                    Width = 30, // ширина маркера
                    Height = 30, // высота маркера
                    ToolTip = "Кто: " + this.Artist + "\nГде: " + this.Venue + "\nКогда: " + this.Date.ToString(), // всплывающая подсказка
                    Source = new BitmapImage(new Uri("pack://application:,,,/Resources/to_icon.png")) // картинка
                },
                Position = this.GeoPoint
            };

            return markerLocation;
        }
    }
}
