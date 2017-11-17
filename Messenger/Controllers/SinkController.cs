using System;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Messenger.Controllers
{
    [Route("api/sink")]
    public class SinkController : Controller
    {        
        public SinkController()
        {            
        }

        // PUT api/values/5
        [HttpPut()]
        public void Publish([FromBody]JObject obj)
        {
            Console.Write("SINK: Received Event..." + Request.Headers["EventR-EventType"] + "\n\t");
            Console.WriteLine(obj.ToString());
        }
    }
}
