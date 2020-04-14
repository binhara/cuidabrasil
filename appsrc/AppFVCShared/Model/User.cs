using AppFVCShared.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Cache;
using System.Text;

namespace AppFVCShared.Model
{
    public class User : BaseModel
    {
        //public string Id { get; set; }
        public string DddPhoneNumber { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public int Age { get; set; }
        public DateTime CreateRecord { get; set; }
        public bool AcceptTerms { get; set; }

        public ObservableCollection<Comorbidity> Comorbidities { get; set; }
        public ObservableCollection<Journal> Journals { get; set; }

        public User()
        {
            Comorbidities =new ObservableCollection<Comorbidity>();
            Comorbidities = Comorbidity.GetList();

            Journals = new ObservableCollection<Journal>();
        }



    }
}
