namespace Neudesic.Core.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using Neudesic.Core.Models;
    using Newtonsoft.Json;

    /// <summary>
    /// User repository.
    /// </summary>
    public class UserRepository : IRepository<User>
    {
        private const string URI = "http://gmeinder.net/api/users";
        /// <summary>
        /// Create a new user.
        /// </summary>
        /// <param name="entity">Entity.</param>
        public async Task<User> CreateAsync(User entity)
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                    var stringContent = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
                    var response = await httpClient.PostAsync(URI, stringContent);
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<User>(content);
                }
                catch (Exception)
                {
                    // TODO: Add logging
                    throw;
                }
            }
        }

        /// <summary>
        /// Delete user by the specified id.
        /// </summary>
        /// <param name="id">Identifier.</param>
        public async Task DeleteAsync(string id)
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                    var deleteUrl = $"{URI}/{id}";
                    await httpClient.DeleteAsync(deleteUrl);
                }
                catch (Exception)
                {
                    // TODO: Add logging
                    throw;
                }
            }
        }

        /// <summary>
        /// Gets all users
        /// </summary>
        /// <returns>The collection of users.</returns>
        public async Task<IEnumerable<User>> GetAsync()
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                    var response = await httpClient.GetAsync(URI);
                    var content = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<IEnumerable<User>>(content);
                }
                catch (Exception)
                {
                    // TODO: Add logging
                    throw;
                }
            }
        }

        /// <summary>
        /// Gets the user by id.
        /// </summary>
        /// <returns>The <see cref="User"/></returns>
        /// <param name="id">Identifier.</param>
        public async Task<User> GetByIdAsync(string id)
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                    var getUri = $"{URI}/{id}";
                    var response = await httpClient.GetAsync(getUri);
                    var content = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<User>(content);
                }
                catch (Exception)
                {
                    // TODO: Add logging
                    throw;
                }
            }
        }

        /// <summary>
        /// Update the specified user.
        /// </summary>
        /// <param name="entity">Entity.</param>
        public async Task<User> UpdateAsync(User entity)
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                    var putUri = $"{URI}/{entity.Id}";
                    var stringContent = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
                    var response = await httpClient.PutAsync(URI, stringContent);
                    var content = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<User>(content);
                }
                catch (Exception)
                {
                    // TODO: Add logging
                    throw;
                }
            }
        }
    }
}