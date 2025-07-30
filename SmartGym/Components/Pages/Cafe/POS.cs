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
    }
}
