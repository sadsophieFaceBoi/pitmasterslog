using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using BackendTypes;

namespace Backend_DB
{
    public class RecipeeDBService
    {
        private Container _container;

        public RecipeeDBService(
            CosmosClient dbClient,
            string databaseName,
            string containerName)
        {
            _container = dbClient.GetContainer(databaseName, containerName);
        }

        public async Task AddRecipeeAsync(Recipee recipee)
        {
            recipee.Id = Guid.NewGuid().ToString();
            recipee.CreatedAt = DateTime.UtcNow;
            recipee.UpdatedAt = DateTime.UtcNow;
            await _container.CreateItemAsync(recipee, new PartitionKey(recipee.Id));
        }

        public async Task<Recipee> GetRecipeeAsync(string id)
        {
            try
            {
                ItemResponse<Recipee> response = await _container.ReadItemAsync<Recipee>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Recipee>> GetRecipeesByIngredientAsync(string ingredientName)
        {
            var queryString = $"SELECT * FROM c WHERE ARRAY_CONTAINS(c.Ingredients, {{'Name': '{ingredientName}'}})";
            var query = _container.GetItemQueryIterator<Recipee>(new QueryDefinition(queryString));
            List<Recipee> results = new List<Recipee>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }

        public async Task UpdateRecipeeAsync(string id, Recipee recipee)
        {
            recipee.UpdatedAt = DateTime.UtcNow;
            await _container.UpsertItemAsync(recipee, new PartitionKey(id));
        }

        public async Task DeleteRecipeeAsync(string id)
        {
            await _container.DeleteItemAsync<Recipee>(id, new PartitionKey(id));
        }
    }
}
