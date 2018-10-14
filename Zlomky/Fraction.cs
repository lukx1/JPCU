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
            if(denominator == 0)
                throw new ArgumentOutOfRangeException("Denominator must not be 0");
            var simplifiedNumbers = Simplify(numerator, denominator);
            Numerator = simplifiedNumbers.Item1;
            Denominator = simplifiedNumbers.Item2;
        }

        private Tuple<int,int> Simplify(int a, int b)
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

        public int ToInt()
        {
            return Numerator / Denominator;
        }

        private static int FindGCD(int a, int b)
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

        public static Fraction operator +(Fraction a, Fraction b)
        {
            int GCD = FindGCD(a.Denominator, a.Denominator);
            return new Fraction
                (
                a.Numerator*(GCD/a.Denominator)+ b.Numerator * (GCD / b.Denominator),
                GCD
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
            int GCD = FindGCD(a.Denominator, a.Denominator);
            return new Fraction
                (
                a.Numerator * (GCD / a.Denominator) - b.Numerator * (GCD / b.Denominator),
                GCD
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

    }
}
