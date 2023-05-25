using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ProductManager
{
    public class ProductService
    {
        private readonly IMongoCollection<Product> _product;
        private readonly MongoDBConfig _settings;

        private static int GetNextId(IMongoCollection<Product> collection)
        {
            var filter = Builders<Product>.Filter.Empty;
            var sort = Builders<Product>.Sort.Descending(p => p.Id);
            var lastProduct = collection.Find(filter).Sort(sort).FirstOrDefault();
            return lastProduct == null ? 1 : int.Parse(lastProduct.Id) + 1;
        }

        public ProductService(IOptions<MongoDBConfig> settings)
        {
            _settings = settings.Value;
            var client = new MongoClient(_settings.ConnectionString);
            var database = client.GetDatabase(_settings.DatabaseName);
            _product = database.GetCollection<Product>(_settings.ProductCollectionName);
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _product.Find(c => true).ToListAsync(); 
        }

        public async Task<Product> CreateAsync(Product product)
        {
            product.Id = GetNextId(_product).ToString();

            await _product.InsertOneAsync(product);
            return product;
        }
    }
}
