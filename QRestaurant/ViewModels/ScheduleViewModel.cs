using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QRestaurantMain.ViewModels
{
    [Keyless]
    [NotMapped]
    public class ScheduleViewModel
    {
        // -- MONDAY ---
        
        public string timeInput0_0 { get; set; }

        public string timeInput0_1 { get; set; }

        public string timeInput0_2 { get; set; }
        
        public string timeInput0_3 { get; set; }

        // -- TUESDAY ---

        public string timeInput1_0 { get; set; }

        public string timeInput1_1 { get; set; }

        public string timeInput1_2 { get; set; }

        public string timeInput1_3 { get; set; }

        // -- WEDNESDAY ---

        public string timeInput2_0 { get; set; }

        public string timeInput2_1 { get; set; }

        public string timeInput2_2 { get; set; }

        public string timeInput2_3 { get; set; }

        // -- THURSDAY ---

        public string timeInput3_0 { get; set; }

        public string timeInput3_1 { get; set; }

        public string timeInput3_2 { get; set; }

        public string timeInput3_3 { get; set; }

        // -- FRIDAY ---

        public string timeInput4_0 { get; set; }

        public string timeInput4_1 { get; set; }

        public string timeInput4_2 { get; set; }

        public string timeInput4_3 { get; set; }

        // -- SATURDAY ---

        public string timeInput5_0 { get; set; }

        public string timeInput5_1 { get; set; }

        public string timeInput5_2 { get; set; }

        public string timeInput5_3 { get; set; }

        // -- SUNDAY ---

        public string timeInput6_0 { get; set; }

        public string timeInput6_1 { get; set; }

        public string timeInput6_2 { get; set; }

        public string timeInput6_3 { get; set; }
    }
}
