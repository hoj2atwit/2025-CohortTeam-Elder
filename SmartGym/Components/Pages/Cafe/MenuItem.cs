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
}
