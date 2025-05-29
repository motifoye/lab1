using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp3.Models
{
    public class BigNumber
    {
        private string number;

        public BigNumber(string number)
        {
            if (!IsValidNumber(number))
                throw new ArgumentException("Invalid number format.");

            this.number = number.TrimStart('0');
            if (this.number == "") this.number = "0";
        }

        public override string ToString() => number;

        public static BigNumber operator +(BigNumber a, BigNumber b)
        {
            var result = AddStrings(a.number, b.number);
            return new BigNumber(result);
        }

        public static BigNumber operator -(BigNumber a, BigNumber b)
        {
            bool negative = false;
            string num1 = a.number;
            string num2 = b.number;

            if (CompareStrings(num1, num2) < 0)
            {
                (num1, num2) = (num2, num1);
                negative = true;
            }

            var result = SubtractStrings(num1, num2);
            return new BigNumber((negative ? "-" : "") + result);
        }

        private static bool IsValidNumber(string str)
        {
            foreach (char c in str)
                if (!char.IsDigit(c)) return false;
            return true;
        }

        private static string AddStrings(string a, string b)
        {
            StringBuilder result = new StringBuilder();
            int carry = 0, i = a.Length - 1, j = b.Length - 1;

            while (i >= 0 || j >= 0 || carry > 0)
            {
                int digitA = i >= 0 ? a[i--] - '0' : 0;
                int digitB = j >= 0 ? b[j--] - '0' : 0;
                int sum = digitA + digitB + carry;
                result.Insert(0, (sum % 10).ToString());
                carry = sum / 10;
            }

            return result.ToString();
        }

        private static string SubtractStrings(string a, string b)
        {
            StringBuilder result = new StringBuilder();
            int borrow = 0, i = a.Length - 1, j = b.Length - 1;

            while (i >= 0)
            {
                int digitA = a[i] - '0';
                int digitB = j >= 0 ? b[j] - '0' : 0;

                int diff = digitA - digitB - borrow;
                if (diff < 0)
                {
                    diff += 10;
                    borrow = 1;
                }
                else
                {
                    borrow = 0;
                }

                result.Insert(0, diff.ToString());
                i--;
                j--;
            }

            return result.ToString().TrimStart('0') == "" ? "0" : result.ToString().TrimStart('0');
        }

        private static int CompareStrings(string a, string b)
        {
            if (a.Length != b.Length)
                return a.Length.CompareTo(b.Length);
            return string.Compare(a, b, StringComparison.Ordinal);
        }
    }

}
