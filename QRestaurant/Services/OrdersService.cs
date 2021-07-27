using QRestaurantMain.Data;
using QRestaurantMain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QRestaurantMain.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly QRestaurantDbContext AppDb;

        public OrdersService(QRestaurantDbContext Db)
        {
            AppDb = Db;
        }

        /// <summary>
        ///     Get Table Id witch Api Key is referenced
        /// </summary>
        /// <param name="ApiKey"> Api Key of rasberry pi </param>
        /// <returns> Tabled Id  </returns>
        public string VerifyOrder(string ApiKey)
        {
            var table = AppDb.Tables.FirstOrDefault(x => x.API_KEY == ApiKey);
            if (table == null)
                return null;
            return table.Id;
        }

        /// <summary>
        ///     Add New Order to a table
        /// </summary>
        /// <param name="orderlist"></param>
        /// <param name="tableId"></param>
        /// <returns> False -> Table Not Available, True -> Sucess </returns>
        public Boolean NewOrder(List<OrderViewModel> orderlist, string tableId)
        {
            var table = AppDb.Tables.FirstOrDefault(x => x.Id == tableId);
            if (table.State != 0)
                return false;
            table.State = 1;
            for (int i = 0; i < orderlist.Count(); i++)
            {
                if (table.Pedido == "")
                    table.Pedido += ",";
                table.Pedido += orderlist[i].ProductId + ",";
                table.Pedido += orderlist[i].Delivered;
                if (i + 1 < orderlist.Count())
                    table.Pedido += ",";
            }
            AppDb.SaveChanges();
            return true;
        }

        /// <summary>
        ///     Edit Order from table
        /// </summary>
        /// <param name="orderlist"> Edited Order </param>
        /// <param name="tableId"> Table Id </param>
        /// <returns> False -> Table Not Available, True -> Sucess </returns>
        public Boolean EditOrder(List<OrderViewModel> orderlist, string tableId)
        {
            var table = AppDb.Tables.FirstOrDefault(x => x.Id == tableId);
            if (table == null)
                return false;
            table.Pedido = "";
            for (int i = 0; i < orderlist.Count(); i++)
            {
                table.Pedido += orderlist[i].ProductId + ",";
                table.Pedido += orderlist[i].Delivered;
                if (i + 1 < orderlist.Count())
                    table.Pedido += ",";
            }
            AppDb.SaveChanges();
            return true;
        }

        /// <summary>
        ///     Check Out table
        /// </summary>
        /// <param name="tableId"></param>
        /// <returns> False -> Table Not Available, True -> Sucess </returns>
        public Boolean CheckOutTable(string tableId)
        {
            var table = AppDb.Tables.FirstOrDefault(x => x.Id == tableId);
            if (table == null)
                return false;
            table.State = 0;
            table.Pedido = "";
            AppDb.SaveChanges();
            return true;
        }

        /// <summary>
        ///     Add Delivered Products
        /// </summary>
        /// <param name="tableId"> TableId </param>
        /// <param name="productsId"> Array Delivered Products Id </param>
        public void DeliverProduct(string tableId, string[] productsId)
        {
            var table = AppDb.Tables.FirstOrDefault(x => x.Id == tableId);
            string[] ordersplited = table.Pedido.Split(',');
            for(int i = 0; i < productsId.Length; i++)
            {
                for (int j = 0; i < ordersplited.Length; j+=2)
                {
                    if(productsId[i] == ordersplited[j])
                    {
                        ordersplited[j+1] = "1";
                        break;
                    }
                }
            }
        }
    }
}
