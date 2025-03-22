using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp
{
    internal class ChatSubject : ISubject
    {
        private List<IObserver> observers = new List<IObserver>();
        private List<(int id, string message, string sender)> messages = new List<(int id, string message, string sender)>(); // Lưu ID, message, sender
        private int messageIdCounter = 0;

        public void RegisterObserver(IObserver observer)
        {
            observers.Add(observer);
        }

        public void RemoveObserver(IObserver observer)
        {
            observers.Remove(observer);
        }

        public int NotifyObservers(string message, string sender)
        {
            int messageId = messageIdCounter++;
            messages.Add((messageId, message, sender));
            foreach (var observer in observers)
            {
                observer.Update(messageId, message, sender);
            }
            return messageId;
        }

        public void UpdateMessage(int messageId, string newMessage, string sender)
        {
            var index = messages.FindIndex(m => m.id == messageId && m.sender == sender);
            if (index != -1)
            {
                messages[index] = (messageId, newMessage, sender);
                foreach (var observer in observers)
                {
                    observer.Update(messageId, newMessage, sender);
                }
            }
        }
    }
}