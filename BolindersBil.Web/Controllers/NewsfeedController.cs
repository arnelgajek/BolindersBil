using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BolindersBil.Web.Controllers
{

    //NYT KEY: f9e4c9f7922b44828a235c984e930a3a


    [Route("api/[controller]")]
    [ApiController]
    public class NewsfeedController : ControllerBase
    {
        // GET: api/Newsfeed
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Newsfeed/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Newsfeed
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Newsfeed/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
