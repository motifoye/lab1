using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp3.Models
{
    //public class BigNumber
    //{
    //    private int[] number;
    //    private const int Base = 10;

    //    public BigNumber(string input)
    //    {
    //        List<int> digits = new();
    //        foreach (char c in input.TrimStart('0'))
    //        {
    //            if (!char.IsDigit(c))
    //                throw new ArgumentException("Invalid character in number");
    //            digits.Add(c - '0');
    //        }

    //        if (digits.Count == 0) digits.Add(0);

    //        number = digits.ToArray();
    //    }

    //    public string getStringNumber()
    //    {
    //        StringBuilder sb = new();
    //        foreach (int digit in number)
    //            sb.Append(digit);
    //        return sb.ToString();
    //    }

    //    public override string ToString()
    //    {
    //        return getStringNumber();
    //    }

    //    public BigNumber getBigNumber()
    //    {
    //        return new BigNumber(this.getStringNumber());
    //    }

    //    public void Add(BigNumber bnum)
    //    {
    //        List<int> result = new();
    //        int carry = 0;

    //        int i = number.Length - 1;
    //        int j = bnum.number.Length - 1;

    //        while (i >= 0 || j >= 0 || carry > 0)
    //        {
    //            int aDigit = i >= 0 ? number[i--] : 0;
    //            int bDigit = j >= 0 ? bnum.number[j--] : 0;
    //            int sum = aDigit + bDigit + carry;
    //            result.Insert(0, sum % Base);
    //            carry = sum / Base;
    //        }

    //        number = result.ToArray();
    //    }

    //    public void Substruct(BigNumber bnum)
    //    {
    //        if (CompareTo(bnum) < 0)
    //            throw new InvalidOperationException("Subtraction would result in a negative number.");

    //        List<int> result = new();
    //        int borrow = 0;

    //        int i = number.Length - 1;
    //        int j = bnum.number.Length - 1;

    //        while (i >= 0)
    //        {
    //            int aDigit = number[i--];
    //            int bDigit = j >= 0 ? bnum.number[j--] : 0;

    //            int diff = aDigit - bDigit - borrow;
    //            if (diff < 0)
    //            {
    //                diff += Base;
    //                borrow = 1;
    //            }
    //            else
    //            {
    //                borrow = 0;
    //            }

    //            result.Insert(0, diff);
    //        }

    //        // Trim leading zeros
    //        while (result.Count > 1 && result[0] == 0)
    //            result.RemoveAt(0);

    //        number = result.ToArray();
    //    }

    //    public void Multiply(double num)
    //    {
    //        double current = double.Parse(this.getStringNumber());
    //        current *= num;
    //        number = ConvertToDigits(current);
    //    }

    //    public void Divide(double num)
    //    {
    //        if (num == 0)
    //            throw new DivideByZeroException();
    //        double current = double.Parse(this.getStringNumber());
    //        current /= num;
    //        number = ConvertToDigits(current);
    //    }

    //    private int[] ConvertToDigits(double value)
    //    {
    //        string str = ((long)value).ToString();
    //        int[] digits = new int[str.Length];
    //        for (int i = 0; i < str.Length; i++)
    //            digits[i] = str[i] - '0';
    //        return digits;
    //    }

    //    private int CompareTo(BigNumber other)
    //    {
    //        if (this.number.Length != other.number.Length)
    //            return this.number.Length.CompareTo(other.number.Length);

    //        for (int i = 0; i < this.number.Length; i++)
    //        {
    //            if (this.number[i] != other.number[i])
    //                return this.number[i].CompareTo(other.number[i]);
    //        }

    //        return 0;
    //    }
    //}

    public class BigNumber
    {
        private List<int> integerPart;   
        private List<int> fractionalPart; 
        private const int Base = 10;
        private bool isNegative = false;

        public bool IsNegative => isNegative;

        public BigNumber(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentException("Input string is null or empty.");

            integerPart = new List<int>();
            fractionalPart = new List<int>();

            input = input.Trim();

            string[] parts = input.Split('.', ',');

            string intPartStr = parts[0].TrimStart('0');
            if (string.IsNullOrEmpty(intPartStr))
                intPartStr = "0";

            foreach (char c in intPartStr)
            {
                if (!char.IsDigit(c))
                    throw new ArgumentException("Invalid character in integer part");
                integerPart.Add(c - '0');
            }

            if (parts.Length > 1)
            {
                string fracPartStr = parts[1].TrimEnd('0');
                foreach (char c in fracPartStr)
                {
                    if (!char.IsDigit(c))
                        throw new ArgumentException("Invalid character in fractional part");
                    fractionalPart.Add(c - '0');
                }
            }

            if (integerPart.Count == 0)
                integerPart.Add(0);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            if (isNegative && !(integerPart.Count == 1 && integerPart[0] == 0 && fractionalPart.Count == 0))
                sb.Append('-');

            foreach (int digit in integerPart)
                sb.Append(digit);

            if (fractionalPart.Count > 0)
            {
                sb.Append('.');
                foreach (int digit in fractionalPart)
                    sb.Append(digit);
            }

            return sb.ToString();
        }


        public void Add(BigNumber other)
        {
            AlignFractionalParts(this, other);

            int carry = 0;
            for (int i = fractionalPart.Count - 1; i >= 0; i--)
            {
                int sum = fractionalPart[i] + other.fractionalPart[i] + carry;
                fractionalPart[i] = sum % Base;
                carry = sum / Base;
            }

            int iInt = integerPart.Count - 1;
            int jInt = other.integerPart.Count - 1;

            while (iInt >= 0 || jInt >= 0 || carry > 0)
            {
                int aDigit = iInt >= 0 ? integerPart[iInt] : 0;
                int bDigit = jInt >= 0 ? other.integerPart[jInt] : 0;
                int sum = aDigit + bDigit + carry;

                if (iInt >= 0)
                    integerPart[iInt] = sum % Base;
                else
                    integerPart.Insert(0, sum % Base);

                carry = sum / Base;
                iInt--;
                jInt--;
            }

            TrimZeros();
        }

        public void Subtract(BigNumber other)
        {
            int cmp = CompareTo(other);
            if (cmp == 0)
            {
                // Результат 0
                integerPart = new List<int> { 0 };
                fractionalPart.Clear();
                isNegative = false;
                return;
            }

            if (cmp > 0)
            {
                // this > other, обычное вычитание
                isNegative = false;
                SubtractAbsolute(other);
            }
            else
            {
                // this < other, вычитаем this из other и меняем знак
                isNegative = true;
                BigNumber temp = other.getBigNumber();
                temp.SubtractAbsolute(this);
                this.integerPart = temp.integerPart;
                this.fractionalPart = temp.fractionalPart;
            }

            TrimZeros();
        }

        private void SubtractAbsolute(BigNumber other)
        {
            AlignFractionalParts(this, other);

            int borrow = 0;

            // Вычитаем дробную часть
            for (int i = fractionalPart.Count - 1; i >= 0; i--)
            {
                int diff = fractionalPart[i] - other.fractionalPart[i] - borrow;
                if (diff < 0)
                {
                    diff += Base;
                    borrow = 1;
                }
                else
                {
                    borrow = 0;
                }
                fractionalPart[i] = diff;
            }

            // Вычитаем целую часть
            int iInt = integerPart.Count - 1;
            int jInt = other.integerPart.Count - 1;

            while (iInt >= 0)
            {
                int aDigit = integerPart[iInt];
                int bDigit = jInt >= 0 ? other.integerPart[jInt] : 0;
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
                integerPart[iInt] = diff;
                iInt--;
                jInt--;
            }
        }


        public void Multiply(double num)
        {
            if (num < 0)
                throw new ArgumentException("Отрицательное умножение не поддерживается.");

            // Преобразуем текущий BigNumber в строку без десятичной точки
            StringBuilder fullNumber = new StringBuilder();

            foreach (int d in integerPart)
                fullNumber.Append(d);
            foreach (int d in fractionalPart)
                fullNumber.Append(d);

            // Кол-во цифр после запятой у текущего числа
            int fracLength = fractionalPart.Count;

            // Умножаем число как целое (без точки)
            if (!double.TryParse(num.ToString(), out double multiplier))
                throw new ArgumentException("Неправильное значение умножителя.");

            // Преобразуем строку в ulong
            // Для простоты используем double для умножения
            double currentValue = double.Parse(fullNumber.ToString());
            double result = currentValue * multiplier;

            // Преобразуем обратно в строку с учётом дробной части
            string resultStr = result.ToString($"F{fracLength}");
            BigNumber newValue = new BigNumber(resultStr);
            this.integerPart = newValue.integerPart;
            this.fractionalPart = newValue.fractionalPart;
        }

        public void Multiply(int num)
        {
            if (num < 0) throw new ArgumentException("Negative multiplication not implemented.");

            int carry = 0;

            // Умножаем дробную часть справа налево
            for (int i = fractionalPart.Count - 1; i >= 0; i--)
            {
                int prod = fractionalPart[i] * num + carry;
                fractionalPart[i] = prod % Base;
                carry = prod / Base;
            }

            // Умножаем целую часть справа налево
            for (int i = integerPart.Count - 1; i >= 0; i--)
            {
                int prod = integerPart[i] * num + carry;
                integerPart[i] = prod % Base;
                carry = prod / Base;
            }

            while (carry > 0)
            {
                integerPart.Insert(0, carry % Base);
                carry /= Base;
            }

            TrimZeros();
        }

        public void Divide(int num, int precision = 10)
        {
            if (num <= 0) throw new ArgumentException("Division by zero or negative number.");

            List<int> resultInt = new List<int>();
            List<int> remainderDigits = new List<int>(integerPart);
            remainderDigits.AddRange(fractionalPart);

            // Представим число как целое, сдвинутое на дробные цифры
            int totalLength = integerPart.Count + fractionalPart.Count;

            int remainder = 0;
            int digitsProcessed = 0;
            List<int> resultDigits = new List<int>();

            while (digitsProcessed < totalLength + precision)
            {
                int currentDigit = digitsProcessed < remainderDigits.Count ? remainderDigits[digitsProcessed] : 0;
                int value = remainder * Base + currentDigit;
                int quotientDigit = value / num;
                remainder = value % num;
                resultDigits.Add(quotientDigit);
                digitsProcessed++;
            }

            // Разделим результат на целую и дробную часть
            integerPart.Clear();
            fractionalPart.Clear();

            for (int i = 0; i < resultDigits.Count; i++)
            {
                if (i < integerPart.Count)
                    integerPart.Add(resultDigits[i]);
                else
                    fractionalPart.Add(resultDigits[i]);
            }

            // В нашем случае целая часть — первые integerPart.Count цифр (по исходной длине)
            for (int i = 0; i < integerPart.Count + fractionalPart.Count; i++)
            {
                if (i < integerPart.Count)
                    integerPart.Add(resultDigits[i]);
                else
                    fractionalPart.Add(resultDigits[i]);
            }

            TrimZeros();
        }

        // Сравнение чисел с учётом дробной части
        public int CompareTo(BigNumber other)
        {
            AlignFractionalParts(this, other);

            if (integerPart.Count != other.integerPart.Count)
                return integerPart.Count.CompareTo(other.integerPart.Count);

            for (int i = 0; i < integerPart.Count; i++)
            {
                if (integerPart[i] != other.integerPart[i])
                    return integerPart[i].CompareTo(other.integerPart[i]);
            }

            for (int i = 0; i < fractionalPart.Count; i++)
            {
                if (fractionalPart[i] != other.fractionalPart[i])
                    return fractionalPart[i].CompareTo(other.fractionalPart[i]);
            }

            return 0;
        }

        // Вспомогательный метод для выравнивания длины дробных частей
        private static void AlignFractionalParts(BigNumber a, BigNumber b)
        {
            int diff = a.fractionalPart.Count - b.fractionalPart.Count;
            if (diff > 0)
            {
                for (int i = 0; i < diff; i++)
                    b.fractionalPart.Add(0);
            }
            else if (diff < 0)
            {
                for (int i = 0; i < -diff; i++)
                    a.fractionalPart.Add(0);
            }
        }

        // Удаление лишних нулей с начала целой части и с конца дробной части
        private void TrimZeros()
        {
            while (integerPart.Count > 1 && integerPart[0] == 0)
                integerPart.RemoveAt(0);

            while (fractionalPart.Count > 0 && fractionalPart[^1] == 0)
                fractionalPart.RemoveAt(fractionalPart.Count - 1);
        }

        public BigNumber getBigNumber()
        {
            StringBuilder sb = new StringBuilder();

            foreach (int digit in integerPart)
                sb.Append(digit);

            if (fractionalPart.Count > 0)
            {
                sb.Append('.');
                foreach (int digit in fractionalPart)
                    sb.Append(digit);
            }

            return new BigNumber(sb.ToString());
        }
    }
}
