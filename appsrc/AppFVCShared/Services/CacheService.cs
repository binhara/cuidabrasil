using AppFVCShared.Model;
using AppFVCShared.WebService;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFVCShared.Services
{
    public interface ICacheService
    {
        //Task<ObservableCollection<User>> GetListaUsers();
        ObservableCollection<User> GetListaUsers();
        Task LoadDataAsync(string date);

    }
    public class CacheService : BaseService, ICacheService
    {

        private readonly ClientSms objClient;
        private readonly Configuration objConfiguration;
        //readonly PastoralWS objPastoralWS;

        public CacheService(IStoreService storeService) : base(storeService)
        {
            objConfiguration = new Configuration();
            objClient = new ClientSms();
            //objPastoralWS = new PastoralWS(objClient, null);

        }
        #region LoadAll
        //public async Task LoadDataAsync()
        //{
        //    await LoadDataAsync();
        //}
        #endregion

        public ObservableCollection<User> GetListaUsers()
        {
            var data = _storeService.FindAll<User>().Select(s =>
                          new User
                          {
                              Id = s?.Id,
                              Name = s?.Name,
                              DddPhoneNumber = s?.DddPhoneNumber
                          }).ToArray();
            return new ObservableCollection<User>(data);
        }

        public async Task LoadDataAsync(string date)
        {


            //var response = await objComentarioWS.ListaComentarioByData(date, await SecureStorage.GetAsync("token"));
            //if (response != null)
            //{
            //_storeService.RemoveAll<User>();
            var user = new User();
            user.Name = "Maria";
            user.DddPhoneNumber = "5541996668442";
            var user1 = new User();
            user.Name = "Sergio";
            user.DddPhoneNumber = "5541996668442";
            ObservableCollection<User> response = new ObservableCollection<User>();
            ObservableCollection<Comorbidity> come = new ObservableCollection<Comorbidity>();
            ObservableCollection<Journal> jor = new ObservableCollection<Journal>();
            response.Add(user);
            var data = response.Select(s => new User
            {
                Id = s.Id.ToString(),
                Name = s.Name.ToString(),
                DddPhoneNumber = s.DddPhoneNumber.ToString()
            });
            _storeService.Store(data);
            //}

        }




    }
}
