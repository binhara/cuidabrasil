//
// LocalPositions.cs: Assignments.
//
// Author:
//      Alessandro de Oliveira Binhara (binhara@azuris.com.br)
//
// Dual licensed under the terms of the MIT or GNU GPL
//
// Copyright 2019-2020 Azuris Mobile & Cloud System
//
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
