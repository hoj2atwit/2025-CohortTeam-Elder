using Microsoft.CodeAnalysis.CSharp.Syntax;
using SmartGym.Models;
using SmartGym.Services;
namespace SmartGym.Components.UIClasses.Cafe
{
    public class PointOfSaleModel
    {
        public static PointOfSaleModel instance;

        public CartModel? CurrentCart = new();
        public List<MenuItemModel> FullMenuList = new();
        private Dictionary<string, MenuItemModel> FullMenuDict = new();
        public List<MenuItemModel>? FilteredMenu;
        private readonly IOrderService _orderService;
        private readonly ICafeService _cafeService;

        /// <summary>
        /// Constructor for POS that ensures that a static instance of POS exists. Also Resets the values of the instance.
        /// </summary>
        public PointOfSaleModel(IOrderService orderService, ICafeService cafeService)
        {
            if (instance != null)
            {
                return;
            }
            else
            {
                _orderService = orderService;
                _cafeService = cafeService;
                instance = this;
            }
        }

        /// <summary>
        /// Refreshes the values of the instanced POS.
        /// </summary>
        public async Task refresh() 
        {
            CurrentCart = new CartModel();
            await getAllItems();
        }

        /// <summary>
        /// Allows user to load a given cart
        /// </summary>
        /// <param name="cart"></param>
        public void loadCart(CartModel cart) 
        { 
            CurrentCart = cart;
        }

        /// <summary>
        /// Filters Menu dependant on user's given search conditions.
        /// </summary>
        /// <param name="search"> text that the user wants to search for in the menu </param>
        public Task search(string search)
        {
            if (search.Trim() == "")
            {
                FilteredMenu = FullMenuList;
                return Task.CompletedTask;
            }
            //Use search value to apply regex requirements to keys in menu or DB
            List<MenuItemModel> filteredList = new List<MenuItemModel>();
            foreach (string name in FullMenuDict.Keys)
            {
                if (name.Contains(search))
                {
                    //Check for filter requirement
                    filteredList.Add(FullMenuDict[name]);
                }
            }

            FilteredMenu = filteredList;
            return Task.CompletedTask;
        }

        /// <summary>
        /// Some Checkout function that does checkout things :^)
        /// </summary>
        public async Task checkout()
        {
            //TODO: Go To Payment window. If payment succeeds, Send order to DB.
            if (CurrentCart != null) 
            {
                await _orderService.CreateOrder(CurrentCart.toDTO());
            }
            await refresh();
            
        }

        //Gets all menuitems from db
        public async Task getAllItems() 
        { 
            FullMenuList = MenuItemModel.DTOListToMenuItemModelList(await _cafeService.GetFullMenu());
            FullMenuDict = MenuItemModel.convertListToDict(FullMenuList);
            FilteredMenu = FullMenuList;
        }
    }
}
