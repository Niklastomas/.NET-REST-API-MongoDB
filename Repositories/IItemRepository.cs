using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using _NET_REST_API_MongoDB.Models;

namespace _NET_REST_API_MongoDB.Repositories
{
    public interface IItemRepository
    {
        Task CreateItemAsync(Item item);
        Task<Item> GetItemAsync(Guid id);
        Task<IEnumerable<Item>> GetAllAsync();
        Task DeleteItemAsync(Guid id);
        Task UpdateItemAsync(Item item);

    }
}