using Bogus;
using SmartGym.Models;

namespace SmartGym.Components.UIClasses.Cafe
{
    public class CartModel
    {
        public OrderedDictionary<int, CartItemModel> CartItems = new();
        public decimal Total = 0;
        public decimal Subtotal = 0;
        public decimal Tax = 0;
        public string TotalString = "";
        public string Notes = "";
        public int cartId = -1;
        public int userId = 1;
        private static readonly decimal TAXRATE = 0.07m;

        /// <summary>
        /// Cart constructor. Applies correct formatting currency formatting.
        /// </summary>
        public CartModel()
        {
            updateTotal();
        }

        /// <summary>
        /// Cart constructor. Applies correct formatting currency formatting.
        /// </summary>
        public CartModel(OrderDTO dto, Dictionary<string, MenuItemModel> allMenuItems)
        {
            Notes = string.IsNullOrEmpty(dto.Notes) ? "" : dto.Notes;
            userId = dto.UserId;
            cartId = dto.Id;
            foreach (CartItemsDTO itemDTO in dto.OrderCartList) 
            {
                CartItems[itemDTO.MenuItemId] = new CartItemModel(allMenuItems[itemDTO.Name], itemDTO.Quantity);
            }
            
            updateTotal();
        }

        /// <summary>
        /// Removes Item from cart Dictionary
        /// </summary>
        /// <param name="id">ItemId of menuItem desired to remove.</param>
        public void removeItem(int id)
        {
            CartItems.Remove(id);
            updateTotal();
        }

        /// <summary>
        /// Adds an Item selected by user to the cart.
        /// If the Item already exists in cart, increment amount of that Item by 1.
        /// </summary>
        /// <param name="Item"> MenuItem selected by user </param>
        public void addItem(MenuItemModel Item)
        {
            if (CartItems.ContainsKey(Item.ItemId))
            {
                CartItems[Item.ItemId].increaseAmount();
            }
            else
            {
                CartItems.Add(Item.ItemId, new CartItemModel(Item, 1));
            }
            updateTotal();
        }

        /// <summary>
        /// Updates Total to the sum of all current cart Item prices.
        /// </summary>
        public void updateTotal()
        {
            Subtotal = 0;
            foreach (CartItemModel ci in CartItems.Values)
            {
                Subtotal += ci.CurrentPrice;
            }
            Tax = Subtotal * TAXRATE;
            Total = Tax + Subtotal;
            TotalString = string.Format("{0:C}", Total);
        }

        /// <summary>
        /// Converts current cart into DTO to create new DB entree
        /// </summary>
        /// <returns></returns>
        public OrderDTO toDTO() 
        { 
            OrderDTO dto = new OrderDTO();
            if (cartId > 0) { dto.Id = cartId; }
            dto.CreatedAt = DateTime.Now;
            dto.OrderTime = dto.CreatedAt.AddMinutes(5);
            dto.UpdatedAt = dto.CreatedAt;
            dto.Notes = Notes;
            dto.OrderCartList = new List<CartItemsDTO>();
            dto.TotalPrice = Total;
            dto.UserId = userId;

            foreach (CartItemModel cartItem in CartItems.Values) 
            {
                dto.OrderCartList.Add(cartItem.toCartItemsDTO());
            }
            
            return dto;
        }

        /// <summary>
        /// Converts current cart into a PatchDTO to update existing DB entree
        /// </summary>
        /// <returns></returns>
        public OrderPatchDTO toPatchDTO(bool updateOrderTime)
        {
            OrderPatchDTO dto = new OrderPatchDTO();
            dto.UpdatedAt = DateTime.Now;
            if (updateOrderTime) { dto.OrderTime = DateTime.Now.AddMinutes(5); }
            dto.Notes = Notes;
            dto.OrderCartList = new List<CartItemsDTO>();
            dto.TotalPrice = Total;

            foreach (CartItemModel cartItem in CartItems.Values)
            {
                dto.OrderCartList.Add(cartItem.toCartItemsDTO());
            }

            return dto;
        }

        /// <summary>
        /// Class of an individual CartItem
        /// </summary>
        public class CartItemModel
        {
            public MenuItemModel Item;
            public int Amount = 0;
            public decimal CurrentPrice;
            public string? CurrentPriceString;

            /// <summary>
            /// Constructor for CartItem.
            /// Automatically updates price of Item depending on amount desired.
            /// </summary>
            /// <param name="Item">Menu Item associated with cartItem</param>
            /// <param name="Amount">Amount of MenuItem desired by user</param>
            public CartItemModel(MenuItemModel Item, int Amount)
            {
                this.Item = Item;
                updatePrice(Amount);
            }

            /// <summary>
            /// Updates price depending on new amount given.
            /// </summary>
            /// <param name="Amount">New amount of MenuItem desired by user</param>
            public void updatePrice(int Amount)
            {
                this.Amount = Amount;
                updatePrice();
            }

            /// <summary>
            /// Updates price of CartItem dependant on the 
            /// MenuItem's price times the amount desired by user.
            /// </summary>
            public void updatePrice()
            {
                CurrentPrice = Item.Price * Amount;
                CurrentPriceString = string.Format("{0:C}", CurrentPrice);
            }

            /// <summary>
            /// Increase amount of MenuItem desired by 1. Updates price accordingly.
            /// </summary>
            public void increaseAmount()
            {
                Amount++;
                updatePrice();
            }

            /// <summary>
            /// Reduce amount of MenuItem desired by 1. Update price accordingly.
            /// </summary>
            public void reduceAmount()
            {
                Amount--;
                updatePrice();
            }

            public CartItemsDTO toCartItemsDTO() 
            {
                CartItemsDTO dto = new CartItemsDTO();
                dto.MenuItemId = Item.ItemId;
                dto.Name = Item.Name;
                dto.Quantity = Amount;
                dto.ImageRef = Item.ImageLocation;
                return dto;
            }
        }

    }
}
