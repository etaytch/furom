using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ForumSever;
using VS.Logger;
using System.Collections;

namespace WebForum {
    public class General {
        public static bool enabled = false;
        public static LogicManager lm;
        public static Database db;
        //public static string uName;
        public static Hashtable table;
        public static Object theLock;

        public General() {
            //Logger tmp = new Logger(2, "log.txt");            
            db = new Database(/*tmp*/);            
            lm = new LogicManager(db);
            //ForumServer.Start(lm/*, tmp*/);
            //table = new Hashtable();
            enabled = true;
        }

        public static void enable(){
            if (!enabled) {
                //Logger tmp = new Logger(2, "log.txt");
                db = new Database(/*tmp*/);
                lm = new LogicManager(db);
                //ForumServer.Start(lm/*, tmp*/);
                //table = new Hashtable();
                enabled = true;
            }
        }

        public static void setUsername(string username, string password) {
            lock (theLock) {

            }
        }
    }
}