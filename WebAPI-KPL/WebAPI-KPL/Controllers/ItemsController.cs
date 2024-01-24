using Microsoft.AspNetCore.Mvc;
using WebAPI_KPL.Model;

namespace WebAPI_KPL.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemsController : ControllerBase
    {
        private static List<Item> _items = new List<Item>
        {
            new Item { Id = 1, Name = "Parfume", Description = "Eau de Toilette Men", Quantity = 20},
            new Item { Id = 2, Name = "Deodorant", Description = "Rexona Men", Quantity = 10},
            new Item { Id = 3, Name = "Chiki Balls", Description = "Chiki rasa Seaweed", Quantity = 62},
            new Item { Id = 4, Name = "Earphone", Description = "Salnotes Zero 2 x Crinacle", Quantity = 3}
        };

        [HttpGet]
        public IEnumerable<Item> GetItems()
        {
            return _items;
        }

        [HttpGet("{id}")]
        public ActionResult<Item> GetItem(int id) 
        {
            var item = _items.Find(i => i.Id == id);
            if (item == null)
                return NotFound();

            return item;
        }

        [HttpPost]
        public ActionResult<Item> PostItem(Item newItem) 
        {
            newItem.Id = _items.Count + 1;
            _items.Add(newItem);
            return CreatedAtAction(nameof(GetItem), new {id = newItem.Id}, newItem);
        }

        [HttpPut("{id}")]
        public IActionResult PutItem(int id, [FromBody] Item updatedItem) 
        {
            var existingItem = _items.Find(i => i.Id == id);
            if (existingItem == null)
                return NotFound();

            updatedItem.Id = existingItem.Id;
            existingItem = updatedItem;
            return Ok(existingItem);
        }

        [HttpDelete]
        public IActionResult DeleteItem(int id)
        {
            var itemToRemove = _items.Find(i => i.Id == id);
            if (itemToRemove == null)
                return NotFound();

            _items.Remove(itemToRemove);
            return NoContent();
        }
    }
}
