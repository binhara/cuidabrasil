using System;
using System.Collections.Generic;
using System.Text;

namespace AppFVCShared.Services
{
    public class BaseService
    {

        protected readonly IStoreService _storeService;

        [Xamarin.Forms.Internals.Preserve]
        public BaseService(IStoreService storeService)
        {
            _storeService = storeService;
        }

    }
}
