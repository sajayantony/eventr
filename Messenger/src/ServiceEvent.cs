using Newtonsoft.Json.Linq;

namespace Messenger
{
    public struct ServiceEvent
    {
        private string _serialized;

        public string Type { get; set; }
        public JObject Payload { get; set; }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(_serialized))
            {
                _serialized = Payload.ToString();
            }

            return _serialized;
        }
    }
}
