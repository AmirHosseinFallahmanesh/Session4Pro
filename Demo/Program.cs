using Demo.Dal;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Demo
{//IDesignTimeDbContext
    internal class Program
    {
        private static void Main(string[] args)
        {
            DbContextOptionsBuilder<MyContext> optionsBuilder = new DbContextOptionsBuilder<MyContext>();
            optionsBuilder.UseSqlServer("Server=.;Database=demo03;Trusted_Connection=True;");
            MyContext ctx = new MyContext(optionsBuilder.Options);

            Order order = new Order()
            {
                OrderId = 3,
                Country = "Ir",
                Username = "Nima"
            };
            ctx.Remove(order);
            ctx.SaveChanges();
            Console.ReadKey();
         

        }

        private static void DeleteDemo(MyContext ctx)
        {
            var order = ctx.OrderLines.Find(1);
            Console.WriteLine(ctx.Entry(order).State);
            ctx.OrderLines.Remove(order);
            Console.WriteLine(ctx.Entry(order).State);
            ctx.SaveChanges();
            Console.WriteLine(ctx.Entry(order).State);
        }

        private static void Update04(MyContext ctx)
        {
            Order order = new Order()
            {
                OrderId = 3,
                Country = "Ir",
                Username = "Nima"
            };
            ctx.Orders.Update(order);
        }

        private static void Update03(MyContext ctx)
        {
            Order order = new Order()
            {
                OrderId = 3,
                Country = "Ir",
                Username = "Nima"
            };
            Console.WriteLine(ctx.Entry(order).State);

            if (ctx.Entry(order).State==EntityState.Detached)
            {
                ctx.Entry(order).State = EntityState.Modified;

            }
          
            ctx.SaveChanges();
        }

        private static void Update02(MyContext ctx, Order o)
        {
            Order order = ctx.Orders.Find(o.OrderId);
            Console.WriteLine(ctx.Entry(order).State);
            order.Country = o.Country;
            order.Username = o.Username;
            Console.WriteLine(ctx.Entry(order).State);
            ctx.SaveChanges();
            Console.WriteLine(ctx.Entry(order).State);
        }

        private static void Update01(MyContext ctx)
        {
            Order order = ctx.Orders.Find(1);
            Console.WriteLine(ctx.Entry(order).State);
            order.Country = "Ag";
            Console.WriteLine(ctx.Entry(order).State);
            ctx.SaveChanges();
            Console.WriteLine(ctx.Entry(order).State);
        }

        private static void InsertDemo(MyContext ctx)
        {
            Order order = new Order()
            {
                Country = "US",
                Username = "Nima"
            };
            Console.WriteLine(ctx.Entry(order).State);
            ctx.Orders.Add(order);
            Console.WriteLine(ctx.Entry(order).State);
            ctx.SaveChanges();
            Console.WriteLine(ctx.Entry(order).State);
            Console.WriteLine("Done!!!");
            Console.ReadKey();
        }









        #region LoadingMethods

        private static void SelecLaoding(MyContext ctx)
        {
            //DTO
            var data = ctx.Orders.Select(c => new
            {

                country = c.Country,
                user = c.Username


            }).ToList();
        }

        private static void ExplictLoading(MyContext ctx)
        {
            var order = ctx.Orders.Find(1);
            var data = ctx.Entry(order).Collection(a => a.OrderLines).Query().Where(a => a.Count > 4).ToList();
            ctx.Entry(order).Collection(a => a.OrderLines).Load();
            var data1 = ctx.Entry(order).Reference(a => a.OrderLines).Query().Where(a => a.Count > 4).ToList();
        }

        private static void EagerLoading(MyContext ctx)
        {
            var result = ctx.Orders.Include(a => a.OrderLines).ToList();
            foreach (var item in result)
            {
                Console.WriteLine(item.Username + " " + item.Country);
                foreach (var orderLine in item.OrderLines)
                {
                    Console.WriteLine("    " + orderLine.ProductName + " " + orderLine.Count);
                }
            }
        }

        //    private static void InitData(MyContext ctx)
        //    {
        //        Order order;
        //        List<OrderLine> orderLines;
        //        order = new Order() { Country = "Uk", Username = "Jack" };
        //        orderLines = new List<OrderLine>()
        //        {
        //            new OrderLine()
        //            {
        //                Count=5,
        //                ProductName="Jacket"
        //            },
        //                new OrderLine()
        //            {
        //                Count=8,
        //                ProductName="Food"
        //            },

        //        };

        //        order.OrderLines = orderLines;
        //        ctx.Orders.Add(order);
        //        ctx.SaveChanges();

        //    }
        //}

        #endregion
    }
}
