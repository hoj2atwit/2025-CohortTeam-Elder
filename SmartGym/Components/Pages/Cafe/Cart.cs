namespace SmartGym.Components.Pages.Cafe
{
    public class Cart
    {
        public OrderedDictionary<int, CartItem> cartItems = new();
        private decimal total = 0;
        public string totalString = "";

        /// <summary>
        /// Cart constructor. Applies correct formatting currency formatting.
        /// </summary>
        public Cart()
        {
            updateTotal();
        }

        /// <summary>
        /// Removes item from cart Dictionary
        /// </summary>
        /// <param name="id">itemId of menuItem desired to remove.</param>
        public void removeItem(int id)
        {
            cartItems.Remove(id);
            updateTotal();
        }

        /// <summary>
        /// Adds an item selected by user to the cart.
        /// If the item already exists in cart, increment amount of that item by 1.
        /// </summary>
        /// <param name="item"> MenuItem selected by user </param>
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

        /// <summary>
        /// Updates total to the sum of all current cart item prices.
        /// </summary>
        public void updateTotal()
        {
            total = 0;
            foreach (CartItem ci in cartItems.Values)
            {
                total += ci.currentPrice;
            }
            totalString = String.Format("{0:C}", total);
        }

        /// <summary>
        /// Class of an individual CartItem
        /// </summary>
        public class CartItem
        {
            public MenuItem? item;
            public int amount = 0;
            public decimal currentPrice;
            public string? currentPriceString;

            /// <summary>
            /// Constructor for CartItem.
            /// Automatically updates price of item depending on amount desired.
            /// </summary>
            /// <param name="item">Menu Item associated with cartItem</param>
            /// <param name="amount">Amount of MenuItem desired by user</param>
            public CartItem(MenuItem item, int amount)
            {
                this.item = item;
                updatePrice(amount);
            }

            /// <summary>
            /// Updates price depending on new amount given.
            /// </summary>
            /// <param name="amount">New amount of MenuItem desired by user</param>
            public void updatePrice(int amount)
            {
                this.amount = amount;
                updatePrice();
            }

            /// <summary>
            /// Updates price of CartItem dependant on the 
            /// MenuItem's price times the amount desired by user.
            /// </summary>
            public void updatePrice()
            {
                currentPrice = item.price * amount;
                currentPriceString = String.Format("{0:C}", currentPrice);
            }

            /// <summary>
            /// Increase amount of MenuItem desired by 1. Updates price accordingly.
            /// </summary>
            public void increaseAmount()
            {
                amount++;
                updatePrice();
            }

            /// <summary>
            /// Reduce amount of MenuItem desired by 1. Update price accordingly.
            /// </summary>
            public void reduceAmount()
            {
                amount--;
                updatePrice();
            }
        }

    }
}
