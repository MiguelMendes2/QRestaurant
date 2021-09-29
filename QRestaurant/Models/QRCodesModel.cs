using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QRestaurantMain.Models
{
    public class QRCodesModel
    {
        [Key]
        public int QRCodesId { get; set; }

        [Required]
        [ForeignKey("Tables")]
        public string TableId { get; set; }
    
        [Required]
        public string APIKEY { get; set; }

        public TablesModel Tables {  get; set; }
    }
}
