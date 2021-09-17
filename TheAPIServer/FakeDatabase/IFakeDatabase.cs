using System.Threading.Tasks;
using TheAPIServer.Model;

namespace TheAPIServer.FakeDatabase
{
    public interface IFakeDatabase
    {
        Task<ShoppingList> AddItem(ShoppingListItem item);
        Task<ShoppingList> Get();
        Task<ShoppingList> Update(ShoppingListItem item);
        Task<ShoppingList> Deletetem(ShoppingListItem item);
    }
}
