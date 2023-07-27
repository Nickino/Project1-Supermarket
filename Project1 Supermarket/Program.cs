using SupermarketSimulation;

class Program
{
    static void Main(string[] args)
    {
        // Create an instance of the Supermarket class
        Supermarket supermarket = new Supermarket();

        // Call the Run method
        supermarket.Run();

        // Display the total number of customers served and total sales
        Console.WriteLine("Total sales: " + supermarket.TotalSales);

        // Keep the console window open
        Console.ReadKey();
    }
}