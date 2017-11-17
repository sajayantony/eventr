using System;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Messenger.Controllers
{
    [Route("api/event")]
    public class EventController : Controller
    {
        private IObserver<ServiceEvent> _events;

        public EventController(IObserver<ServiceEvent> events)
        {
            _events = events;
        }
        
        [HttpPost()]
        public void Publish([FromBody]JObject obj)
        {
            var type = Request.GetEventType();
            _events.OnNext(new ServiceEvent {
                 Type = type,
                 Payload = obj
            });
        }
    }
}
