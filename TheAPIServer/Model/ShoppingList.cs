using System;
using System.Collections.Generic;

namespace TheAPIServer.Model
{
    public class ShoppingList : List<ShoppingListItem>
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
    }
}
