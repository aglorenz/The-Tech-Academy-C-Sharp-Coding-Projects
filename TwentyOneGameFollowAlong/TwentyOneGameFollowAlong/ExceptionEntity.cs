using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwentyOneGameFollowAlong
{
    /*
     * Exeption Entity Class.  The class has only properites, and each property corresponds only to a column in 
     * the database.  That way  when you read from the database, you end up with a collection of objects
     * each of which represents a row in the database.  An entity is a word commonly used to refer to 
     * a database object.  When you refer to a class as an entity, then usually one would assume that 
     * class maps exactly to a database. Each property maps to a column in the database.
    */
    public class ExceptionEntity
    {
        public int Id { get; set; }
        public string ExceptionType { get; set; }
        public string ExceptionMessage { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
