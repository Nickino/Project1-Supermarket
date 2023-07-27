namespace SupermarketSimulation
{
    public class Customer
    {
        public int ID { get; set; }
        public Stack<Item> Cart { get; set; }

        public Customer()
        {
            Cart = new Stack<Item>();
        }

        //Stack method
        //Adding item to the cart(top of the stack)
        //LIFO
        public void AddToCart(Item item)
        {
            Cart.Push(item);
        }
        //Item on the top most in the cart removed and returned
        public Item RemoveFromCart()
        {
            if (Cart.Count > 0)
                return Cart.Pop();
            else
                return null;
        }
    }
}
