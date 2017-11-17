using System;
using System.Linq;
using System.Reactive.Subjects;
using System.Reactive.Linq;

namespace Messenger
{
    //TODO: Implement guranteed delivery for all services. 
    public class Demuxer :IObserver<ServiceEvent>
    {
        Subject<ServiceEvent> _innerSink = new Subject<ServiceEvent>();

        public Demuxer(IObservable<EventSubscription> subs)
        {
            subs.Subscribe(e =>
            {
                _innerSink
                    .Where(i => i.Type == e.EventType)
                    .Do(evt => { Console.WriteLine("Received event" + evt.Type); })
                    .Subscribe(new EventObserver(e.Endpoint));
            });
        }

        public void OnCompleted()
        {
            ((IObserver<ServiceEvent>)_innerSink).OnCompleted();
        }

        public void OnError(Exception error)
        {
            ((IObserver<ServiceEvent>)_innerSink).OnError(error);
        }

        public void OnNext(ServiceEvent value)
        {
            ((IObserver<ServiceEvent>)_innerSink).OnNext(value);
        }
    }
}
