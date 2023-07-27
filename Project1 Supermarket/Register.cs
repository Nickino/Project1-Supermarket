using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketSimulation
{
    public class Register
    {
        public int ID { get; set; }
        public Queue<Customer> Line { get; set; }
        public double TotalSales { get; set; }
        public int CustomersServed { get; set; }

        public Register()
        {
            Line = new Queue<Customer>();
            TotalSales = 0;
            CustomersServed = 0; // Initialize to 0
        }

        //Customer joining in line
        //Queue FIFO First come, first served
        public void JoinLine(Customer customer)
        {
            Line.Enqueue(customer);

        }

        //Customer getting served and removed from the line
        public double CheckOut()
        {
            if (Line.Count > 0)
            {
                Customer customer = Line.Dequeue();
                double totalSales = customer.Cart.Sum(i => i.Price);
                TotalSales += totalSales;
                CustomersServed++;  // Increment the number of customers served by this register
                return totalSales;
            }
            return 0;
        }
    }
}
