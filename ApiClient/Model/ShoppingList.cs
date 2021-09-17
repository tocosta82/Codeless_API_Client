using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiClient.Model
{
    public class ShoppingList : List<ShoppingListItem>
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }

        internal void Print()
        {
            Console.WriteLine("================================================================");
            ForEach(item =>
            {
                item.Print();
            });

            Console.WriteLine("================================================================");
        }
    }
}
