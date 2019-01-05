using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwentyOneGameFollowAlong
{
    // A class can inherit as many interfaces as it wants to, but it can only inherit ONE class.
    // Any class that inherits an interface must implement this method, take in said parm, and return void.
    interface IWalkAway  // Naming convention for interfaces is to start with a capital I
    {
        void WalkAway(Player player);  // can't declare as public because all interfaces are public

    }
}
