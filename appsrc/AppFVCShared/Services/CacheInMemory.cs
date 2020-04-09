using AppFVCShared.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AppFVCShared.Services
{
    public class CacheInMemory
    {

        ////public static IAlertService alertService;

        //private static ObservableCollection<User> _users;
        //public ObservableCollection<User> Users
        //{
        //    get
        //    {
        //        return _users;
        //    }
        //}


        //private ICacheService _cache;
        //public CacheInMemory(ICacheService cache)
        //{
        //    // load data from cache
        //    //CacheService cache = new CacheService(DependencyService.Get<IStoreService>());
        //    //alertService = DependencyService.Get<IAlertService>();
        //    _cache = cache;

        //    if (_users == null) _users = new ObservableCollection<User>();


        //    cache.LoadNoticiaAsync();
        //    // _noticias = cache.GetListaNoticias();

        //    _users = cache.GetListaComissoes();
        //    if (_users.Count == 0)
        //    {
        //        cache.LoadComissaoAsync();
        //        _users = cache.GetListaComissoes();
        //    }

        //}



        //public ObservableCollection<Pastoral> GetListPastorais(Comissao com)
        //{
        //    ObservableCollection<Pastoral> pastorais = new ObservableCollection<Pastoral>();
        //    foreach (var p in this.Pastorais)
        //    {
        //        if (p.Comissao.Id == com.Id)
        //        {
        //            pastorais.Add(p);
        //        }
        //    }
        //    return pastorais;

        //}

        //public ObservableCollection<Pastoral> ListaPastorais(Comissao com)
        //{
        //    return _cache.GetListaPastorais(com);
        //}

    }


}

