using System;
using System.Collections.Generic;
using System.Text;

namespace EdaVPuti.Models
{
    public enum MenuItemType
    {
        EnterTicketPage,
        About,
        Cart
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
