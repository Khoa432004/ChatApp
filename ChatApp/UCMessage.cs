using System;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace ObserverChatting
{
    public partial class UCMessage : UserControl
    {
        public event EventHandler<string> MessageEdited;
        private string sender;
        private int messageId;

        public UCMessage()
        {
            InitializeComponent();
        }

        public int MessageId
        {
            get => messageId;
            set => messageId = value;
        }

        public void SetMessage(string message, string sender)
        {
            this.lblUserName.Text = sender + ":";
            this.lblMessage.Text = message;
            this.sender = sender;
            btnEdit.Visible = false;
        }

        public string Sender => sender;

        private void btnEdit_Click(object sender, EventArgs e)
        {
            string newMessage = Interaction.InputBox("Edit your message:", "Edit Message", lblMessage.Text);
            if (!string.IsNullOrEmpty(newMessage) && newMessage != lblMessage.Text)
            {
                lblMessage.Text = newMessage;
                MessageEdited?.Invoke(this, newMessage);
            }
        }
    }
}