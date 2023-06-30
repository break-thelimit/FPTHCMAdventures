using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Model;
using DataAccess.Repositories;
using Microsoft.Extensions.Configuration;

namespace FPTHCMAdventuresAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        IITemRepository itemRepository = new ItemRepository();
        private readonly IConfiguration Configuration;

        public ItemsController(IConfiguration config)
        {
            Configuration = config;
        }

        // GET: api/Items
        [HttpGet]
        public JsonResult GetItems()
        {
            IEnumerable<Item> list = itemRepository.GetItems();
            return new JsonResult(new
            {
                result = list
            });
        }

        // GET: api/Items/5
        [HttpGet("getitem/{itemid}")]
        public JsonResult GetItem(Guid id)
        {
            Item item = itemRepository.GetItemByID(id);
            return new JsonResult(new
            {
                result = item
            });
        }

        // PUT: api/Items/5
        [HttpPut]
        public JsonResult UpdateItem([FromForm] Item item)
        {
            Item tmp = itemRepository.UpdateItem(item);
            if (tmp != null)
            {
                return new JsonResult(new
                {
                    status = 200, // updated successfully!
                    item_id = tmp.Id,
                    message = "Updated successfully!"
                });
            }
            else
            {
                return new JsonResult(new
                {
                    status = 406, // updated failed with invalid input value!
                    message = "This item hasn't existed in system!"
                });
            }
        }

        // POST: api/Items
        [HttpPost]
        public JsonResult CreateItem([FromForm] Item item)
        {
            Item tmp = itemRepository.CreateItem(item);
            if (tmp != null)
            {
                return new JsonResult(new
                {
                    status = 200, // created successfully!
                    item_id = tmp.Id,
                    message = "Created successfully!"
                });
            }

            else
            {
                return new JsonResult(new
                {
                    status = 406, // created failed with invalid input value!
                    message = "This item has existed in system!"
                });
            }
        }
    }
}
