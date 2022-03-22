using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Data;

namespace videoclub_project.Backend.Servicios {

    /// <summary>
    /// Class that implements the data access interface.
    /// </summary>
    class ServicioGenerico<T> : IServicioGenerico<T> where T : class {
        /// <summary>
        /// Object that accesses the data access layer created by Entity Framework.
        /// </summary>
        protected DbContext _entities;

        /// <summary>
        /// Object that allows us to access the classes associated with the database tables.
        /// </summary>
        protected readonly IDbSet<T> _dbset;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="context">
        /// Object that allows us to access the classes associated to the database.
        /// </param>
        public ServicioGenerico(DbContext context) {
            _entities = context;
            _dbset = context.Set<T>();
        }

        /// <summary>
        /// Adds the entity to the database.
        /// Requires a commit to make the transaction persistent.
        /// </summary>
        /// <param name="entity">Generic type entity</param>
        /// <returns>Added entity</returns>
        public T add(T entity) {
            return _dbset.Add(entity);
        }
        /// <summary>
        /// Deletes the entity to the database.
        /// Requires a commit to make the transaction persistent.
        /// </summary>
        /// <param name="entity">Generic type entity</param>
        /// <returns>Deleted entity</returns>
        public T delete(T entity) {
            return _dbset.Remove(entity);
        }
        /// <summary>
        /// Returns a list of all objects in a database table
        /// </summary>
        public IEnumerable<T> getAll() {
            return _dbset.AsEnumerable<T>();
        }
        /// <summary>
        /// Commits the cache to the database
        /// </summary>
        public void save() {
            _entities.SaveChanges();
        }
        /// <summary>
        /// Returns an object identified by its id.
        /// </summary>
        /// <param name="id">identifier</param>
        public T findByID(int id) {
            return _dbset.Find(id);
        }
        /// <summary>
        /// Adds the entity to the database
        /// Requires a commit to make the transaction persistent
        /// </summary>
        /// <param name="entity">Generic type entity</param>
        public void edit(T entity) {
            _entities.Entry(entity).State = EntityState.Modified;
        }

        public IEnumerable<T> findBy(Expression<Func<T, bool>> predicate) {
            IEnumerable<T> query = _dbset.Where(predicate).AsEnumerable();
            return query;
        }
    }
}
