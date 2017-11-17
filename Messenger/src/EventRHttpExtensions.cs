using System;
using System.Net.Http;
using Microsoft.AspNetCore.Http;

namespace Messenger
{
    public static class EventRHttpExtensions
    {
        const string _eventTypeHeader = "EventR-Event";        

        public static void SetEvent(this HttpRequestMessage req, string type, string version = "v1.0")
        {
            if (string.IsNullOrEmpty(type))
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (string.IsNullOrEmpty(version))
            {
                throw new ArgumentNullException(nameof(version));
            }

            req.Headers.Add(_eventTypeHeader,type + "/" + version);
        }

        public static string GetEventType(this HttpRequest req)
        {
            if (req.Headers.ContainsKey(_eventTypeHeader))
            {
                return req.Headers[_eventTypeHeader];
            }

            return string.Empty;
        }
    }
}
