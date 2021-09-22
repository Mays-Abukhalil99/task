using Microsoft.AspNetCore.Mvc;
using onlineShop.Model;
using onlineShop.Resource;
using onlineShop.Services.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace onlineShop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InventoryController : ControllerBase
    {

        private readonly IInventoryService _IInventoryService;
        public InventoryController(IInventoryService iInventoryService)
        {
            _IInventoryService = iInventoryService;

        }
        //get all items
        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<InventoryResource>>>> GetAllAsync()
        {
            var response = await _IInventoryService.GetAll();
            return Ok(response);
        }
        //create new item
        [HttpPost]
        public async Task<ActionResult<List<ServiceResponse<InventoryResource>>>> CreateAsync(InventoryModel newItem)
        {
            return Ok(await _IInventoryService.AddItem(newItem));
        }
        //get item by id
        [HttpGet("(id)")]
        public async Task<ActionResult<ServiceResponse<InventoryResource>>> GetByIdAsync(int Id)
        {
            return Ok(await _IInventoryService.GetItemById(Id));
        }
        [HttpPut]
        public async Task<ActionResult<InventoryResource>> UpdateAsync(InventoryModel UpdateItem, int Id)
        {
            return Ok(await _IInventoryService.UpdateItem(UpdateItem, Id));
        }
        [HttpDelete("(id)")]
        public async Task<ActionResult<InventoryResource>> DeleteAsync( int Id)
        {
            return Ok(await _IInventoryService.DeleteItem(Id));
        }
    }
}
