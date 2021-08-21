using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using _NET_REST_API_MongoDB.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace _NET_REST_API_MongoDB.Repositories
{
    public class MongoDbItemsRepository : IItemRepository
    {
        private const string databaseName = "REST-API-MongoDB";
        private const string collectionName = "items";
        private readonly IMongoCollection<Item> itemsCollection;
        private readonly FilterDefinitionBuilder<Item> filterBuilder = Builders<Item>.Filter;

        public MongoDbItemsRepository(IMongoClient mongoClient)
        {
            IMongoDatabase db = mongoClient.GetDatabase(databaseName);
            itemsCollection = db.GetCollection<Item>(collectionName);
        }
        public async Task CreateItemAsync(Item item)
        {
            await itemsCollection.InsertOneAsync(item);
        }

        public async Task DeleteItemAsync(Guid id)
        {
            var filter = filterBuilder.Eq(item => item.Id, id);
            await itemsCollection.DeleteOneAsync(filter);
        }

        public async Task<IEnumerable<Item>> GetAllAsync()
        {
            return await itemsCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<Item> GetItemAsync(Guid id)
        {
            var filter = filterBuilder.Eq(item => item.Id, id);
            return await itemsCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task UpdateItemAsync(Item item)
        {
            var filter = filterBuilder.Eq(item => item.Id, item.Id);
            await itemsCollection.ReplaceOneAsync(filter, item);
        }
    }
}