using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopBridge.Models;
using ShopBridge.Models.Dtos;
using ShopBridge.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private IInventoryRepository _ivRepo;
        private readonly IMapper _mapper;

        public InventoryController(IInventoryRepository repo, IMapper map)
        {
            this._ivRepo = repo;
            this._mapper = map;
        }

        [HttpPost]
        public IActionResult createInventory([FromBody] InventoryDto inventoryDto)
        {
            if (inventoryDto == null)
            {
                return BadRequest(ModelState);
            }

            if (_ivRepo.InventoryItemExists(inventoryDto.ItemID))
            {
                ModelState.AddModelError("", "Inventory Exists");
                return StatusCode(404, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var inventory = _mapper.Map<Inventory>(inventoryDto);
            if (!_ivRepo.AddItemToInventory(inventory))
            {
                ModelState.AddModelError("", $"Something went wrong");
                return StatusCode(500, ModelState);
            }
            //return CreatedAtRoute("GetInventoryItem", new { inventoryName = inventory.InventoryItems.Name}, inventory);
            return Ok();
        }

        [HttpGet("{inventoryId}", Name ="GetInventoryItem")]
        //[Route("/{inventoryName}")]
        public IActionResult GetInventoryItem(int inventoryId)
        {
            var item = _ivRepo.GetInventoryItem(inventoryId);
            if (item == null)
            {
                return NotFound();
            }
            var inventoryDto = _mapper.Map<InventoryDto>(item);
            return Ok(inventoryDto);
        }

        [HttpGet]
        public IActionResult GetAllInventoryItems()
        {
            var inventoryItems = _ivRepo.GetAllInventoryItems();
            var inventoryItemsDto = new List<InventoryDto>();
            foreach (var item in inventoryItems)
            {
                inventoryItemsDto.Add(_mapper.Map<InventoryDto>(item));
            }
            return Ok(inventoryItemsDto);
        }

        [HttpPatch("{inventoryId}", Name = "UpdateInventoryItem")]
        public IActionResult UpdateInventoryItem(int inventoryId, [FromBody] InventoryDto item)
        {
            if (item == null || item.ItemID!= inventoryId)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var invObj = _mapper.Map<Inventory>(item);
            if(!_ivRepo.UpdateItemInInventory(invObj))
            {
                ModelState.AddModelError("", $"Something went wrong when update the response");
                return StatusCode(500, ModelState);
            }
            return Content("Data created successfully");

        }

        [HttpDelete("{inventoryId}", Name = "DeleteInventoryItem")]
        public IActionResult DeleteInventoryItem(int inventoryId)
        {
            if(!_ivRepo.InventoryItemExists(inventoryId))
            {
                return NotFound();
            }

            var itemObj = _ivRepo.GetInventoryItem(inventoryId);
            if(!_ivRepo.DeleteItemFromInventory(itemObj))
            {
                ModelState.AddModelError("", $"Something went wrong while Deleting the recored");
                return StatusCode(500, ModelState);
            }

            return Content("Data Deleted Successfully");

        }
    }
}
