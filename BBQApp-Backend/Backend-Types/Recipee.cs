using System;
using System.Collections.Generic;

namespace BackendTypes
{
    public class Recipee
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double CookTime { get; set; } // in minutes, will be conberted by UI
        public List<RecipeIngredient> Ingredients { get; set; }
        public List<Instruction> Instructions { get; set; }
        public List<string> BBQTypeIds { get; set; }
 
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string CreatorId { get; set; }
        public List<string> AdminIds { get; set; } = new List<string>();
        public List<string> ApproverIds { get; set; } = new List<string>();
        public List<string> Tags { get; set; } = new List<string>();
    }
    public class Ingredient
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string IngredientCategory { get; set; } = string.Empty;
        public List<string> ALternativeNames { get; set; }=new List<string>();
    }
    public class RecipeIngredient
    {
        //this refers to the ingredient ID in the database
     
        public string Name { get; set; }
        public string Quantity { get; set; }
        public string Unit { get; set; } = string.Empty;
        public string Preparation { get; set; } = string.Empty;
    }

    public class Instruction
    {
        public InstructionDetails Details { get; set; }
        public List<InstructionVariation> Variations { get; set; } = new List<InstructionVariation>();
        //This could be preperation,brining, cooking,serving, etc
        public string InstructionCategory { get; set; } = string.Empty;
    }
    public class InstructionVariation
    {
        public InstructionDetails Details { get; set; }
        public List<string> ValidForEquipments { get; set; } = new List<string>();
    }
    public class InstructionDetails
    {
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public List<string> Tips { get; set; } = new List<string>();
        public List<string> Warnings { get; set; } = new List<string>();
        public List<string> Equipments { get; set; } = new List<string>();

        public List<string> Ingredients { get; set; } = new List<string>();
        public double Duration { get; set; } // in minutes
        public TemperatureInstruction TemperatureInstruction { get; set; }
    }
    public class Equipment
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; }
        public string Manufacturer { get; set; } = string.Empty;
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public List<string> ImageUrls { get; set; } = new List<string>();
        public string EquipmentCategory { get; set; } = string.Empty;

    }
    public class BBQEquipmentType
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; }
    
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public List<string> ImageUrls { get; set; } = new List<string>();
        public List<CookType> CookTypes { get; set; }= new List<CookType>();
    }
    public class CookingEquipment
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public List<string> ImageUrls { get; set; } = new List<string>();
        public List<CookType> CookTypes { get; set; }
    }
    public enum CookType
    {
        Grill,
        Hot_Smoke,
        Cold_Smoke,
        Bake,
        Roast,
        Fry,
        Boil,
        Steam,
        Saute,
        SousVide,
        SlowCook,
        PressureCook,
        Microwave,
        Other
    }

    public class TemperatureInstruction
    {
        public string Description { get; set; } = string.Empty;

        public double Value { get; set; }
        public string Unit { get; set; } // e.g., Celsius, Fahrenheit
       
    }
}
