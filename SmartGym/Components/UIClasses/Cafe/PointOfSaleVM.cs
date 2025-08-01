using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SmartGym.Components.UIClasses.Cafe
{
    public class PointOfSaleVM
    {
        public static PointOfSaleVM instance = new PointOfSaleVM();

        public CartVM? CurrentCart = new();
        public List<MenuItemVM> FullMenuList = new();
        private Dictionary<string, MenuItemVM> FullMenuDict = new();
        public List<MenuItemVM> FilteredMenu = new();
        
        /// <summary>
        /// Constructor for POS that ensures that a static instance of POS exists. Also Resets the values of the instance.
        /// </summary>
        public PointOfSaleVM()
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

        /// <summary>
        /// Refreshes the values of the instanced POS.
        /// </summary>
        public void refresh() 
        {
            CurrentCart = new CartVM();
            FullMenuList = MenuItemVM.generateItemList();
            FullMenuDict = MenuItemVM.generateItemDict();
            FilteredMenu = FullMenuList;
        }

        /// <summary>
        /// Allows user to load a given cart
        /// </summary>
        /// <param name="cart"></param>
        public void loadCart(CartVM cart) 
        { 
            CurrentCart = cart;
        }

        /// <summary>
        /// Filters Menu dependant on user's given search conditions.
        /// </summary>
        /// <param name="search"> text that the user wants to search for in the menu </param>
        public void search(string search)
        {
            if (search.Trim() == "")
            {
                FilteredMenu = FullMenuList;
                return;
            }
            //Use search value to apply regex requirements to keys in menu or DB
            List<MenuItemVM> filteredList = new List<MenuItemVM>();
            foreach (string name in FullMenuDict.Keys)
            {
                if (name.Contains(search))
                {
                    //Check for filter requirement
                    filteredList.Add(FullMenuDict[name]);
                }
            }

            FilteredMenu = filteredList;
        }

        /// <summary>
        /// Some Checkout function that does checkout things :^)
        /// </summary>
        public void checkout()
        {
            //TODO: Process cart information to create a receipt.
        }
    }
}
