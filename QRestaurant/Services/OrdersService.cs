using QRestaurantMain.Data;
using QRestaurantMain.Models;
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
            var table = AppDb.Tables.FirstOrDefault(x => x.APIKEY == ApiKey);
            if (table == null)
                return null;
            return table.TablesId;
        }

        /// <summary>
        ///     Add New Order to a table
        /// </summary>
        /// <param name="orderlist"></param>
        /// <param name="tableId"></param>
        /// <returns> False -> Table Not Available, True -> Sucess </returns>
        public Boolean NewOrder(List<OrderViewModel> orderlist, string APIKEY)
        {
            var table = AppDb.Tables.FirstOrDefault(x => x.APIKEY == APIKEY);
            QRCodesModel qrcode = new QRCodesModel { APIKEY = APIKEY, TableId = table.TablesId };
            AppDb.QRCodes.Add(qrcode);
            AppDb.SaveChanges();
            AppDb.Orders.Add(new Models.OrdersModel { QRCodesId = qrcode.QRCodesId, Date = DateTime.Now, Products = string.Join(",", orderlist) });
            return true;
        }

        /// <summary>
        ///     Edit Order from table
        /// </summary>
        /// <param name="orderlist"> Edited Order </param>
        /// <param name="tableId"> Table Id </param>
        /// <returns> False -> Table Not Available, True -> Sucess </returns>
        public Boolean EditOrder(List<OrderViewModel> orderlist,string tableId, string APIKEY)
        {
            var qrCode = AppDb.QRCodes.FirstOrDefault(x => x.TableId == tableId && x.APIKEY == APIKEY);
            if (qrCode == null)
                return false;
            var order = AppDb.Orders.FirstOrDefault(x => x.QRCodesId == qrCode.QRCodesId);
            
            return true;
        }

        /// <summary>
        ///     Check Out table
        /// </summary>
        /// <param name="tableId"></param>
        /// <returns> False -> Table Not Available, True -> Sucess </returns>
        public Boolean CheckOutTable(string tableId, string APIKEY)
        {
            var table = AppDb.Tables.FirstOrDefault(x => x.TablesId == tableId && x.APIKEY == APIKEY);
            if ( table == null)
                return false;
            table.State = 0;
            table.APIKEY = Guid.NewGuid().ToString();
            AppDb.SaveChanges();
            return true;
        }

        /// <summary>
        ///     Add Delivered Products
        /// </summary>
        /// <param name="tableId"> TableId </param>
        /// <param name="productsId"> Array Delivered Products Id </param>
        public void DeliverProduct(string APIKEY,string tableId, string[] productsId)
        {
            var qrcode = AppDb.QRCodes.FirstOrDefault(x => x.TableId == tableId && x.APIKEY == APIKEY);
            if (qrcode == null)
                return;
            var order = AppDb.Orders.FirstOrDefault(x => x.QRCodesId == qrcode.QRCodesId);
            if (qrcode == null)
                return;
        }
    }
}
