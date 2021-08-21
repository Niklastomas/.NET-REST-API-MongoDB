using System;
using System.Collections.Generic;
using System.Linq;
using _NET_REST_API_MongoDB.Models;

namespace _NET_REST_API_MongoDB.Repositories
{
    class ItemInMemoryRepository
    {
        private readonly List<Item> items = new()
        {
            new Item { Id = Guid.NewGuid(), Name = "Potion", CreatedAt = DateTimeOffset.UtcNow, Price = 9.5f },
            new Item { Id = Guid.NewGuid(), Name = "Sword", CreatedAt = DateTimeOffset.UtcNow, Price = 28.9f },
            new Item { Id = Guid.NewGuid(), Name = "Staff", CreatedAt = DateTimeOffset.UtcNow, Price = 15.5f }

        };

        public void CreateItemAsync(Item item)
        {
            items.Add(item);
        }

        public void DeleteItem(Guid id)
        {
            var item = items.Where(x => x.Id == id).FirstOrDefault();
            items.Remove(item);
        }

        public IEnumerable<Item> GetAll()
        {
            return items;
        }

        public Item GetItem(Guid id)
        {
            return items.Where(x => x.Id == id).FirstOrDefault();
        }

        public void UpdateItem(Item item)
        {
            var index = items.FindIndex(existingItem => existingItem.Id == item.Id);
            items[index] = item;

        }
    }
}