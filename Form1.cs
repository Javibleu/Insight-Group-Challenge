using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Challenge
{
    public partial class Form1 : Form
    {

        private Random rand = new Random();
        Point[] customers = new Point[100];
        Point[] wHouse = new Point[5];
        public List<Warehouse> wHouseList = new List<Warehouse>();
        static List<Customer> customerList = new List<Customer>();
        Pen myPen = new Pen(Brushes.Red);


        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Randomize click event, get values from Text boxes, generates Warehouse & clients Objects with random coordenates.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRandom(object sender, EventArgs e)
        {
            int customerNumber = 100;
            int whouseNumber = 5;
            Graphics g = Graphics.FromHwnd(canvas.Handle);                  // instance Graphic Object
            canvas.Refresh();                                           // clear previous draws 

            try
            {
                customerNumber = Convert.ToInt32(txtCustomer.Text);     // get customer input
                whouseNumber = Convert.ToInt32(txtWhouse.Text);         // get Warehouse input

                if (customerNumber > 100)
                {
                    customerNumber = 100;
                    txtCustomer.Text = "100";
                    MessageBox.Show("Max customer amount reached, run calculation on 100");
                }

                if (whouseNumber > 10)
                {

                    whouseNumber = 10;
                    MessageBox.Show("Max warehouses amount reached, run calculation on 10");
                    txtWhouse.Text = "10";
                }


                if (customerNumber <= 0 || whouseNumber <= 0)           // validate input
                {
                    throw new System.Exception();                       // if 0 or negative throw exception
                }

                int hSpan = canvas.Width;                                   // get canvas width property
                int vSpan = canvas.Height;                                  // get canvas height property

                customerList.Clear();                                       // empty customers ArrayList
                wHouseList.Clear();                                         // empty customers ArrayList

                for (int i = 0; i < whouseNumber; i++)                      // creates random position Warehouses
                {
                    // Creates anew Random color to assign Color to warehouse path draw
                    Color whColor = Color.FromArgb(rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255));
                    // instances Warecouse
                    wHouseList.Add(new Warehouse(new Point(rand.Next(hSpan), rand.Next(vSpan)), whColor, i));
                }

                for (int i = 0; i < customerNumber; i++)                    // create random position customers
                {
                    // Instances customers
                    customerList.Add(new Customer(new Point(rand.Next(hSpan), rand.Next(vSpan)), i));
                }

                drawCoordinates(g);                                          // Draw clients & wh onover map

            }
            catch (Exception)                                           // catch Exception in case of text, 0 empty or negative numbers
            {
                string message = "Values cannot be 0, empty or negative values. Please fill them again";
                string caption = "Error";
                MessageBox.Show(message, caption);

            }

        }// end Randomize Method



        /// <summary>
        /// Draw Warehouses & Customers on the map
        /// /// </summary>
        /// <param name="g"></param>
        public void drawCoordinates(Graphics g)
        {
            int whdiameter = 18;                                            //defines warehouse dot diameter 
            int cmdiameter = 5;                                             //defines customer dot diameter

            foreach (Warehouse wh in wHouseList)                            // Draws WareHouses
            {
                int whMapPosX = wh.GetPoint().X - whdiameter / 2;                // Center dot in graphic
                int whMapPosY = wh.GetPoint().Y - whdiameter / 2;                // Center dot Graphic

                // draws dot on map & write index number on it
                g.FillEllipse(Brushes.Gray, whMapPosX, whMapPosY, whdiameter, whdiameter);
                Font font = new Font("calibri", 8);
                g.DrawString(wh.index.ToString(), font, Brushes.White, whMapPosX + 3, whMapPosY + 3);
            }

            foreach (Customer ctm in customerList)                          // Draws clients                       
            {
                int cmMapPosX = ctm.point.X - cmdiameter / 2;               // Center dot in graphic
                int cmMapPosY = ctm.point.Y - cmdiameter / 2;               // Center dot in Graphic
                // draws dot on map 
                g.FillEllipse(Brushes.OrangeRed, cmMapPosX, cmMapPosY, cmdiameter, cmdiameter);
            }

        }

        /// <summary>
        /// Draw shortest paths between warehouses & customers
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCalculate(object sender, EventArgs e)
        {
            Graphics g = Graphics.FromHwnd(canvas.Handle);

            foreach (Customer c in customerList)                                // iterate client List to draw
            {
                myPen.Color = c.getDistance(wHouseList).wHColor;                // gets color assigned to each warehouse

                g.DrawLine(myPen, c.point, c.getDistance(wHouseList).GetPoint());    // draw line betweeen coordinates

            }
            drawCoordinates(g);                                                 // reDraw dots to show them over paths 
            g.Dispose();

        }// Method ends

        private void button3_Click(object sender, EventArgs e)
        {
            canvas.Refresh();

        }



        private void btnnew(object sender, EventArgs e)
        {

        }

    }
}
