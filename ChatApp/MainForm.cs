using ChatApp;
using System;
using System.Windows.Forms;

namespace ObserverChatting
{
    public partial class mainChattingView : Form
    {
        private ChatSubject chatSubject;
        private ChatObserver observer1;
        private ChatObserver observer2;
        private ChatObserver observer3;
        public bool user1Key = true;
        public bool user2Key = true;
        public bool user3Key = true;

        public mainChattingView()
        {
            InitializeComponent();
            chatSubject = new ChatSubject();
            observer1 = new ChatObserver(flpMessage1, (id, newMsg, sender) => HandleMessageEdit(id, newMsg, sender, "User1"));
            observer2 = new ChatObserver(fplMessage2, (id, newMsg, sender) => HandleMessageEdit(id, newMsg, sender, "User2"));
            observer3 = new ChatObserver(flpMessage3, (id, newMsg, sender) => HandleMessageEdit(id, newMsg, sender, "User3"));
            if (user1Key) chatSubject.RegisterObserver(observer1);
            if (user2Key) chatSubject.RegisterObserver(observer2);
            if (user3Key) chatSubject.RegisterObserver(observer3);
            flpMessage1.Controls.Clear();
            fplMessage2.Controls.Clear();
            flpMessage3.Controls.Clear();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            btnSend1.Click += (object btnSender, EventArgs btnE) => SendMessage("User1", tbTyping1);
            btnSend2.Click += (object btnSender, EventArgs btnE) => SendMessage("User2", tbTyping2);
            btnSend3.Click += (object btnSender, EventArgs btnE) => SendMessage("User3", tbTyping3);
        }

        public void setQuantityMember()
        {
            int quantityMem = 0;
            if (user1Key) { quantityMem++; }
            if (user2Key) { quantityMem++; }
            if (user3Key) { quantityMem++; }
            if (quantityMem > 0 && quantityMem == 1)
            {
                lblQuantityMem1.Text = quantityMem.ToString() + " Member";
                lblQuantityMem2.Text = quantityMem.ToString() + " Member";
                lblQuantityMem3.Text = quantityMem.ToString() + " Member";
            }
            else if (quantityMem > 1)
            {
                lblQuantityMem1.Text = quantityMem.ToString() + " Members";
                lblQuantityMem2.Text = quantityMem.ToString() + " Members";
                lblQuantityMem3.Text = quantityMem.ToString() + " Members";
            }
        }

        private void btnOut1_Click(object sender, EventArgs e)
        {
            if (user1Key)
            {
                user1ChattingView.Visible = false;
                user1Key = false;
                btnOut1.Text = "Join";
                chatSubject.RemoveObserver(observer1);
            }
            else
            {
                user1ChattingView.Visible = true;
                user1Key = true;
                btnOut1.Text = "Out";
                chatSubject.RegisterObserver(observer1);
            }
            setQuantityMember();
            UpdateEditButtonVisibility("User1");
        }

        private void btnOut2_Click(object sender, EventArgs e)
        {
            if (user2Key)
            {
                user2ChattingView.Visible = false;
                user2Key = false;
                btnOut2.Text = "Join";
                chatSubject.RemoveObserver(observer2);
            }
            else
            {
                user2ChattingView.Visible = true;
                user2Key = true;
                btnOut2.Text = "Out";
                chatSubject.RegisterObserver(observer2);
            }
            setQuantityMember();
            UpdateEditButtonVisibility("User2");
        }

        private void btnOut3_Click(object sender, EventArgs e)
        {
            if (user3Key)
            {
                user3ChattingView.Visible = false;
                user3Key = false;
                btnOut3.Text = "Join";
                chatSubject.RemoveObserver(observer3);
            }
            else
            {
                user3ChattingView.Visible = true;
                user3Key = true;
                btnOut3.Text = "Out";
                chatSubject.RegisterObserver(observer3);
            }
            setQuantityMember();
            UpdateEditButtonVisibility("User3");
        }

        private void SendMessage(string sender, TextBox tbTyping)
        {
            string message = tbTyping.Text;
            if (!string.IsNullOrEmpty(message))
            {
                chatSubject.NotifyObservers(message, sender);
                tbTyping.Text = "";
                UpdateEditButtonVisibility(sender);
            }
        }

        private void HandleMessageEdit(int messageId, string newMessage, string sender, string currentUser)
        {
            if (sender == currentUser)
            {
                chatSubject.UpdateMessage(messageId, newMessage, sender);
                UpdateEditButtonVisibility(sender);
            }
            else
            {
                MessageBox.Show("You can only edit your own messages.");
            }
        }

        private void UpdateEditButtonVisibility(string currentUser)
        {
            foreach (Control control in flpMessage1.Controls)
            {
                if (control is UCMessage msg)
                {
                    msg.btnEdit.Visible = (msg.Sender == "User1" && user1Key);
                }
            }
            foreach (Control control in fplMessage2.Controls)
            {
                if (control is UCMessage msg)
                {
                    msg.btnEdit.Visible = (msg.Sender == "User2" && user2Key);
                }
            }
            foreach (Control control in flpMessage3.Controls)
            {
                if (control is UCMessage msg)
                {
                    msg.btnEdit.Visible = (msg.Sender == "User3" && user3Key);
                }
            }
        }
    }
}