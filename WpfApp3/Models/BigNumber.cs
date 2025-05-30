using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp3.Models
{
    public class BigNumber
    {
        private int[] number;
        private const int Base = 10;

        public BigNumber(string input)
        {
            List<int> digits = new();
            foreach (char c in input.TrimStart('0'))
            {
                if (!char.IsDigit(c))
                    throw new ArgumentException("Invalid character in number");
                digits.Add(c - '0');
            }

            if (digits.Count == 0) digits.Add(0);

            number = digits.ToArray();
        }

        public string getStringNumber()
        {
            StringBuilder sb = new();
            foreach (int digit in number)
                sb.Append(digit);
            return sb.ToString();
        }

        public override string ToString()
        {
            return getStringNumber();
        }

        public BigNumber getBigNumber()
        {
            return new BigNumber(this.getStringNumber());
        }

        public void Add(BigNumber bnum)
        {
            List<int> result = new();
            int carry = 0;

            int i = number.Length - 1;
            int j = bnum.number.Length - 1;

            while (i >= 0 || j >= 0 || carry > 0)
            {
                int aDigit = i >= 0 ? number[i--] : 0;
                int bDigit = j >= 0 ? bnum.number[j--] : 0;
                int sum = aDigit + bDigit + carry;
                result.Insert(0, sum % Base);
                carry = sum / Base;
            }

            number = result.ToArray();
        }

        public void Substruct(BigNumber bnum)
        {
            if (CompareTo(bnum) < 0)
                throw new InvalidOperationException("Subtraction would result in a negative number.");

            List<int> result = new();
            int borrow = 0;

            int i = number.Length - 1;
            int j = bnum.number.Length - 1;

            while (i >= 0)
            {
                int aDigit = number[i--];
                int bDigit = j >= 0 ? bnum.number[j--] : 0;

                int diff = aDigit - bDigit - borrow;
                if (diff < 0)
                {
                    diff += Base;
                    borrow = 1;
                }
                else
                {
                    borrow = 0;
                }

                result.Insert(0, diff);
            }

            // Trim leading zeros
            while (result.Count > 1 && result[0] == 0)
                result.RemoveAt(0);

            number = result.ToArray();
        }

        public void Multiply(double num)
        {
            double current = double.Parse(this.getStringNumber());
            current *= num;
            number = ConvertToDigits(current);
        }

        public void Divide(double num)
        {
            if (num == 0)
                throw new DivideByZeroException();
            double current = double.Parse(this.getStringNumber());
            current /= num;
            number = ConvertToDigits(current);
        }

        private int[] ConvertToDigits(double value)
        {
            string str = ((long)value).ToString();
            int[] digits = new int[str.Length];
            for (int i = 0; i < str.Length; i++)
                digits[i] = str[i] - '0';
            return digits;
        }

        private int CompareTo(BigNumber other)
        {
            if (this.number.Length != other.number.Length)
                return this.number.Length.CompareTo(other.number.Length);

            for (int i = 0; i < this.number.Length; i++)
            {
                if (this.number[i] != other.number[i])
                    return this.number[i].CompareTo(other.number[i]);
            }

            return 0;
        }
    }

}
