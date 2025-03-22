using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp
{
    public interface ISubject
    {
        void RegisterObserver(IObserver observer);
        void RemoveObserver(IObserver observer);
        int NotifyObservers(string message, string sender);
        void UpdateMessage(int messageId, string newMessage, string sender);
    }
}
