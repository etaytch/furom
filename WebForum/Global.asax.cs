using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using ForumSever;
using VS.Logger;
using System.Net;


namespace WebForum {
    public class Global : System.Web.HttpApplication {
        public static LogicManager lm;
        public static Database db;
        public static string status;


        void Application_Start(object sender, EventArgs e) {
            Console.Out.Write("");
            IPAddress[] localIPs = Dns.GetHostAddresses(Dns.GetHostName());
            /* Code that runs on application startup
            status = "start";
            Logger tmp = new Logger(2, "log.txt");
            db = new Database(tmp);
            lm = new LogicManager(db);
            ForumServer.Start(lm,tmp);*/
        }

        void Application_End(object sender, EventArgs e) {
            //  Code that runs on application shutdown

        }

        void Application_Error(object sender, EventArgs e) {
            // Code that runs when an unhandled error occurs

        }

        void Session_Start(object sender, EventArgs e) {
            // Code that runs when a new session is started
            Console.Out.Write("");
        }

        void Session_End(object sender, EventArgs e) {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.

        }

    }
}
