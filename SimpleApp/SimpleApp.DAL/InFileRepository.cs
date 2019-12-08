using Newtonsoft.Json;
using SimpleApp.Core.Entities;
using SimpleApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleApp.DAL
{
    public class InFileRepository : IRepository
    {
        public int Create<T>(T entity) where T : BaseEntity
        {
            var collection = this.Deserialize<T>().ToList();
            entity.Id = collection.Count == 0 ? 1 :collection.Max(x => x.Id) + 1;
            collection.Add(entity);
            this.Serialize(collection);
            return entity.Id;
        }

        public bool Delete<T>(int id) where T : BaseEntity
        {
            var collection = this.Deserialize<T>().ToList();
            collection.RemoveAt(collection.FindIndex(it => it.Id == id));
            this.Serialize(collection);
            return true;
        }

        public T FirstOrDefault<T>(Predicate<T> predicate) where T : BaseEntity
        {
            var collection = this.Deserialize<T>();
            return collection.FirstOrDefault(x => predicate.Invoke(x));
        }

        public IEnumerable<T> GetAll<T>(Predicate<T> predicate) where T : BaseEntity
        {
            var collection = this.Deserialize<T>();
            return collection.Where(x => predicate.Invoke(x));
        }

        public IEnumerable<T> GetAll<T>() where T : BaseEntity
        {
            return this.GetAll<T>(x => true);
        }

        public bool Update<T>(T entity) where T : BaseEntity
        {
            var collection = this.Deserialize<T>().ToList();
            var itemIdx = collection.FindIndex(x => x.Id == entity.Id);
            collection.RemoveAt(itemIdx);
            collection.Insert(itemIdx, entity);
            this.Serialize(collection);
            return true;
        }

        private void Serialize<T>(IEnumerable<T> collection) where T : BaseEntity
        {
            using (FileStream fs = File.Create(new StringBuilder(typeof(T).Name).Append(".json").ToString()))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    var serializer = new JsonSerializer();
                    serializer.Serialize(sw, collection);
                }
            }
        }

        private IEnumerable<T> Deserialize<T>() where T : BaseEntity
        {
            try
            {
                using (StreamReader sr = File.OpenText(new StringBuilder(typeof(T).Name).Append(".json").ToString()))
                {
                    var serializer = new JsonSerializer();
                    return (IEnumerable<T>)serializer.Deserialize(sr, typeof(IEnumerable<T>));
                }
            }
            catch
            {
                return new List<T>();
            }
        }
    }
}
