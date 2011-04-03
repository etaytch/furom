using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MessagePack
{
    public class Message {

        public string _uName;
        
        //public string _uName;
        public Message(string uName) 
        {            
            this._uName = uName;
        }
        public virtual string getMessageType()
        {
            return "Message";
        }

    }
}
