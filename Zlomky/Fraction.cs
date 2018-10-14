using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zlomky
{
    public class Fraction
    {
        public int Numerator { get; set; }
        private int _Denominator = 1;
        public int Denominator { get => _Denominator; set
            {
                if (value == 0)
                    throw new ArgumentOutOfRangeException("Denominator must not be 0");
                _Denominator = value;
            } }

        public Fraction()
        {
            Numerator = 0;
        }

        public Fraction(int numerator)
        {
            Numerator = numerator;
        }

        public Fraction(int numerator, int denominator)
        {
            Numerator = numerator;
            Denominator = denominator;
        }

        public static Fraction Simplify(Fraction f)
        {
            var t = SimplifyInts(f.Numerator, f.Denominator);
            return new Fraction(t.Item1, t.Item2);
        }

        private static Tuple<int,int> SimplifyInts(int a, int b)
        {
            int gcd = FindGCD(a, b);
            int twoMinuses = a < 0 && b < 0 ? -1 : 1; //Removes minuses if they are in num and denom
            return new Tuple<int, int>(a / gcd * twoMinuses, b / gcd * twoMinuses);
        }

        public override string ToString()
        {
            return Numerator + "/" + Denominator;
        }

        public double ToDouble()
        {
            return ((double)Numerator) / (Denominator);
        }

        public decimal ToDecimal()
        {
            return new decimal(Numerator) / new decimal(Denominator);
        }

        public int ToInt()
        {
            return Numerator / Denominator;
        }

        /*private static int FindGCD(int a, int b)
        {
            while (a != 0 && b != 0)
            {
                if (a > b)
                    a %= b;
                else
                    b %= a;
            }

            return a == 0 ? b : a;
        }
        */
        private static int FindGCD(int ao, int bo)
        {
            var a = Math.Abs(ao);
            var b = Math.Abs(bo);

            while (a != 0 && b != 0)
            {
                if (a > b)
                    a %= b;
                else
                    b %= a;
            }

            return a == 0 ? b : a;
        }

        private static int FindLCM(int a, int b)
        {
            return a * b / FindGCD(a, b);
        }

        public static Fraction operator +(Fraction a, Fraction b)
        {
            int LCM = FindLCM(a.Denominator, b.Denominator);
            return new Fraction
                (
                a.Numerator*(LCM/a.Denominator)+ b.Numerator * (LCM / b.Denominator),
                LCM
                );
        }

        public static Fraction operator +(Fraction a, int b)
        {
            return new Fraction
                (
                a.Numerator+ b*a.Numerator,
                a.Denominator
                );
        }

        public static Fraction operator -(Fraction a, Fraction b)
        {
            int LCM = FindLCM(a.Denominator, b.Denominator);
            return new Fraction
                (
                a.Numerator * (LCM / a.Denominator) - b.Numerator * (LCM / b.Denominator),
                LCM
                );
        }

        public static Fraction operator -(Fraction a, int b)
        {
            return new Fraction
                (
                a.Numerator - b * a.Numerator,
                a.Denominator
                );
        }

        public static Fraction operator *(Fraction a, Fraction b)
        {
            return new Fraction
                (
                a.Numerator*b.Numerator,
                a.Denominator*b.Denominator
                );
        }

        public static Fraction operator *(Fraction a, int b)
        {
            return new Fraction
                (
                a.Numerator * b,
                a.Denominator 
                );
        }

        public static Fraction operator /(Fraction a, Fraction b)
        {
            return new Fraction
                (
                a.Numerator * b.Denominator,
                a.Denominator * b.Numerator
                );
        }

        public static Fraction operator /(Fraction a, int b)
        {
            return new Fraction
                (
                a.Numerator,
                a.Denominator*b
                );
        }

        public static bool operator ==(Fraction a, double b)
        {
            return a.ToDouble() == b;
        }

        public static bool operator !=(Fraction a, double b)
        {
            return a.ToDouble() != b;
        }

        public static bool operator == (Fraction a, Fraction b)
        {
            var sa = Simplify(a);
            var sb = Simplify(b);
            return sa.Numerator == sb.Numerator && sa.Denominator == sb.Denominator;
        }

        public static bool operator !=(Fraction a, Fraction b)
        {
            return !(a == b);
        }

    }
}
