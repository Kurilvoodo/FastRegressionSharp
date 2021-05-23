using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FRSWebApp.Models.Regression
{
    public class CreateRegressionVM
    {
        [Required]
        public string XData { get; set; }
        [Required]
        public string YData { get; set; }

        [Required]
        public int userId { get; set; }
    }
}