using System;
using System.Device.Location;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using GMap.NET;
using GMap.NET.WindowsPresentation;

namespace Music_Map.Classes
{
    /// <summary> Класс, описывающий музыкальное событие </summary>
    class EventData
    {
        /// <summary> Поле, хранящее информацию об исполнителе </summary>
        public Artist Artist { get; set; }
        /// <summary> Поле, хранящее информацию о месте проведения события </summary>
        public Venue Venue { get; set; }
        #region эти значения можно было доставать из Venue, но так обращаться к ним будет быстрее
        /// <summary> Поле, хранящее город, в котором проводится события </summary>
        public string City { get; set; }
        /// <summary> Поле, хранящее страну, в которой проводится событие </summary>
        public string Country { get; set; }
        /// <summary> Поле, хранящее название события </summary>
        public string EventName { get; set; }
        /// <summary> Поле, хранящее географические координаты (широту и долготу) места проведения события </summary>
        public PointLatLng GeoPoint { get; set; }
        #endregion
        /// <summary> Поле, хранящее описание события </summary>
        public string Description { get; set; }
        /// <summary> Поле, хранящее дату проведения события </summary>
        public DateTime Date { get; set; }

        /// <summary> Конструктор класса </summary>
        public EventData(Artist artist, Venue venue, string description, DateTime date)
        {
            this.Artist = artist;
            this.Venue = venue;
            // эти данные мы берём из места проведения события
            this.City = venue.city;
            this.Country = venue.country;
            this.EventName = venue.name;
            this.GeoPoint = new PointLatLng(Convert.ToDouble(venue.latitude, CultureInfo.InvariantCulture), // CultureInfo.InvariantCulture значит что при конвертации в тип double 
                                            Convert.ToDouble(venue.longitude, CultureInfo.InvariantCulture)); // как разделитель целой и десятичной части будет использоваться не только точка
            //////////////////////////////////////////////////
            this.Description = description;
            this.Date = date;
        }

        /// <summary> Метод для создания маркера события </summary>
        /// <returns> Возвращает Макрер события </returns>
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
                Position = this.GeoPoint // позиция на карте
            };

            return markerLocation;
        }

        /// <summary> Метод для нахождения расстояния между событием и переданной точкой </summary>
        /// <returns> Возвращает расстояние между событием и выбранной точкой </returns>
        public double GetDistance(PointLatLng point2)
        {
            // создаём точки класса GeoCoordinate находим расстояние между ними с помощью метода, который есть в этом классе
            GeoCoordinate c1 = new GeoCoordinate(GeoPoint.Lat, GeoPoint.Lng);
            GeoCoordinate c2 = new GeoCoordinate(point2.Lat, point2.Lng);

            double distance = c1.GetDistanceTo(c2);

            return distance;
        }

        /// <summary> Метод для получения полной локации, в которой будет проходить события </summary>
        /// <returns> Возвращает страну, город и название события, сшитые в одну строку </returns>
        public string GetLocation()
        {
            return this.Country + ", " + this.City + ", " + this.EventName;
        }
    }
}