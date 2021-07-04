using _NET_REST_API_MongoDB.Dtos;
using _NET_REST_API_MongoDB.Models;

namespace _NET_REST_API_MongoDB.Extensions
{
    public static class ItemExtensions
    {
        public static ItemDto AsDto(this Item item)
        {
            return new ItemDto()
            {
                Id = item.Id,
                Name = item.Name,
                Price = item.Price,
                CreatedAt = item.CreatedAt,
                UpdatedAt = item.UpdatedAt
            };
        }

    }
}