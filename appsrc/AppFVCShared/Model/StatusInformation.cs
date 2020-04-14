using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AppFVCShared.Model
{
    public class StatusInformation
    {
        public string header_title { get; set; }
        public string header_body { get; set; }

        public ObservableCollection<News> news { get; set; }
    }
}
