//
// Journal.cs: Assignments.
//
// Author:
//      Adriano D'Luca Binhara Gonçaves (adriano@azuris.com.br)
//
//
// Dual licensed under the terms of the MIT or GNU GPL
//
// Copyright 2019-2020 Azuris Mobile & Cloud System
//
using System.Collections.ObjectModel;

namespace AppFVCShared.Model
{
    public class StatusInformation
    {
        public string header_title { get; set; }
        public string header_body { get; set; }
        public string number_of_days { get; set; }

        public ObservableCollection<News> news { get; set; }
    }
}
