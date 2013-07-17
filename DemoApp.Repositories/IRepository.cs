using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DemoApp.Repositories
{
    public interface IRepository<T>
    {
        IQueryable<T> Get();
        T Create(T aEntity);
        T Save(T aEntity);
        void Delete(T aEntity);

        ITransaction BeginTran();
    }
    public interface ITransaction:IDisposable {
        void Commit();
        void Rollback();
    }
}