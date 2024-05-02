using System.Runtime.CompilerServices;
using MongoDB.Driver;

namespace APImongo.Data
{
    public class MongoDbService
    {
        private readonly IConfiguration _configuration;
        private readonly IMongoDatabase? _database;
        public MongoDbService(IConfiguration configuration)
        {
            _configuration = configuration;

            var connectionStrig = _configuration.GetConnectionString("DbConnection");
            var mongoUrl = MongoUrl.Create(connectionStrig);
            var mongoClient = new MongoClient(mongoUrl);
            _database = mongoClient.GetDatabase(mongoUrl.DatabaseName);
        }

        public IMongoDatabase? Database => _database;
    }
}