using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SmartGym.Components.Pages.Cafe
{
    public class POS
    {
        public static POS instance = new POS();

        public MenuCartLayout.Cart? cart = new();
        public List<MenuItemLayout.MenuItem> fullMenu = new(); 
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
            this.fullMenu = MenuItemLayout.generateItemList();
            filteredMenu = this.fullMenu;
        }
    }
}
