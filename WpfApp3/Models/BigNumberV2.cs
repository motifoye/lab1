using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp3.Models
{
    public class BigNumberV2
    {
        private readonly string digits; // Без знака
        private readonly bool isNegative;

        public BigNumberV2(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentException("Empty input");

            input = input.Trim();
            isNegative = input.StartsWith("-");
            digits = input.TrimStart('-').TrimStart('0');

            if (digits == "") digits = "0"; // zero normalization
            if (digits == "0") isNegative = false; // -0 => 0
        }

        private BigNumberV2(string digits, bool isNegative)
        {
            this.digits = digits.TrimStart('0');
            if (this.digits == "") this.digits = "0";
            this.isNegative = (this.digits != "0") && isNegative;
        }

        public override string ToString() => isNegative ? "-" + digits : digits;

        // -------------------- Arithmetic --------------------

        public static BigNumberV2 operator +(BigNumberV2 a, BigNumberV2 b)
        {
            if (a.isNegative == b.isNegative)
            {
                string sum = AddStrings(a.digits, b.digits);
                return new BigNumberV2(sum, a.isNegative);
            }
            else
            {
                int cmp = CompareStrings(a.digits, b.digits);
                if (cmp == 0)
                    return new BigNumberV2("0", false);
                else if (cmp > 0)
                    return new BigNumberV2(SubtractStrings(a.digits, b.digits), a.isNegative);
                else
                    return new BigNumberV2(SubtractStrings(b.digits, a.digits), b.isNegative);
            }
        }

        public static BigNumberV2 operator -(BigNumberV2 a, BigNumberV2 b)
        {
            return a + new BigNumberV2(b.digits, !b.isNegative);
        }

        // -------------------- Utilities --------------------

        private static string AddStrings(string a, string b)
        {
            StringBuilder result = new();
            int carry = 0, i = a.Length - 1, j = b.Length - 1;

            while (i >= 0 || j >= 0 || carry > 0)
            {
                int da = i >= 0 ? a[i--] - '0' : 0;
                int db = j >= 0 ? b[j--] - '0' : 0;
                int sum = da + db + carry;
                result.Insert(0, (sum % 10).ToString());
                carry = sum / 10;
            }

            return result.ToString();
        }

        private static string SubtractStrings(string a, string b)
        {
            StringBuilder result = new();
            int borrow = 0, i = a.Length - 1, j = b.Length - 1;

            while (i >= 0)
            {
                int da = a[i--] - '0';
                int db = j >= 0 ? b[j--] - '0' : 0;

                int diff = da - db - borrow;
                if (diff < 0)
                {
                    diff += 10;
                    borrow = 1;
                }
                else
                {
                    borrow = 0;
                }

                result.Insert(0, diff);
            }

            return result.ToString().TrimStart('0') == "" ? "0" : result.ToString().TrimStart('0');
        }

        private static int CompareStrings(string a, string b)
        {
            if (a.Length != b.Length)
                return a.Length.CompareTo(b.Length);
            return string.CompareOrdinal(a, b);
        }
    }

}
