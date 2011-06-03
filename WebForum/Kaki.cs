using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebForum
{
    public class Shnizale
    {
        private ArrayList<Shnizel> _readyShnizels;
        private int _money;
        

        void createShnizel(); //adds a new shnizel to the ready shnizels
        Shnizel getShnizel(int payment); //returns the oldest shnizel, removes it, and adds the customer's payment .
        bool isAGoodDay(); // returns true if money is above 300000$

        step 1:
        Shnizel getShnizel(int payment); //returns the oldest shnizel, removes it, and adds the customer's payment .
        to:
        //commands
        void addMoney(int payment); //adds the customer's payment.
        void removeShnizel(); //removes some shnizel
        //queries
        Shnizel getShnizel(); //returns the shnizel
        

        step 2 & 3:
        bool isAGoodDay(); // returns true if money is above 300000$
        to:
        int getMoney(); // returns money count;
        
        bool isAGoodDay(); // // returns true if getMoney is above 300000$
                ensure:
	                 money:
		                  Result =  (getMoney()> 3000000)    



        step 4:
        void addMoney(int payment); 
             ensure:
	                 money_changed:
		                  getMoney() =  getMoney()@pre + payment

        void removeShnizel(); //removes the oldest shnizel
            ensure:
	                     shnizel_count_changed:
		                      _readyShnizels.Count =  _readyShnizels.Count@pre - 1

        void createShnizel(); //adds a new shnizel to the ready shnizels
              ensure:
	                     shnizel_count_added:
		                      _readyShnizels.Count =  _readyShnizels.Count@pre + 1
        step 5:
        Shnizel getShnizel(); //returns the shnizel
            require: 
                    shnizel_count:
                               _readyShnizels.Count >0;

        int getMoney(); // returns money count;
                 require: true

        
        bool isAGoodDay(); // // returns true if getMoney is above 300000$
                require: true
                ensure:
	                 money:
		                  Result =  (getMoney()> 3000000)    


        void addMoney(int payment); 
             require: true
             ensure:
	                 money_changed:
		                  getMoney() =  getMoney()@pre + payment

        void removeShnizel(); //removes the oldest shnizel
            require: 
                    shnizel_count:
                               _readyShnizels.Count >0;
            ensure:
	                     shnizel_count_changed:
		                      _readyShnizels.Count =  _readyShnizels.Count@pre - 1

        void createShnizel(); //adds a new shnizel to the ready shnizels
                require: true
              ensure:
	                     shnizel_count_added:
		                      _readyShnizels.Count =  _readyShnizels.Count@pre + 1     

        invarients: 
            good balance:
                    getMoney()>0
            
    }
}