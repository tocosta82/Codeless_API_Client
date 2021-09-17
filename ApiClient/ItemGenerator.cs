
using ApiClient.Model;
using System.Collections.Generic;

namespace ApiClient
{
    public static class ItemGenerator
    {
        public static IEnumerable<ShoppingListItem> Generate()
        {
            yield return new ShoppingListItem()
            {
                Name = "Banana",
                Quantity = 5,
                UnitName = "Units",
                Unit = "",
                IsBought = false
            };

            yield return new ShoppingListItem()
            {
                Name = "Apple",
                Quantity = 5,
                UnitName = "Units",
                Unit = "",
                IsBought = false
            };

            yield return new ShoppingListItem()
            {
                Name = "Letuce",
                Quantity = 2,
                UnitName = "Units",
                Unit = "",
                IsBought = false
            };

            yield return new ShoppingListItem()
            {
                Name = "Rice",
                Quantity = 2,
                UnitName = "Kilogram",
                Unit = "Kg",
                IsBought = false
            };

            yield return new ShoppingListItem()
            {
                Name = "Milk",
                Quantity = 2,
                UnitName = "Pint",
                Unit = "pt",
                IsBought = false
            };

            yield return new ShoppingListItem()
            {
                Name = "Bread",
                Quantity = 5,
                UnitName = "Units",
                Unit = "",
                IsBought = false
            };
        }
    }
}
