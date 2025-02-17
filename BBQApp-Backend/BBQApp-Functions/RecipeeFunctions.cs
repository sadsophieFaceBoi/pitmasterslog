using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Backend_DB;
using Backend_Types;

using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Cosmos;
using BackendTypes; // Add this using directive to resolve the CosmosClient type
public static class RecipeeFunctions
{
    private static readonly RecipeeDBService dbService = new RecipeeDBService(
        new CosmosClient("<your-cosmos-db-connection-string>"), 

        "RecipeeDatabase", 
        "RecipeeContainer");

    [Function("CreateRecipee")]
    public static async Task<IActionResult> CreateRecipee(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "recipee")] HttpRequest req,
        ILogger log)
    {
        log.LogInformation("C# HTTP trigger function processed a request to create a recipee.");

        string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        Recipee recipee = JsonConvert.DeserializeObject<Recipee>(requestBody);

        await dbService.AddRecipeeAsync(recipee);

        return new OkObjectResult(recipee);
    }

    [Function("GetRecipee")]
    public static async Task<IActionResult> GetRecipee(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "recipee/{id}")] HttpRequest req,
        string id,
        ILogger log)
    {
        log.LogInformation($"C# HTTP trigger function processed a request to get a recipee with id: {id}.");

        var recipee = await dbService.GetRecipeeAsync(id);

        if (recipee == null)
        {
            return new NotFoundResult();
        }

        return new OkObjectResult(recipee);
    }

    [Function("GetRecipeesByIngredient")]
    public static async Task<IActionResult> GetRecipeesByIngredient(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "recipee/ingredient/{ingredientName}")] HttpRequest req,
        string ingredientName,
        ILogger log)
    {
        log.LogInformation($"C# HTTP trigger function processed a request to get recipees with ingredient: {ingredientName}.");

        var recipees = await dbService.GetRecipeesByIngredientAsync(ingredientName);

        return new OkObjectResult(recipees);
    }

    [Function("UpdateRecipee")]
    public static async Task<IActionResult> UpdateRecipee(
        [HttpTrigger(AuthorizationLevel.Function, "put", Route = "recipee/{id}")] HttpRequest req,
        string id,
        ILogger log)
    {
        log.LogInformation($"C# HTTP trigger function processed a request to update a recipee with id: {id}.");

        string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        Recipee recipee = JsonConvert.DeserializeObject<Recipee>(requestBody);

        await dbService.UpdateRecipeeAsync(id, recipee);

        return new OkObjectResult(recipee);
    }

    [Function("DeleteRecipee")]
    public static async Task<IActionResult> DeleteRecipee(
        [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "recipee/{id}")] HttpRequest req,
        string id,
        ILogger log)
    {
        log.LogInformation($"C# HTTP trigger function processed a request to delete a recipee with id: {id}.");

        await dbService.DeleteRecipeeAsync(id);

        return new OkResult();
    }
}
