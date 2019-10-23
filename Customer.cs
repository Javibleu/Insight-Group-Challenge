using System;
using System.Collections.Generic;
using System.Drawing;

namespace Challenge
{
    /// <summary>
    /// Customer object represents a client point with defined position and index
    /// 
    /// </summary>
    public class Customer
    {

        public System.Drawing.Point point;                  // declares Point object
        private double distance = 10000;                    // Declares distance & assign a value to be compared 
        private int cstNumber;                              // index customer to be used to identify each customer
        private int closerWhouse = 0;                       // to determine closest warehouse 


        public Customer(Point point, int cstNumber)         // constructor
        {
            this.point = point;
            this.cstNumber = cstNumber;

        }
        /// <summary>
        /// Method to determine closest warehouse 
        /// </summary>
        /// <param name="wList"></param>
        /// <returns></returns>
        public Warehouse getDistance(List<Warehouse> wList)
        {

            foreach (Warehouse w in wList)                                              // iterates warehouse List
            {
                double deltaX = this.point.X - w.GetPoint().X;                               // get side length
                double deltaY = this.point.Y - w.GetPoint().Y;                               // get side length
                double answer = Math.Sqrt(Math.Pow(deltaX, 2) + Math.Pow(deltaY, 2));   // Pythagorean theorem
                answer = Math.Round(answer, 4);                                         // round result

                if (answer < distance)                                                  // if Wharehouse is closer than previous
                {
                    closerWhouse = w.index;                                             // keep warehose index
                    distance = answer;
                }
            }
            return wList[closerWhouse];
        }// end Method
    } // end class
}

