using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using BackendTypes;

namespace BackendDB
{
    public class BBQEquipmentTypeService
    {
        private readonly CosmosClient _cosmosClient;
        private readonly string databaseName;
        private  Container _container;
        string _containerName = "BBQEquipmentType";
     
        public BBQEquipmentTypeService(CosmosClient cosmosClient, string databaseName)
        {
          
            _cosmosClient = cosmosClient;
            this.databaseName = databaseName;
            var db = _cosmosClient.GetDatabase(databaseName);
            _container = db.GetContainer(_containerName);
        }
 
        public async Task<BBQEquipmentType> CreateBBQEquipmentTypeAsync(BBQEquipmentType equipmentType)
        {
            equipmentType.id = Guid.NewGuid().ToString();
            try {
                ItemResponse<BBQEquipmentType> response = await _container.CreateItemAsync(equipmentType);
                return response.Resource;
            }
            catch(Exception e)
            {
                return null;
            }
           
        }

        public async Task<BBQEquipmentType> GetBBQEquipmentTypeAsync(string id)
        {
            try
            {
      
                ItemResponse<BBQEquipmentType> response = await _container.ReadItemAsync<BBQEquipmentType>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<IEnumerable<BBQEquipmentType>> GetAllBBQEquipmentTypesAsync()
        {
           
            var query = _container.GetItemQueryIterator<BBQEquipmentType>(new QueryDefinition("SELECT * FROM c"));
            List<BBQEquipmentType> results = new List<BBQEquipmentType>();
            while (query.HasMoreResults)
            {
                FeedResponse<BBQEquipmentType> response = await query.ReadNextAsync();
                results.AddRange(response);
            }
            return results;
        }

        public async Task<BBQEquipmentType> UpdateBBQEquipmentTypeAsync(string id, BBQEquipmentType equipmentType)
        {
         
            ItemResponse<BBQEquipmentType> response = await _container.UpsertItemAsync(equipmentType, new PartitionKey(id));
            return response.Resource;
        }

        public async Task DeleteBBQEquipmentTypeAsync(string id)
        {
           
            await _container.DeleteItemAsync<BBQEquipmentType>(id, new PartitionKey(id));
        }
    }
}
