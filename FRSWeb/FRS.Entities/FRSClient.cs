namespace FRS.Entities
{
    public class FRSClient
    {
        public string AccessKey { get; set; }
        public string SecretAccessKey { get; set; }

        public RegressionData regressionData { get; set; }
        public RegressionModel regressionModel { get; set; }

        public double X { get; set; }
    }
}