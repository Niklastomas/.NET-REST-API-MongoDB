using System;
using System.Collections.Generic;
using System.Linq;
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
        public IActionResult GetItems([FromQuery] PaginationFilter filter)
        {
            PaginationFilter validFilter = new(filter.PageNumber, filter.PageSize);
            var items = _itemRepository.GetAll()
                .Select(item => item.AsDto())
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize);


            if (items is null)
            {
                return NotFound();
            }

            return Ok(new PagedResponse<IEnumerable<ItemDto>>(items, validFilter.PageNumber, validFilter.PageSize));
        }

        [HttpGet("{id}")]
        public ActionResult<ItemDto> GetItem(Guid id)
        {
            var item = _itemRepository.GetItem(id);

            if (item is null)
            {
                return NotFound();
            }
            return Ok(item.AsDto());

        }

        [HttpPost]
        public ActionResult CreateItem(CreateItemDto itemDto)
        {
            Item item = new()
            {
                Id = Guid.NewGuid(),
                Name = itemDto.Name,
                Price = itemDto.Price,
                CreatedAt = DateTimeOffset.UtcNow
            };

            _itemRepository.CreateItem(item);

            return CreatedAtAction(nameof(GetItem), new { Id = item.Id }, item.AsDto());


        }

    }
}