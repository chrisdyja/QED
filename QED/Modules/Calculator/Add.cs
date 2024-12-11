using QED.Models;
using System.Globalization;

namespace QED.Modules.Calculator
{
    public class Add
    {
        public const string Summary = "Sum of two numbers";

        public static async Task<IResult> Handle(string a, string b)
        {
            Thread.Sleep(4000);
            if (TryParseDouble(a, out double number1) && TryParseDouble(b, out double number2))
            {
                var result = number1 + number2;
                var sumResponse = new SumResponse() { Sum = result};
                return Results.Ok(sumResponse);
            }

            return Results.BadRequest("Invalid number format. Please provide valid doubles.");
        }
        static bool TryParseDouble(string input, out double result)
        {
            var cultures = new[]
            {
                new CultureInfo("pl-PL"),
            };

            foreach (var culture in cultures)
            {
                if (double.TryParse(input, NumberStyles.Float, culture, out result))
                {
                    return true;
                }
            }

            result = 0;
            return false;
        }

    }
}
