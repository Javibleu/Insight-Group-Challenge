using System.Drawing;

namespace Challenge
{
    /// <summary>
    /// WareHouse object represents a logistic distribution point with defined position, 
    /// index and a own color to differentiate paths
    /// </summary>
    public class Warehouse
    {    // fields declaration
        private Point point;

        public Point GetPoint()
        {
            return point;
        }

        public void SetPoint(Point value)
        {
            point = value;
        }

        public Color wHColor;
        public int index;

        public Warehouse(Point point, Color whColor, int index) // Constructor
        {
            this.SetPoint(point);
            this.wHColor = whColor;
            this.index = index;
        }
    } //End class

}
