using System.Data;

namespace ZB.Calculator
{
    public class MathOperations
    {
        public static string EvaluateInput(string input)
        {
            return new DataTable().Compute(input, "").ToString();
        }
    }
}
