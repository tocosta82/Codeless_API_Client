using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TheAPIServer.FakeDatabase;
using TheAPIServer.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TheAPIServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingListController : ControllerBase
    {
        private IFakeDatabase _fakeDB;

        public ShoppingListController(IFakeDatabase database)
        {
            _fakeDB = database;
        }

        // GET: api/<ShopingListController>
        [HttpGet]
        public async Task<ShoppingList> Get()
        {
            return await _fakeDB.Get();
        }

        // POST api/<ShopingListController>
        [HttpPost]
        public async Task<ShoppingList> Post([FromBody] ShoppingListItem item)
        {
            return await _fakeDB.AddItem(item);
        }

        // PUT api/<ShopingListController>
        [HttpPut]
        public async Task<ShoppingList> Put([FromBody] ShoppingListItem item)
        {
            return await _fakeDB.Update(item);
        }

        // DELETE api/<ShopingListController>/5
        [HttpDelete("{id}")]
        public async Task<ShoppingList> Delete(Guid id)
        {
            return await _fakeDB.Deletetem(new ShoppingListItem() { Id = id });
        }
    }
}
