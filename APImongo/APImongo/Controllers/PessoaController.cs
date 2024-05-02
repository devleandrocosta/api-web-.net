using APImongo.Data;
using APImongo.Entities;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using ZstdSharp.Unsafe;

namespace APImongo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoaController : ControllerBase
    {
        private readonly IMongoCollection<Pessoa>? _pessoas;
        public PessoaController(MongoDbService mongoDbService)
        {
            _pessoas = mongoDbService.Database?.GetCollection<Pessoa>("pessoa");
        }

        [HttpGet]
        public async Task<IEnumerable<Pessoa>> Read()
        {
            return await _pessoas.Find(FilterDefinition<Pessoa>.Empty).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pessoa?>> ReadId(string id)
        {
            var filter = Builders<Pessoa>.Filter.Eq(x => x.Id, id);
            var pessoa = _pessoas.Find(filter).FirstOrDefault();
            return pessoa is not null ? Ok(pessoa) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Pessoa pessoa)
        {
            await _pessoas.InsertOneAsync(pessoa);
            return CreatedAtAction(nameof(ReadId), new { id = pessoa.Id }, pessoa);
        }

        [HttpPut]
        public async Task<ActionResult> Update(Pessoa pessoa)
        {
            var filter = Builders<Pessoa>.Filter.Eq(x => x.Id, pessoa.Id);
            //var update = Builders<Pessoa>.Update
            //    .Set(x => x.Sexo, pessoa.Sexo)
            //    .Set(x => x.Telefone, pessoa.Telefone)
            //    .Set(x => x.Nome, pessoa.Nome);
            //await _pessoas.UpdateOneAsync(filter, update);

            await _pessoas.ReplaceOneAsync(filter, pessoa);
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(string id)
        {
            var filter = Builders<Pessoa>.Filter.Eq(x => x.Id, id);
            await _pessoas.DeleteOneAsync(filter);
            return Ok();
        }
    }
}
