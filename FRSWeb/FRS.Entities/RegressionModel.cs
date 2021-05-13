using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRS.Entities
{
    public class RegressionModel
    {
        public double AFreeCoefficient { get; set; }
        public double BCoefficientForX { get; set; }
        public double XRegressiontCounting { get; set; }
        public double YRegressionResult { get; set; }
        public List<double> MultipleRegressionAnswers { get; set; }
        
    }
}
