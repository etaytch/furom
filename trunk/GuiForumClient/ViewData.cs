using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataManagment
{
    public class ViewData
    {
        private String name;
        private int id;
        private bool selected;


        public ViewData()
        {
            this.name = "undefined";
            this.id = -1;
            this.selected = false;
        }

        public ViewData(string p_name,int p_id)
        {
            this.name = p_name;
            this.id = p_id;
            this.selected = false;
        }


        /************************
        * 
        *  getter and setter 
        *
        ************************/
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public bool Selected
        {
            get { return selected; }
            set { selected = value; }
        }

    
    
    
    
    }

    
  
}


