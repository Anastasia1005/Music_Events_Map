using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using GMap.NET;
using GMap.NET.WindowsPresentation;

namespace Music_Map.Classes
{
    class EventData
    {
        public Artist Artist { get; set; }
        public Venue Venue { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }
        public PointLatLng GeoPoint { get; set; }
        public DateTime Date { get; set; }
        
        public EventData(Artist artist, Venue venue, string city, string country, string latitude, string longitude,
                                                     string description, string date)
        {
            this.Artist = artist;
            this.Venue = venue;
            this.City = city;
            this.Country = country;
            this.Description = description;
            this.GeoPoint = new PointLatLng(Convert.ToDouble(latitude), Convert.ToDouble(longitude));
            this.Date = DateTime.Parse(date);
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
