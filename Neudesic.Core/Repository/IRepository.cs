namespace Neudesic.Core.Repository
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Neudesic.Core.Models;

    /// <summary>
    /// The IRepository interface
    /// </summary>
    public interface IRepository<T> where T : EntityBase

    {
        /// <summary>
        /// Get All
        /// </summary>
        /// <returns>The get.</returns>
        Task<IEnumerable<T>> GetAsync();

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <returns>The by identifier.</returns>
        /// <param name="id">Identifier.</param>
        Task<T> GetByIdAsync(string id);

        /// <summary>
        /// Create the specified entity.
        /// </summary>
        /// <returns>The create.</returns>
        /// <param name="entity">Entity.</param>
        Task<T> CreateAsync(T entity);

        /// <summary>
        /// Delete the specified id.
        /// </summary>
        /// <returns>The delete.</returns>
        /// <param name="id">Identifier.</param>
        Task DeleteAsync(string id);

        /// <summary>
        /// Update the specified entity.
        /// </summary>
        /// <returns>The update.</returns>
        /// <param name="entity">Entity.</param>
        Task<T> UpdateAsync(T entity);
    }
}