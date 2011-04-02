using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MessagePack
{
    public class Message {

        public string _uName;
        
        //public string _uName;
        public Message(string uName) {            
            Console.WriteLine("uName: {0}",uName);
            this._uName = uName;
            Console.WriteLine("Massage Created!");
        }
        public virtual string getMessageType(){
            return "Message";
        }

    }
}
