using System;
using System.Collections.Generic;

namespace Euclidean_Algorithm_COMP_201_Assignment
{
    class Program
    {
        static int mod(int a, int b) {
         return (a-((a/b)*b)); //Eg 5mod3 = 5 - [(5/3)*3] = 5-(1*3)=2 
            //NB conventionally you'd say System.Math.Floor(a/b) instead of just (a/b) but because in C# this is automatically integer divsion, output is always an integer equivallent to the FLOOR value
        }


        static void GCDWhile(List<int> r,List<int> q, int a, int b) {
            while (b != 0) {
                r.Add(mod(a, b)); //add current remainder to  remainders list for use when solving
                q.Add(a / b); //add current quotient to quotients list for use later when solving
                              //a = r[r.Count - 2]; //new dividend is 1 place before latest remainder. Recall that indexing starts at 0 so 1 place before latest is total elements - 2. OR a=b
                a = b; //new dividend = old divisor
                b = r[r.Count - 1]; //new divisor = latest remainder in r list
            }

            Console.WriteLine("GCD using while loop gives: {0}", r[(r.Count) - 2]);
        }
        static int GCD(ref List<int> r, ref List<int> q, ref List<int> aList, ref List<int> bList, int a,int b) {

            aList.Add(a); //Add a to be used to list of a's for use later in solving linear equations
            bList.Add(b); //Add b to be used to list of b's for use later in solving linear equations
            r.Add(mod(a, b)); //add current remainder to  remainders list for use when solving
            q.Add(a / b); //add current quotient to quotients list for use later when solving
            //a = r[r.Count - 2]; //new dividend is 1 place before latest remainder. Recall that indexing starts at 0 so 1 place before latest is total elements - 2. OR a=b
            
            a = b; //new dividend = old divisor
            b = r[r.Count - 1]; //new divisor = latest remainder in r list
            if (b != 0) //if remainder is not 0 then continue doing EA
            {
                GCD(ref r, ref q, ref aList, ref bList, a, b); //recursive call
            }
      
            return r[(r.Count)-2]; //last stored r was zero, so return the r before that. -2 not -1 because C# index starts at 0 not 1
        }

        static void Main(string[] args)
        {
            int c;
            List<int> r = new List<int>();
            List<int> q = new List<int>();
            List<int> aList = new List<int>();
            List<int> bList = new List<int>();


            //Taking in a and b for gcd(a,b)
            Console.WriteLine("Enter a for gcd(a,b): ");
            int a = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Enter b for gcd(a,b): ");
            int b = Int32.Parse(Console.ReadLine());

            if (b>a) {
                //swap a and b if inputted b>a because order is important in mod function used
                int temp = a;
                a = b;
                b = temp;
            }

            if (mod(a, b) == 0) //if b is gcd
            {
                c = b;
                Console.WriteLine("GCD is {0}", c);
            }
            else {
                GCDWhile(r, q, a, b); //finding gcd using while loop
                r.Clear(); //to clear list and remove values entered during GCDWhile
                q.Clear();

                c = GCD(ref r, ref q, ref aList, ref bList, a, b); //pass r, and q by reference to GCD, then our a and b...this function uses recursion

                Console.WriteLine("\nGCD using recursion is: {0}", c);
            }

            //removing irrelevant last list row with r=0
            r.RemoveAt(r.Count-1);
            q.RemoveAt(q.Count-1);
            aList.RemoveAt(aList.Count - 1);
            bList.RemoveAt(bList.Count - 1);

            //reversing r, q, a and b lists so as to solve equations as if from bottom up
            r.Reverse();
            q.Reverse();
            aList.Reverse();
            bList.Reverse();

            Console.WriteLine("relevant steps are: {0}", r.Count);
            foreach (int i in r)
            {
                Console.WriteLine(" " + i);
            }


            int steps = q.Count;

            int coefficientA = 1;
            int coefficientB = -q[0];

            if (steps >= 2)
            {
                coefficientA = -q[0];
                coefficientB = (q[0] * q[1]) + 1;

                if (steps >= 3)
                {
                    for (int count = 2; count <= steps-1; count++)
                    {
                        coefficientA = coefficientB; //from sequence observed that when going down the equations, coefficient of a is equal to the previous equation's coefficient of b
                        coefficientB = (c - (coefficientA * aList[count])) / (bList[count]); //basic arithmatic: y = (c - ax) / b
                    }
                }

            }

            Console.WriteLine("Solution for ax + by = gcd(a,b)");
            Console.WriteLine("Equation: {0}x + {1}y = {2} is : ", a,b,c);
            Console.WriteLine("x = {0}    and    y = {1}",coefficientA,coefficientB);

            Console.ReadLine();
        }
    }
}


//DOCUMENTATION

//Appedix A - for use in explaining GCD process Take example of gcd(55,15)
/*1) 55/15 gives q0=3 and r0=10
    Set new a=b=15, and new b=r0=10;  

  2) 15/10 gives q1=1 and r1=5
    Set new a=b=10, and new b=r1=5

  3) 10/5 gives q2=2, and r2=0
    STOP. gcd is r1

 NOTE that in step 1 there were no previous 

*/
