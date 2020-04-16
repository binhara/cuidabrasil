//
// Journal.cs: Assignments.
//
// Author:
//      Alessandro de Oliveira Binhara (binhara@azuris.com.br)
//
// Dual licensed under the terms of the MIT or GNU GPL
//
// Copyright 2019-2020 Azuris Mobile & Cloud System
//
using AppFVCShared.Services;
using System;
using System.Collections.ObjectModel;

namespace AppFVCShared.Model
{
    public class User : BaseModel
    {

        public string DddPhoneNumber { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime CreateRecord { get; set; }
        public bool AcceptTerms { get; set; }
        public bool? ConditionRiskGroup { get; set; }

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
