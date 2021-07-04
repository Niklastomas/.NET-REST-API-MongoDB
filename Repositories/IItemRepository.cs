using System;
using System.Collections.Generic;
using _NET_REST_API_MongoDB.Models;

namespace _NET_REST_API_MongoDB.Repositories
{
    public interface IItemRepository
    {
        void CreateItem(Item item);
        Item GetItem(Guid id);
        IEnumerable<Item> GetAll();
        void DeleteItem(Guid id);
        void UpdateItem(Guid id, Item item);

    }
}