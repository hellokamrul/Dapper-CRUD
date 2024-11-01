using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace DapperCRUD.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IConfiguration _config;

        public PersonController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet]
        public async Task<ActionResult<List<Persons>>> GetAllPersonList()
        {
            using var conncetion = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            var persons = await conncetion.QueryAsync<Persons>("select * from Person");
            return Ok(persons);
        }

        [HttpGet("personId")]
        public async Task<ActionResult<Persons>> GetPerson(int id)
        {
            using var conncetion = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            var person = await conncetion.QueryFirstAsync<Persons>("select * from Person where id =@Id",
                new { Id = id });
            return Ok(person);
        }

    }
}
