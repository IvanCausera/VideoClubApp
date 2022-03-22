using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace videoclub_project.Backend.Servicios {

    /// <summary>
    /// Interface that shows the main operations to be performed with the database objects.
    /// </summary>
    interface IServicioGenerico<T> where T: class {

        /// <summary>
        /// Gets all objects of a given entity
        /// </summary>

        IEnumerable<T> getAll();

        /// <summary>
        /// Searches for elements according to the expression or predicate passed as parameter
        /// </summary>
        IEnumerable<T> findBy(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Searches for an object by its identifier
        /// </summary>
        T findByID(int id);

        /// <summary>
        /// Insert a new object in the database.
        /// Then a commit must be performed to make the changes persistent.
        /// </summary>
        T add(T entity);

        /// <summary>
        /// Deletes an object from the database based on its id.
        /// Then a commit must be performed to make the changes persistent.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        T delete(T entity);

        /// <summary>
        /// Edit a database object.
        /// Then a commit must be performed to make the changes persistent.
        /// </summary>
        void edit(T entity);

        /// <summary>
        /// Realiza un commit para que los cambios se hagan persistentes
        /// </summary>
        void save();
    }
}
