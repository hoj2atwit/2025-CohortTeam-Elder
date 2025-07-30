using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SmartGym.Components.Pages.Cafe
{
    public class POS
    {
        public static POS instance = new POS();

        public Cart? currentCart = new();
        public List<MenuItem> fullMenuList = new();
        private Dictionary<string, MenuItem> fullMenuDict = new();
        public List<MenuItem> filteredMenu = new();
        public POS()
        {
            refresh();

            if (instance != null)
            {
                return;
            }
            else
            {
                instance = this;
            }
        }

        public void refresh() 
        {
            currentCart = new Cart();
            fullMenuList = MenuItem.generateItemList();
            fullMenuDict = MenuItem.generateItemDict();
            filteredMenu = fullMenuList;
        }

        public void loadCart(Cart cart) 
        { 
            this.currentCart = cart;
        }

        public void search(string search)
        {
            if (search.Trim() == "")
            {
                filteredMenu = fullMenuList;
                return;
            }
            //Use search value to apply regex requirements to keys in menu or DB
            List<MenuItem> filteredList = new List<MenuItem>();
            foreach (string name in fullMenuDict.Keys)
            {
                if (name.Contains(search))
                {
                    //Check for filter requirement
                    filteredList.Add(fullMenuDict[name]);
                }
            }

            filteredMenu = filteredList;
        }

        /// <summary>
        /// Some Checkout function that does checkout things :^)
        /// </summary>
        public void checkout()
        {
            //TODO: Process cart information to create a receipt.
        }

        /// <summary>
        /// MenuItem class holds all relavent information for an item on the menu
        /// </summary>
        public class MenuItem
        {

            public int itemId = 0;
            public decimal price = 0.0m;
            public string name = "";
            public string description = "";
            public string priceString = "";
            public string imageLocation = "";

            public MenuItem()
            {
                setPrice(0);
                name = "None";
                setImageLocation("None");
                itemId = 0;
                description = "No Description";
            }

            public MenuItem(string name, decimal price, int itemId, string description)
            {
                setPrice(price);
                this.name = name;
                setImageLocation(name);
                this.itemId = itemId;
                this.description = description;
            }
            private void setImageLocation(string name)
            {
                //True Implementation of image location. Adapt to naming convention
                //imageLocation = $"lib/images/{name}.jpg";
                //Just using Coffee Image for now
                imageLocation = $"lib/images/Coffee.jpg";
            }

            private void setPrice(decimal p)
            {
                price = p;
                priceString = String.Format("{0:C}", p);
            }

            public static List<MenuItem> generateItemList()
            {
                List<MenuItem> menuItems = new List<MenuItem>();
                //TODO: Have this generate a list of menu items from the database
                for (int i = 0; i < 50; i++)
                {
                    menuItems.Add(new MenuItem("Coffee " + i, 6.99m, i, "Cup of Coffee"));
                }

                return menuItems;
            }

            public static Dictionary<string, MenuItem> generateItemDict()
            {
                Dictionary<string, MenuItem> menuItems = new Dictionary<string, MenuItem>();
                //TODO: Have this generate a list of menu items from the database
                for (int i = 0; i < 50; i++)
                {
                    menuItems.Add("Coffee " + i, new MenuItem("Coffee " + i, 6.99m, i, "Cup of Coffee"));
                }

                return menuItems;
            }

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
        }
    }
}
