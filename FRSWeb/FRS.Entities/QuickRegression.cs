namespace FRS.Entities
{
    public class QuickRegression
    {
        public string XArgs { get; set; }
        public string YArgs { get; set; }
        public string X { get; set; }
        public FunctionType FunctionType { get; set; }

        public double bAnswer { get; set; }
        public double aAnswer { get; set; }
        public double yAnswer { get; set; }

        public string Format
        {
            get
            {
                switch (FunctionType)
                {
                    case FunctionType.SimpleLine:
                        return $"y = {aAnswer}*x + {bAnswer}";

                    case FunctionType.Exponential:
                        return $"ln(y) = {bAnswer} + {aAnswer}*x -> y = e^{bAnswer} * e^({aAnswer}*x)";

                    case FunctionType.Logarithmic:
                        return $"y = {bAnswer} + {aAnswer}*ln(x)";

                    case FunctionType.Sedate:
                        return $"ln(y) = {bAnswer} + {aAnswer}*ln(x) -> y = e^{bAnswer} * x^{aAnswer}";

                    default:
                        return $"Function type wasn't provided";
                }
            }
        }
    }
}