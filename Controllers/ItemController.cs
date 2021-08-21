using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _NET_REST_API_MongoDB.Dtos;
using _NET_REST_API_MongoDB.Extensions;
using _NET_REST_API_MongoDB.Filters;
using _NET_REST_API_MongoDB.Models;
using _NET_REST_API_MongoDB.Repositories;
using _NET_REST_API_MongoDB.Wrappers;
using Microsoft.AspNetCore.Mvc;

namespace _NET_REST_API_MongoDB.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly IItemRepository _itemRepository;

        public ItemController(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetItems([FromQuery] PaginationFilter filter)
        {
            PaginationFilter validFilter = new(filter.PageNumber, filter.PageSize);
            var items = await _itemRepository.GetAllAsync();
            var itemsResponse = items.Select(item => item.AsDto())
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize);

            if (items is null)
            {
                return NotFound();
            }

            return Ok(new PagedResponse<IEnumerable<ItemDto>>(itemsResponse, validFilter.PageNumber, validFilter.PageSize));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetItem(Guid id)
        {
            var item = await _itemRepository.GetItemAsync(id);

            if (item is null)
            {
                return NotFound();
            }
            return Ok(item.AsDto());

        }

        [HttpPost]
        public async Task<IActionResult> CreateItem(CreateItemDto itemDto)
        {
            Item item = new()
            {
                Id = Guid.NewGuid(),
                Name = itemDto.Name,
                Price = itemDto.Price,
                CreatedAt = DateTimeOffset.UtcNow
            };

            await _itemRepository.CreateItemAsync(item);

            return CreatedAtAction(nameof(GetItem), new { Id = item.Id }, item.AsDto());

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(Guid id, UpdateItemDto itemDto)
        {
            var existingItem = await _itemRepository.GetItemAsync(id);

            if (existingItem is null)
            {
                return NotFound();
            }

            Item updatedItem = existingItem with
            {
                Name = itemDto.Name,
                Price = itemDto.Price
            };

            await _itemRepository.UpdateItemAsync(updatedItem);
            return NoContent();


        }

    }
}