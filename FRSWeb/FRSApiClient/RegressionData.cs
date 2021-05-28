using System.Collections.Generic;

namespace FRSApiClient
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
        public string X { get; set; }

        public double YResult { get; set; }

        public string Format
        {
            get
            {
                switch (FunctionType)
                {
                    case FunctionType.SimpleLine:
                        return $"y = {ACoefficientForX}*x + {BFreeCoefficient}";

                    case FunctionType.Exponential:
                        return $"ln(y) = {BFreeCoefficient} + {ACoefficientForX}*x -> y = e^{BFreeCoefficient} * e^({ACoefficientForX}*x)";

                    case FunctionType.Logarithmic:
                        return $"y = {BFreeCoefficient} + {ACoefficientForX}*ln(x)";

                    case FunctionType.Sedate:
                        return $"ln(y) = {BFreeCoefficient} + {ACoefficientForX}*ln(x) -> y = e^{BFreeCoefficient} * x^{ACoefficientForX}";

                    default:
                        return $"Function type wasn't provided";
                }
            }
        }
    }
}