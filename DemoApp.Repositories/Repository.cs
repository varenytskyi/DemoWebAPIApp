using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Data.Objects;
using System.Data;

namespace DemoApp.Repositories
{
    public class Repository<T>: IRepository<T> where T: class
    {
        private ObjectContext _context;

        public Repository(ObjectContext aContext)
        {
            _context = aContext;
        }

        public IQueryable<T> Get()
        {
            return _context.CreateObjectSet<T>();
        }

        public T Create(T aEntity)
        {
            _context.CreateObjectSet<T>().AddObject(aEntity);
            _context.SaveChanges();
            return aEntity;
        }

        public T Save(T aEntity)
        {
            _context.SaveChanges();
            return aEntity;
        }

        public void Delete(T aEntity)
        {
            _context.DeleteObject(aEntity);
            _context.SaveChanges();
        }

        public ITransaction BeginTran()
        {
            if (_context.Connection.State != ConnectionState.Open)
            {
                _context.Connection.Open();
            }
            return new Transaction(_context.Connection.BeginTransaction());
        }
    }
    internal class Transaction : ITransaction {
        IDbTransaction _tran;

        public Transaction(IDbTransaction aTran)
        {
            _tran = aTran;
        }
        public void Commit()
        {
            _tran.Commit();
			_tran = null;
        }

        public void Rollback()
        {
			_tran.Rollback();
			_tran = null;
        }

        public void Dispose()
        {
			if (_tran != null)
				_tran.Rollback();
        }
    }
}