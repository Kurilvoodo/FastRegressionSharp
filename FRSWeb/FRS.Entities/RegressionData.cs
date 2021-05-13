using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRS.Entities
{
    public class RegressionData
    {
        public int UserId { get; set; }
        public int RegressionDataId { get; set; }
        public double AFreeCoefficient { get; set; }
        public double BCoefficientForX { get; set; }
        public List<double> XArgumentsFromRegressionAnalyze { get; set; }
        public List<double> YArgumentsFromRegressionAnalyze { get; set; }
    }
}
