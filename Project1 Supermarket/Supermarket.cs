using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketSimulation
{
    public class Supermarket
    {
        public List<Register> Registers { get; set; }
        public int LongestLine { get; set; }
        public int CustomersArrived { get; set; }
        public int CustomersServed { get; set; }
        public double TotalSales { get; set; }
        public double AverageCustomerTotal { get; set; }
        public double MinimumCustomerTotal { get; set; }
        public double MaximumCustomerTotal { get; set; }
        public List<string> ItemNames { get; set; }



        public Supermarket()
        {
            Random rnd = new Random();
            int numberOfRegisters = rnd.Next(5, 16); // Random number between 5 and 15

            Registers = new List<Register>();
            ItemNames = new List<string>
        {
            "Apple", "Banana", "Cereal", "Milk", "Eggs",
            "Cheese", "Bread", "Coffee", "Tea", "Chicken"
        };
            

            for (int i = 0; i < numberOfRegisters; i++)
            {
                Registers.Add(new Register());
            }
        }
        public double NextGaussian(Random rand, double mean, double standardDeviation)
        {
            double u1 = 1.0 - rand.NextDouble();
            double u2 = 1.0 - rand.NextDouble();
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);
            double randNormal = mean + standardDeviation * randStdNormal; //random normal(mean,stdDev^2)
            return randNormal;
        }

        public void Run()
        {
            Random rnd = new Random();
            List<double> cartTotals = new List<double>();

            // Random number of customers
            int numCustomers = rnd.Next(5, 501);//(50, 501); // Random number between 50 and 500

            // Create a list to hold customer arrival times
            List<Customer>[] arrivals = new List<Customer>[numCustomers];
            for (int i = 0; i < numCustomers; i++)
            {
                arrivals[i] = new List<Customer>();
            }

            // Create customers
            for (int i = 0; i < numCustomers; i++)
            {
                var customer = new Customer { ID = i + 1 };
                CustomersArrived++;

                // Each customer has random number of items in their cart
                int numItems = rnd.Next(5, 31); // Random number between 5 and 30

                double totalSaleForThisCustomer = 0; // This will hold the total sales for this customer


                for (int j = 0; j < numItems; j++)
                {
                    string itemName = ItemNames[rnd.Next(ItemNames.Count)];
    double itemPrice = 0.5 + rnd.NextDouble() * (100.0 - 0.5); // Random price between 0.50 and 100.00
                    customer.AddToCart(new Item { Name = itemName, Price = itemPrice });
                    totalSaleForThisCustomer += itemPrice; // Add the price to this customer's total


                }

                // Calculate arrival time using a normal distribution
                double arrivalTime = NextGaussian(rnd, numCustomers / 2.0, numCustomers / 6.0);
                arrivalTime = Math.Max(0, Math.Min(numCustomers - 1, arrivalTime)); // Keep arrival time within [0, numCustomers - 1]
                                                                                   
                // Add customer to the arrivals array at the corresponding time point
                arrivals[(int)Math.Round(arrivalTime)].Add(customer);

                Console.WriteLine("Customer " + customer.ID + " has " + numItems + " items with total sales of " + totalSaleForThisCustomer);
            }

            // Simulate time for customer shopping and arriving at the register
            for (int time = 0; time < numCustomers; time++)
            {
                // Process all customers arriving at this time point
                foreach (var customer in arrivals[time])
                {
                    // Customer joins the shortest line
                    Register shortestRegister = Registers.OrderBy(r => r.Line.Count).FirstOrDefault();
                    shortestRegister.JoinLine(customer);
                    LongestLine = Math.Max(LongestLine, Registers.Max(r => r.Line.Count));
                }
                foreach (Register register in Registers)
                {
                    if (register.Line.Count > 0)
                    {
                        double totalSales = register.CheckOut();
                        CustomersServed = Registers.Sum(r => r.CustomersServed);
                        cartTotals.Add(totalSales);
                    }

                }
                // Lists to store total sales of each customer

                if (cartTotals.Any())
                {
                    // Calculate statistics after each cycle of checking out
                    TotalSales = Registers.Sum(r => r.TotalSales);
                    AverageCustomerTotal = cartTotals.Average();
                    MinimumCustomerTotal = cartTotals.Min();
                    MaximumCustomerTotal = cartTotals.Max();
                }
                else
                {
                    AverageCustomerTotal = 0;
                    MinimumCustomerTotal = 0;
                    MaximumCustomerTotal = 0;
                }
                // Pause the simulation
                Thread.Sleep(200);


                Console.WriteLine("\n" + $"Total supermarket sales: ${TotalSales}, " + 
                   $"Longest Line: {LongestLine}, " +
                   $"Total Customer Arrived: {CustomersArrived}, " +
                   $"Total Customer Served: {CustomersServed}, " +
                   $"Average Customer Total: {AverageCustomerTotal}, " +
                   $"Minimum Customer Total: {MinimumCustomerTotal}, " +
                   $"Maximum Customer Total: {MaximumCustomerTotal}" +
                   $"Number of registers opened: " + Registers.Count);



            }


        }
    }
}
