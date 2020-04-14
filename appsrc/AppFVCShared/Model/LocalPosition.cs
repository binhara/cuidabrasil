using System;
using AppFVCShared.Services;
using Plugin.Geolocator.Abstractions;
using Xamarin.Essentials;

namespace AppFVCShared.Model
{
    public class LocalPosition : BaseModel
    {
        public Phone PhoneNumber { get; set; }
        public DateTime DateTime { get; set; }
        public Location Location { get; set; }
        public Position Position { get; set; }
    }
}
