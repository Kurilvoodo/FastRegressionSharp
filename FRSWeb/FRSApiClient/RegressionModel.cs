namespace FRSApiClient
{
    public class RegressionModel
    {
        public double ACoefficientForX { get; set; }
        public double BFreeCoefficient { get; set; }
        public double XRegressiontCounting { get; set; }
        public double YRegressionResult { get; set; }
        public FunctionType FunctionType { get; set; }
        public List<double> MultipleRegressionAnswers { get; set; }
    }
}