using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ForumSever;
using VS.Logger;
using System.Collections;
using System.Threading;
using System.Web.UI;

namespace WebForum {
    public class General {
        public static bool enabled = false;
        public static LogicManager lm;
        public static Database db;
        public static Hashtable table;
        public static Hashtable pages;        
        private static Object _refreshLock = new Object();

        public General() {
            //Logger tmp = new Logger(2, "log.txt");            
            db = new Database(/*tmp*/);            
            lm = new LogicManager(db);
            Thread t = new Thread(new ThreadStart(start));          // Kick off a new thread
            t.Start();
            //ForumServer.Start(lm/*, tmp*/);
            pages = new Hashtable();
            enabled = true;
            //refreshPages();
        }

        public static void start() {
            ForumServer.Start(lm);
        }

        public static void enable(){
            if (!enabled) {
                //Logger tmp = new Logger(2, "log.txt");
                db = new Database(/*tmp*/);
                lm = new LogicManager(db);
                Thread gt = new Thread(new ThreadStart(start));          // Kick off a new thread
                gt.Start();
                //ForumServer.Start(lm/*, tmp*/);
                pages = new Hashtable();
                enabled = true;
                Thread t = new Thread(new ThreadStart(refreshPages));
                t.Start();
            }
        }

        public static void setPage(string IP, PageLoader page) {
            lock (_refreshLock) {
                if (pages[IP] == null) {
                    pages.Add(IP, page);
                }
                else {
                    pages[IP] = page;
                }
            }
        }

        public static void refreshPages() {
            while (true) {             
                Thread.Sleep(100);
                lock (_refreshLock) {
                    IDictionaryEnumerator iter = pages.GetEnumerator();
                    DictionaryEntry di;
                    string ip;
                    PageLoader page;
                    while(iter.MoveNext()){
                        di = iter.Entry;
                        ip = (string)di.Key;
                        page = (PageLoader)di.Value;
                        page.update(ip);
                    }                
                }
            }
        }

    }
}