using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataManagment
{
    public class ViewHirarchiData : ViewData
    {
        private List<ViewHirarchiData> children;

        public ViewHirarchiData() : base()
        {
            children = new List<ViewHirarchiData>();
        }

        public ViewHirarchiData(string p_name, int p_id) : base(p_name,p_id)
        {
            children = new List<ViewHirarchiData>();
        }

        public List<ViewHirarchiData> Children
        {
            get { return children; }
        }

        public void addChild(ViewHirarchiData p_child)
        {
            this.children.Add(p_child);
        }

        public ViewHirarchiData getChildAt(int index)
        {
            return this.children.ElementAt(index);
        }
    }
}
