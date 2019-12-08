using SimpleApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleApp.Core.Interfaces
{
    public interface IRepository
    {
        IEnumerable<T> GetAll<T>(Predicate<T> predicate) where T : BaseEntity;

        IEnumerable<T> GetAll<T>() where T : BaseEntity;

        T FirstOrDefault<T>(Predicate<T> predicate) where T : BaseEntity;

        int Create<T>(T entity) where T : BaseEntity;

        bool Delete<T>(int id) where T : BaseEntity;

        bool Update<T>(T entity) where T : BaseEntity;
    }
}
