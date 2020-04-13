using LiteDB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;

namespace AppFVCShared.Services
{
    public interface IStoreService
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

    public class StoreService : IStoreService
    {
        readonly string _dataBaseFilePath;

        static object __lock = new object();

        [Xamarin.Forms.Internals.Preserve]
        public StoreService()
             => _dataBaseFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "database.db");

        public IEnumerable<T> FindAll<T>() where T : IBaseModel {
            lock (__lock) {
                using (var db = new LiteDatabase(_dataBaseFilePath)) {
                    return db.GetCollection<T>(typeof(T).Name).FindAll().ToList();
                }
            }
        }

        public void Store<T>(T data) where T : IBaseModel {
            lock (__lock) {
                using (var db = new LiteDatabase(_dataBaseFilePath)) {
                    var collection = db.GetCollection<T>(typeof(T).Name);
                    if (collection.Exists(w => w.Id == data.Id))
                        collection.Update(data);
                    else
                        collection.Insert(data);
                }
            }
        }

        public void Store<T>(IEnumerable<T> data) where T : IBaseModel {
            foreach (var item in data) {
                Store(item);
            }
        }

        public void RemoveAll<T>() where T : IBaseModel {
            lock (__lock) {
                using (var db = new LiteDatabase(_dataBaseFilePath)) {
                    db.DropCollection(typeof(T).Name);
                }
            }
        }

        public void Reset<T>(IEnumerable<T> data) where T : IBaseModel {
            lock (__lock) {
                using (var db = new LiteDatabase(_dataBaseFilePath)) {
                    var collection = db.GetCollection<T>(typeof(T).Name);
                    db.DropCollection(typeof(T).Name);
                    collection.Insert(data);
                }
            }
        }

        public T FindById<T>(string id) where T : IBaseModel {
            lock (__lock) {
                using (var db = new LiteDatabase(_dataBaseFilePath)) {
                    return db.GetCollection<T>(typeof(T).Name).FindById(id);
                }
            }
        }

        public IEnumerable<T> Find<T>(Expression<Func<T, bool>> predicate) where T : IBaseModel {
            lock (__lock) {
                using (var db = new LiteDatabase(_dataBaseFilePath)) {
                    return db.GetCollection<T>(typeof(T).Name).Find(predicate).ToList();
                }
            }
        }

        public void RemoveDataBase() {
            lock (__lock) {
                using (var db = new LiteDatabase(_dataBaseFilePath)) {
                    var collection = db.GetCollectionNames().ToList();
                    foreach (var item in collection) {
                        db.DropCollection(item);
                    }
                }
            }
        }

        public void Remove<T>(params string[] ids) where T : IBaseModel {
            if (ids?.Any() != true)
                return;

            lock (__lock) {
                using (var db = new LiteDatabase(_dataBaseFilePath)) {
                    var collection = db.GetCollection<T>(typeof(T).Name);
                    collection.Delete(w => ids.Contains(w.Id));
                }
            }
        }
    }
}