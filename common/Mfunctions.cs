using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace common //
{

    public class BasicMassage
    {
        string massage;
        int uid;

        public BasicMassage(int p_uid, string p_massage)
        {
            uid = p_uid;
            massage = p_massage;
        }

        public int Uid{ get {return uid;} set { uid = value;}}
        public string Massage{ get { return massage; } set { massage = value; } }
    
    }

    public class BlockingQueue<T>
    {
        private Queue<T> que = new Queue<T>();
        private Semaphore sem = new Semaphore(0, Int32.MaxValue);

        public void Enqueue(T item)
        {
            lock (que)
            
            {
                que.Enqueue(item);
            }

            sem.Release();
        }

        public T Dequeue()
        {
            sem.WaitOne();
            lock (que)
            {
                return que.Dequeue();
            }
          
        }
    }
}
