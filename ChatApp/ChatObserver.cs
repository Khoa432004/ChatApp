using ChatApp;
using System;
using System.Windows.Forms;

namespace ObserverChatting
{
    public class ChatObserver : IObserver
    {
        private FlowLayoutPanel flpMessages;
        private Action<int, string, string> onMessageEdited;

        public ChatObserver(FlowLayoutPanel flpMessages, Action<int, string, string> onMessageEdited)
        {
            this.flpMessages = flpMessages;
            this.onMessageEdited = onMessageEdited;
        }

        public void Update(int messageId, string message, string sender)
        {
            // Tìm tin nhắn cũ dựa trên messageId
            foreach (Control control in flpMessages.Controls)
            {
                if (control is UCMessage msgControl && msgControl.MessageId == messageId)
                {
                    msgControl.SetMessage(message, sender);
                    return;
                }
            }

            UCMessage newMsgControl = new UCMessage();
            newMsgControl.MessageId = messageId;
            newMsgControl.SetMessage(message, sender);
            newMsgControl.MessageEdited += (s, newMessage) => onMessageEdited?.Invoke(messageId, newMessage, sender);
            flpMessages.Controls.Add(newMsgControl);
        }
    }
}