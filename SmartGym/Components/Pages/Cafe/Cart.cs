namespace SmartGym.Components.Pages.Cafe
{
    public class Cart
    {
        public OrderedDictionary<int, CartItem> cartItems = new();
        private decimal total = 0;
        public string totalString = "";

        public Cart()
        {
            updateTotal();
        }

        public void removeItem(int id)
        {
            cartItems.Remove(id);
            updateTotal();
        }

        public void addItem(MenuItem item)
        {
            if (cartItems.ContainsKey(item.itemId))
            {
                cartItems[item.itemId].increaseAmount();
            }
            else
            {
                cartItems.Add(item.itemId, new CartItem(item, 1));
            }
            updateTotal();
        }

        public void updateTotal()
        {
            total = 0;
            foreach (CartItem ci in cartItems.Values)
            {
                total += ci.currentPrice;
            }
            totalString = String.Format("{0:C}", total);
        }

        public class CartItem
        {
            public MenuItem? item;
            public int amount = 0;
            public decimal currentPrice;
            public string? currentPriceString;

            public CartItem(MenuItem item, int amount)
            {
                this.item = item;
                updatePrice(amount);
            }

            public void updatePrice(int amount)
            {
                this.amount = amount;
                updatePrice();
            }
            public void updatePrice()
            {
                currentPrice = item.price * amount;
                currentPriceString = String.Format("{0:C}", currentPrice);
            }

            public void increaseAmount()
            {
                amount++;
                updatePrice();
            }

            public void reduceAmount()
            {
                amount--;
                updatePrice();
            }
        }
    }
}
