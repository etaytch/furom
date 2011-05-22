using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebForum {
    public interface PageLoader {
        void update(string ip);
    }
}