using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBus
{


    
    public class MessageBus<T>
    {
        private static MessageBus<T> _instance = null;
        private static readonly object _lock = new object();

        protected MessageBus()
        {
        }

        public static MessageBus<T> Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new MessageBus<T>();
                    }
                    return _instance;
                }
            }
        }

        public event EventHandler<MessageBusEventArgs<T>> MessageRecieved;

        public void SendMessage(object sender, T Message)
        {
            if (MessageRecieved != null)
            {
                MessageRecieved(sender, new MessageBusEventArgs<T>(Message));
            }
        }

    }

    public class MessageBusEventArgs<T> : EventArgs
    {
        private T _message;

        public MessageBusEventArgs(T Message)
        {
            _message = Message;
        }

        public T Message
        {
            get
            {
                return _message;
            }
        }
    }




}
