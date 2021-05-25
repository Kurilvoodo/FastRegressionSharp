using System.Collections.Generic;

namespace FRS.Entities
{
    public class RegressionData
    {
        public int UserId { get; set; }
        public int RegressionDataId { get; set; }
        public double ACoefficientForX { get; set; }
        public double BFreeCoefficient { get; set; }
        public double PrecisionError { get; set; }
        public FunctionType FunctionType { get; set; }
        public List<double> XArgumentsFromRegressionAnalyze { get; set; }
        public List<double> YArgumentsFromRegressionAnalyze { get; set; }
    }
}