namespace SmartGym.Components.UIClasses.Cafe
{
    public class MenuItemVM
    {
        public int ItemId = 0;
        public decimal Price = 0.0m;
        public string Name = "";
        public string Description = "";
        public string PriceString = "";
        public string ImageLocation = "";

        /// <summary>
        /// Default constructor for MenuItem.
        /// </summary>
        public MenuItemVM()
        {
            setPrice(0);
            Name = "None";
            setImageLocation("None");
            ItemId = 0;
            Description = "No Description";
        }

        /// <summary>
        /// Constructor for MenuItem
        /// </summary>
        /// <param name="Name"> Name of MenuItem </param>
        /// <param name="Price"> The decimal value Price of the given item </param>
        /// <param name="ItemId"> The MenuItem's unique id </param>
        /// <param name="Description"> The Description of the menu item </param>
        public MenuItemVM(string Name, decimal Price, int ItemId, string Description)
        {
            setPrice(Price);
            this.Name = Name;
            setImageLocation(Name);
            this.ItemId = ItemId;
            this.Description = Description;
        }

        /// <summary>
        /// Set MenuItem image location depending on name entered.
        /// Automatically fills out the proper naming convention of file location.
        /// TODO: Might name based off of ItemId Later as a more unique Identifier.
        /// </summary>
        /// <param name="Name"> Name of the given item </param>
        private void setImageLocation(string name)
        {
            //True Implementation of image location. Adapt to naming convention
            //ImageLocation = $"lib/images/{name}.jpg";
            //Just using Coffee Image for now
            ImageLocation = $"lib/images/Coffee.jpg";
        }

        /// <summary>
        /// Sets Price of MenuItem. Formats the decimal value into a curreny string for display purposes.
        /// </summary>
        /// <param name="p"> new Price of MenuItem </param>
        private void setPrice(decimal p)
        {
            Price = p;
            PriceString = string.Format("{0:C}", p);
        }

        /// <summary>
        /// Generates a list of MenuItems to show on the menu.
        /// Ideally pulls from the database but currently just generates 50 almost identical items.
        /// </summary>
        /// <returns> List<MenuItem> that is filled full list of MenuItems </returns>
        public static List<MenuItemVM> generateItemList()
        {
            List<MenuItemVM> menuItems = new List<MenuItemVM>();
            //TODO: Have this generate a list of menu items from the database
            for (int i = 0; i < 50; i++)
            {
                menuItems.Add(new MenuItemVM("Coffee " + i, 6.99m, i, "Cup of Coffee"));
            }

            return menuItems;
        }

        /// <summary>
        /// Generates a dictionary of MenuItems. This is so the menu is searchable by name.
        /// </summary>
        /// <returns> A Dictionary<string,MenuItem> filled with all of the MenuItems available as the values and names as the keys </returns>
        public static Dictionary<string, MenuItemVM> generateItemDict()
        {
            Dictionary<string, MenuItemVM> menuItems = new Dictionary<string, MenuItemVM>();
            //TODO: Have this generate a list of menu items from the database
            for (int i = 0; i < 50; i++)
            {
                menuItems.Add("Coffee " + i, new MenuItemVM("Coffee " + i, 6.99m, i, "Cup of Coffee"));
            }

            return menuItems;
        }
    }
}
