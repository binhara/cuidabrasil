using AppFVCShared.Services;
using System;
using Xamarin.Forms;
using Xunit;

namespace XUnitTestFvcWS
{
    public class UnitTest1
    {
        private ICacheService _cacheService;
        [Fact]
        public async System.Threading.Tasks.Task Test1Async()
        {
            //await _cacheService.LoadArquidioceseAsync().ConfigureAwait(false);
            _cacheService = new CacheService(DependencyService.Get<IStoreService>());
            await _cacheService.LoadDataAsync().ConfigureAwait(false); ;

            var u = _cacheService.GetListaUsers();

        }
    }
}
