namespace SmartGym.Components.UIClasses.Cafe
{
    public class CartVM
    {
        public OrderedDictionary<int, CartItemVM> CartItems = new();
        private decimal Total = 0;
        public string TotalString = "";

        /// <summary>
        /// Cart constructor. Applies correct formatting currency formatting.
        /// </summary>
        public CartVM()
        {
            updateTotal();
        }

        /// <summary>
        /// Removes Item from cart Dictionary
        /// </summary>
        /// <param name="id">ItemId of menuItem desired to remove.</param>
        public void removeItem(int id)
        {
            CartItems.Remove(id);
            updateTotal();
        }

        /// <summary>
        /// Adds an Item selected by user to the cart.
        /// If the Item already exists in cart, increment amount of that Item by 1.
        /// </summary>
        /// <param name="Item"> MenuItem selected by user </param>
        public void addItem(MenuItemVM Item)
        {
            if (CartItems.ContainsKey(Item.ItemId))
            {
                CartItems[Item.ItemId].increaseAmount();
            }
            else
            {
                CartItems.Add(Item.ItemId, new CartItemVM(Item, 1));
            }
            updateTotal();
        }

        /// <summary>
        /// Updates Total to the sum of all current cart Item prices.
        /// </summary>
        public void updateTotal()
        {
            Total = 0;
            foreach (CartItemVM ci in CartItems.Values)
            {
                Total += ci.CurrentPrice;
            }
            TotalString = string.Format("{0:C}", Total);
        }

        /// <summary>
        /// Class of an individual CartItem
        /// </summary>
        public class CartItemVM
        {
            public MenuItemVM? Item;
            public int Amount = 0;
            public decimal CurrentPrice;
            public string? CurrentPriceString;

            /// <summary>
            /// Constructor for CartItem.
            /// Automatically updates price of Item depending on amount desired.
            /// </summary>
            /// <param name="Item">Menu Item associated with cartItem</param>
            /// <param name="Amount">Amount of MenuItem desired by user</param>
            public CartItemVM(MenuItemVM Item, int Amount)
            {
                this.Item = Item;
                updatePrice(Amount);
            }

            /// <summary>
            /// Updates price depending on new amount given.
            /// </summary>
            /// <param name="Amount">New amount of MenuItem desired by user</param>
            public void updatePrice(int Amount)
            {
                this.Amount = Amount;
                updatePrice();
            }

            /// <summary>
            /// Updates price of CartItem dependant on the 
            /// MenuItem's price times the amount desired by user.
            /// </summary>
            public void updatePrice()
            {
                CurrentPrice = Item.Price * Amount;
                CurrentPriceString = string.Format("{0:C}", CurrentPrice);
            }

            /// <summary>
            /// Increase amount of MenuItem desired by 1. Updates price accordingly.
            /// </summary>
            public void increaseAmount()
            {
                Amount++;
                updatePrice();
            }

            /// <summary>
            /// Reduce amount of MenuItem desired by 1. Update price accordingly.
            /// </summary>
            public void reduceAmount()
            {
                Amount--;
                updatePrice();
            }
        }

    }
}
