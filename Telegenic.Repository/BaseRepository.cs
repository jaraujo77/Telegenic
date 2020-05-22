using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using Telegenic.Entities.Interfaces;

namespace Telegenic.Repository
{
    public abstract class BaseRepository<T> where T : IEntityBase
    {
        protected ISession _session;

        protected BaseRepository(ISession session)
        {
            if (session == null)
            {
                throw new ArgumentNullException("nHibernate-session");
            }

            this._session = session;
        }


        public virtual IEnumerable<T> GetAll()
        {
            var query = _session.Query<T>();
            return query.ToList<T>();
        }

        public virtual T GetById(int _id)
        {
            var query = _session.Load<T>(_id);
            return query;
        }

        public virtual bool Delete(IEntityBase entity)
        {
            using (var tx = _session.BeginTransaction())
            {
                try
                {
                    _session.Delete(entity);
                    tx.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    SetError(ex);
                    tx.Rollback();
                    return false;
                }
            }
        }

        public virtual void Save(IEntityBase entity)
        {
            using (var tx = _session.BeginTransaction())
            {
                try
                {
                    _session.SaveOrUpdate(entity);
                    tx.Commit();
                }
                catch (Exception ex)
                {
                    SetError(ex);
                    tx.Rollback();
                }
            }
        }

        public virtual string ErrorMessage { get; set; }

        public bool HasError { get; set; }

        public Exception Exception { get; set; }

        private void SetError(Exception ex)
        {
            this.HasError = true;
            this.ErrorMessage = ex.InnerException.Message;
            this.Exception = ex;
        }
    }
}
