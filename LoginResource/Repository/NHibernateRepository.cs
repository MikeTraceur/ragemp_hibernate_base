using LoginResource.Config;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoginResource
{
    public abstract class NHibernateRepository<T> where T : class
    {
        protected readonly ISessionBuilder mSessionBuilder;

        private ISession mSession;
        private ITransaction mTransaction;

        public NHibernateRepository()
        {
        }

        public NHibernateRepository(ISession Session, ITransaction Transaction)
        {
            mSession = Session;
            mTransaction = Transaction;
        }

        public T Retrieve(int id)
        {
            if (mSession == null)
            {
                ISession session = GetSession();

                return session.Get<T>(id);
            }
            else
            {
                return mSession.Get<T>(id);
            }
        }

        public void Save(T entity)
        {
            if (mSession == null)
            {
                ISession session = GetSession();

                session.SaveOrUpdate(entity);
            }
            else
            {
                mSession.SaveOrUpdate(entity);
            }
        }

        public void Delete(T entity)
        {
            if (mSession == null)
            {
                ISession session = GetSession();

                session.Delete(entity);
            }
            else
            {
                mSession.Delete(entity);
            }
        }

        public IQueryable<T> RetrieveAll()
        {
            if (mSession == null)
            {
                ISession session = GetSession();

                var query = from Item in session.Query<T>() select Item;

                return query;
            }
            else
            {
                var query = from Item in mSession.Query<T>() select Item;

                return query;
            }
        }

        protected virtual ISession GetSession()
        {
            return NHibernateHelper.GetCurrentSession();
        }
    }
}
