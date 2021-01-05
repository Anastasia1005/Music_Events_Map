using System;
using System.Collections.Generic;
using System.Globalization;
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
        public string EventName { get; set; }
        public PointLatLng GeoPoint { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public List<string> Lineup { get; set; } = new List<string>();

        public EventData(Artist artist, Venue venue, List<string> lineup, string description, DateTime date)
        {
            this.Artist = artist;
            this.Venue = venue;
            this.City = venue.city;
            this.Country = venue.country;
            this.EventName = venue.name;
            this.GeoPoint = new PointLatLng(Convert.ToDouble(venue.latitude, CultureInfo.InvariantCulture),
                                            Convert.ToDouble(venue.longitude, CultureInfo.InvariantCulture));
            this.Description = description;
            this.Date = date;

            foreach (string artistName in lineup)
            {
                this.Lineup.Add(artistName);
            }
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
                    ToolTip = "Кто: " + this.Artist.name +
                              "\nГде: " + this.Country + ", " + this.City + ", " + this.EventName +
                              "\nКогда: " + this.Date.ToString(), // всплывающая подсказка
                    Source = new BitmapImage(new Uri("pack://application:,,,/Resources/to_icon.png")) // картинка
                },
                Position = this.GeoPoint
            };

            return markerLocation;
        }
    }
}
