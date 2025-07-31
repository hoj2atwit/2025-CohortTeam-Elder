namespace SmartGym.Components.Pages.Cafe
{
    public class MenuItem
    {
        public int itemId = 0;
        public decimal price = 0.0m;
        public string name = "";
        public string description = "";
        public string priceString = "";
        public string imageLocation = "";

        /// <summary>
        /// Default constructor for MenuItem.
        /// </summary>
        public MenuItem()
        {
            setPrice(0);
            name = "None";
            setImageLocation("None");
            itemId = 0;
            description = "No Description";
        }

        /// <summary>
        /// Constructor for MenuItem
        /// </summary>
        /// <param name="name"> Name of MenuItem </param>
        /// <param name="price"> The decimal value price of the given item </param>
        /// <param name="itemId"> The MenuItem's unique id </param>
        /// <param name="description"> The description of the menu item </param>
        public MenuItem(string name, decimal price, int itemId, string description)
        {
            setPrice(price);
            this.name = name;
            setImageLocation(name);
            this.itemId = itemId;
            this.description = description;
        }

        /// <summary>
        /// Set MenuItem image location depending on name entered.
        /// Automatically fills out the proper naming convention of file location.
        /// TODO: Might name based off of itemId Later as a more unique Identifier.
        /// </summary>
        /// <param name="name"> name of the given item </param>
        private void setImageLocation(string name)
        {
            //True Implementation of image location. Adapt to naming convention
            //imageLocation = $"lib/images/{name}.jpg";
            //Just using Coffee Image for now
            imageLocation = $"lib/images/Coffee.jpg";
        }

        /// <summary>
        /// Sets price of MenuItem. Formats the decimal value into a curreny string for display purposes.
        /// </summary>
        /// <param name="p"> new price of MenuItem </param>
        private void setPrice(decimal p)
        {
            price = p;
            priceString = String.Format("{0:C}", p);
        }

        /// <summary>
        /// Generates a list of MenuItems to show on the menu.
        /// Ideally pulls from the database but currently just generates 50 almost identical items.
        /// </summary>
        /// <returns> List<MenuItem> that is filled full list of MenuItems </returns>
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

        /// <summary>
        /// Generates a dictionary of MenuItems. This is so the menu is searchable by name.
        /// </summary>
        /// <returns> A Dictionary<string,MenuItem> filled with all of the MenuItems available as the values and names as the keys </returns>
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
}
