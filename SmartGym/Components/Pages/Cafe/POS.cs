using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SmartGym.Components.Pages.Cafe
{
    public class POS
    {
        public static POS instance = new POS();

        public MenuCartLayout.Cart? cart = new();
        public List<MenuItemLayout.MenuItem> fullMenuList = new(); 
        private Dictionary<string, MenuItemLayout.MenuItem> fullMenuDict = new();
        public List<MenuItemLayout.MenuItem> filteredMenu = new();
        public POS() 
        {
            if (instance != null)
            {
                instance = new POS();
            }
            else 
            {
                instance = this;
            }

            cart = new MenuCartLayout.Cart();
            fullMenuList = MenuItemLayout.MenuItem.generateItemList();
            fullMenuDict = MenuItemLayout.MenuItem.generateItemDict();
            filteredMenu = fullMenuList;
        }

        public void search(string search) 
        {
            if (search.Trim() == "" ) 
            {
                filteredMenu = fullMenuList;
                return;
            }
            //Use search value to apply regex requirements to keys in menu or DB
            List<MenuItemLayout.MenuItem> filteredList = new List<MenuItemLayout.MenuItem>();
            foreach (string name in fullMenuDict.Keys) {
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

    }
}
