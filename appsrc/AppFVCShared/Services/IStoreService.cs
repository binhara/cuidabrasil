using LiteDB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;

namespace AppFVCShared.Services
{
    interface IStoreService
    {
        void Store<T>(T data) where T : IBaseModel;
        void Remove<T>(params string[] ids) where T : IBaseModel;
        void RemoveAll<T>() where T : IBaseModel;
        void Store<T>(IEnumerable<T> data) where T : IBaseModel;
        void Reset<T>(IEnumerable<T> data) where T : IBaseModel;
        IEnumerable<T> FindAll<T>() where T : IBaseModel;
        IEnumerable<T> Find<T>(Expression<Func<T, bool>> predicate) where T : IBaseModel;
        T FindById<T>(string id) where T : IBaseModel;

        void RemoveDataBase();
    }
}
