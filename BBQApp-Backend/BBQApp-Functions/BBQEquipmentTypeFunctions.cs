using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using BackendDB;
using BackendTypes;

using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Cosmos;

public static class BBQEquipmentTypeFunctions
{
    private static readonly BBQEquipmentTypeService dbService = new BBQEquipmentTypeService(
        new CosmosClient("<your-cosmos-db-connection-string>"), 
        "BBQDatabase", 
        "BBQEquipmentTypeContainer");

    [Function("CreateBBQEquipmentType")]
    public static async Task<IActionResult> CreateBBQEquipmentType(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "bbqequipmenttype")] HttpRequest req,
        ILogger log)
    {
        log.LogInformation("C# HTTP trigger function processed a request to create a BBQEquipmentType.");

        string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        BBQEquipmentType equipmentType = JsonConvert.DeserializeObject<BBQEquipmentType>(requestBody);

        await dbService.CreateBBQEquipmentTypeAsync(equipmentType);

        return new OkObjectResult(equipmentType);
    }

    [Function("GetBBQEquipmentType")]
    public static async Task<IActionResult> GetBBQEquipmentType(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "bbqequipmenttype/{id}")] HttpRequest req,
        string id,
        ILogger log)
    {
        log.LogInformation($"C# HTTP trigger function processed a request to get a BBQEquipmentType with id: {id}.");

        var equipmentType = await dbService.GetBBQEquipmentTypeAsync(id);

        if (equipmentType == null)
        {
            return new NotFoundResult();
        }

        return new OkObjectResult(equipmentType);
    }

    [Function("GetAllBBQEquipmentTypes")]
    public static async Task<IActionResult> GetAllBBQEquipmentTypes(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "bbqequipmenttype")] HttpRequest req,
        ILogger log)
    {
        log.LogInformation("C# HTTP trigger function processed a request to get all BBQEquipmentTypes.");

        var equipmentTypes = await dbService.GetAllBBQEquipmentTypesAsync();

        return new OkObjectResult(equipmentTypes);
    }

    [Function("UpdateBBQEquipmentType")]
    public static async Task<IActionResult> UpdateBBQEquipmentType(
        [HttpTrigger(AuthorizationLevel.Function, "put", Route = "bbqequipmenttype/{id}")] HttpRequest req,
        string id,
        ILogger log)
    {
        log.LogInformation($"C# HTTP trigger function processed a request to update a BBQEquipmentType with id: {id}.");

        string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        BBQEquipmentType equipmentType = JsonConvert.DeserializeObject<BBQEquipmentType>(requestBody);

        await dbService.UpdateBBQEquipmentTypeAsync(id, equipmentType);

        return new OkObjectResult(equipmentType);
    }

    [Function("DeleteBBQEquipmentType")]
    public static async Task<IActionResult> DeleteBBQEquipmentType(
        [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "bbqequipmenttype/{id}")] HttpRequest req,
        string id,
        ILogger log)
    {
        log.LogInformation($"C# HTTP trigger function processed a request to delete a BBQEquipmentType with id: {id}.");

        await dbService.DeleteBBQEquipmentTypeAsync(id);

        return new OkResult();
    }
}
