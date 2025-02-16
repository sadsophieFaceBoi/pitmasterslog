using System;
using System.Collections.Generic;

namespace Backend_DB
{
    public class Recipee
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public double CookTime { get; set; } // in minutes, will be conberted by UI
        public List<Ingredient> Ingredients { get; set; }
        public List<Instruction> Instructions { get; set; }
        public List<CookingEquipment> CookOn { get; set; }
        public List<TemperatureInstruction> TemperatureInstructions { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
    public class Ingredient
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string IngredientCategory { get; set; } = string.Empty;
    }
    public class RecipeIngredient
    {
        //this refers to the ingredient ID in the database
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; }
        public string Quantity { get; set; }
        public string Unit { get; set; } = string.Empty;
        public string Preparation { get; set; } = string.Empty;
    }

    public class Instruction
    {
        public int StepNumber { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public List<string> Tips { get; set; }
        public List<string> Warnings { get; set; }
        public List<string> Equipments { get; set; }

        public List<string> Ingredients { get; set; }
        public double Duration { get; set; } // in minutes
        //The ids of the equipments that this instruction applies to
        public List<string> ApplyOnlyForEquipments { get; set; }
        //This could be preperation,brining, cooking,serving, etc
        public string InstructionCategory { get; set; } = string.Empty;
    }
    public class Equipment
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; }
        public string Manufacturer { get; set; } = string.Empty;
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public List<string> ImageUrls { get; set; }
        public string EquipmentCategory { get; set; } = string.Empty;

    }
    public class CookingEquipment
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public List<string> ImageUrls { get; set; }
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
        public int StepNumber { get; set; }
    }
}
