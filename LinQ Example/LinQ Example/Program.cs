using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinQ_Example
{
    class Program
    {
        static void Main(string[] args)
        {
            //1)  Specify the dta source.
            //string[] students = new string[]  {"Lacey","Trisha", "Gavin","Josh","Jon","Landon","Kyndreshia"};
            string[] students = new string[]  {"Lacey","Trisha", "Gavin","Josh","Jon","Landon","Kyndreshia"};

            //Define the query expression.
            IEnumerable<string> studentQuery =
                from student in students
                where student.Length < 4 // only Jon is returned
                select student;

            //execute the query
            foreach (string s in studentQuery) // studentQuery holds the results of the query
            {
                Console.WriteLine(s + " Length = " + s.Length);
            }
            Console.ReadLine();
        }
    }
}
