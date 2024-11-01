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

    }
}
