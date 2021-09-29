using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace QRestaurantMain.Models
{
    [Keyless]
    public class OrdersModel
    {
        [Required]
        public int QRCodesId {  get; set; }

        [Required]
        public string Products { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public DateTime DeliveryDate {  get; set; }
    }
}
