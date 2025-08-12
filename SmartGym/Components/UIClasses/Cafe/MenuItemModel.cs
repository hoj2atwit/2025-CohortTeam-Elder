using SmartGym.Models;
namespace SmartGym.Components.UIClasses.Cafe
{
    public class MenuItemModel
    {
        public int ItemId = 0;
        public decimal Price = 0.0m;
        public string Name = "";
        public string Description = "";
        public string PriceString = "";
        public string ImageLocation = "";
        public string Tags = "";
        public string Ingredients = "";

        /// <summary>
        /// Default constructor for MenuItem.
        /// </summary>
        public MenuItemModel()
        {
            setPrice(0);
            Name = "None";
            setImageLocation("None");
            ItemId = 0;
            Description = "No Description";
            Tags = "Food";
            Ingredients = "None";
        }

        /// <summary>
        /// Default constructor for MenuItem.
        /// </summary>
        public MenuItemModel(MenuItemsDTO menuItemDTO)
        {
            setPrice(menuItemDTO.Price);
            Name = menuItemDTO.Name;
            setImageLocation(menuItemDTO.ImageRef);
            ItemId = menuItemDTO.Id;
            Description = menuItemDTO.Description;
            Tags = menuItemDTO.Tags;
            Ingredients = menuItemDTO.Ingredients;
        }

        /// <summary>
        /// Constructor for MenuItem
        /// </summary>
        /// <param name="Name"> Name of MenuItem </param>
        /// <param name="Price"> The decimal value Price of the given item </param>
        /// <param name="ItemId"> The MenuItem's unique id </param>
        /// <param name="Description"> The Description of the menu item </param>
        public MenuItemModel(string Name, decimal Price, int ItemId, string Description, string Tags, string Ingredients)
        {
            setPrice(Price);
            this.Name = Name;
            setImageLocation(Name);
            this.ItemId = ItemId;
            this.Description = Description;
            this.Tags = Tags;
            this.Ingredients = Ingredients;
        }

        /// <summary>
        /// Set MenuItem image location depending on name entered.
        /// Automatically fills out the proper naming convention of file location.
        /// TODO: Might name based off of ItemId Later as a more unique Identifier.
        /// </summary>
        /// <param name="Name"> Name of the given item </param>
        private void setImageLocation(string imageRef)
        {
            //True Implementation of image location. Adapt to naming convention
            ImageLocation =  imageRef;
            //Just using Coffee Image for now
            //ImageLocation = $"lib/images/Coffee.jpg";
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
        public static List<MenuItemModel> generateItemList()
        {
            List<MenuItemModel> menuItems = new List<MenuItemModel>();
            //TODO: Have this generate a list of menu items from the database
            for (int i = 0; i < 50; i++)
            {
                menuItems.Add(new MenuItemModel("Coffee " + i, 6.99m, i, "Cup of Coffee", "", ""));
            }

            return menuItems;
        }

        /// <summary>
        /// Generates a dictionary of MenuItems. This is so the menu is searchable by name.
        /// </summary>
        /// <returns> A Dictionary<string,MenuItem> filled with all of the MenuItems available as the values and names as the keys </returns>
        public static Dictionary<string, MenuItemModel> generateItemDict()
        {
            Dictionary<string, MenuItemModel> menuItems = new Dictionary<string, MenuItemModel>();
            //TODO: Have this generate a list of menu items from the database
            for (int i = 0; i < 50; i++)
            {
                menuItems.Add("Coffee " + i, new MenuItemModel("Coffee " + i, 6.99m, i, "Cup of Coffee", "", ""));
            }

            return menuItems;
        }

        public static List<MenuItemModel> DTOListToMenuItemModelList(List<MenuItemsDTO> menuItemsDTO) {
            List<MenuItemModel> menuItems = new List<MenuItemModel>();
            if (menuItemsDTO != null) 
            {
                foreach (MenuItemsDTO itemDTO in menuItemsDTO)
                {
                    menuItems.Add(new MenuItemModel(itemDTO));
                }
            }

            return menuItems;
        }

        public static Dictionary<string, MenuItemModel> convertListToDict(List<MenuItemModel> model)
        {
            Dictionary<string, MenuItemModel> d = new Dictionary<string, MenuItemModel>();
            foreach (MenuItemModel item in model) 
            {
                d[item.Name] = item;
            }
            return d;
        }
    }
}
