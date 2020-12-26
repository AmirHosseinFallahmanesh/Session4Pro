using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;

namespace Demo.Dal
{
    public class Order
    {
        private readonly ILazyLoader lazyLoader;

        public Order(ILazyLoader lazyLoader)
        {
            this.lazyLoader = lazyLoader;
        }
        public Order()
        {

        }
        public int OrderId { get; set; }
        public string Username { get; set; }

        public string Country { get; set; }

        public List<OrderLine> OrderLines { get; set; }

        //private List<OrderLine> _orderLines;
        ////double dispatch
        //public List<OrderLine> OrderLines
        //{
        //    get
        //    {

        //        lazyLoader.Load(this, "OrderLines");

        //        return _orderLines;

        //      }
        //    set 
        //    {
        //        _orderLines = value;
        //    }
        //}

    }
    public class IranOrderDTO
    {
        public string Username { get; set; }

        public string Country { get; set; }

    }
}
