using System.Collections.ObjectModel;

namespace AppFVCShared.Model
{
    public class Comorbidity
    {
        public string Name { get; set; }
        public bool IsPositive { get; set; }

        public static ObservableCollection<Comorbidity> GetList()
        {
            var list = new ObservableCollection<Comorbidity>();
            list.Add(new Comorbidity { Name =  "Hipertensão"});
            list.Add(new Comorbidity { Name = "Asma" });
            list.Add(new Comorbidity { Name = "Diabetes" });
            list.Add(new Comorbidity { Name = "Fumante" });
            list.Add(new Comorbidity { Name = "Cardíaco" });
            list.Add(new Comorbidity { Name = "Insuficiência Renal" });

            return list;
        }
    }
}
