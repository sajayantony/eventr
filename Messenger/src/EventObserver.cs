using System;
using System.Net.Http;
using System.Text;

namespace Messenger
{
    public class EventObserver : IObserver<ServiceEvent>
    {
        private string _endpoint;
        private HttpClient _client;

        public EventObserver(string endpoint)
        {
            _endpoint = endpoint;
            _client = new HttpClient();
        }

        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(ServiceEvent value)
        {
            //TODO: Handle all exception or persist and retry.
            var req = new HttpRequestMessage(HttpMethod.Put, _endpoint)
            {
                 Content = new StringContent(value.ToString(), Encoding.UTF8, "application/json")               
            };

            req.Headers.Add("EventR-EventType", value.Type);

            _client.SendAsync(req);
        }
    }
}
