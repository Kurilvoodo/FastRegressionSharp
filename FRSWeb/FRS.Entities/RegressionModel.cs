using System.Collections.Generic;

namespace FRS.Entities
{
    public class RegressionModel
    {
        public double ACoefficientForX { get; set; }
        public double BFreeCoefficient { get; set; }
        public double XRegressiontCounting { get; set; }
        public double YRegressionResult { get; set; }
        public List<double> MultipleRegressionAnswers { get; set; }
    }
}