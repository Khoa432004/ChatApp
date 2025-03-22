using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp
{
    public interface IObserver
    {
        void Update(int messageId, string message, string sender);
    }
}
