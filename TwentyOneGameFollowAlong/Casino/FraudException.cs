using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casino
{
    public class FraudException : Exception // FraudException inherits from Exception as if we were creating a new exception
    {
        // creating two constructors, one overloading the other and having them implement the exact same  
        // implementation that exists for Exception
        public FraudException()
            : base() { }  // inherit from the base constructor
        public FraudException(string message) // overloading the constructor method
            : base(message) { } 
    }
}
