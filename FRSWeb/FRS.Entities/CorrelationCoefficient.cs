namespace FRS.Entities
{
    public class CorrellationCoefficient
    {
        private double r;

        public double R
        {
            get
            {
                return r;
            }
            set
            {
                if (value > 1)
                {
                    throw new System.Exception("Correlation can't be greater than 1");
                }
                if (value < -1)
                {
                    throw new System.Exception("Correlation can't be lesser than -1");
                }
                r = value;
            }
        }

        public CorrelationFactor CorrelationFactor { get; set; }
    }
}