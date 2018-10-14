using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zlomky;

namespace Testy
{
    [TestClass]
    public class FractionTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException),
        "Denominator = 0 was allowed")]
        public void ZeroConstructor()
        {
            var f = new Fraction(1, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException),
        "Denominator = 0 was allowed")]
        public void ZeroSet()
        {
            var f = new Fraction(1,1);
            f.Denominator = 0;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException),
        "Denominator = 0 was allowed")]
        public void ZeroDiv()
        {
            var f = new Fraction(1, 1);
            var r = f / 0;
        }

        private int RandomIntWithinPlusMinus(Random rnd,int minInc,int maxExc)
        {
            return rnd.Next(minInc, maxExc) * (rnd.Next(1, 3) == 1 ? 1 : -1);
        }

        public void ToDecimal()
        {
            Random rnd = new Random(123456);
            for (int i = 0; i < 1000; i++)
            {
                var num = RandomIntWithinPlusMinus(rnd, 0, 100);
                var denom = RandomIntWithinPlusMinus(rnd, 1, 100);
                var frc = new Fraction(num, denom);
                decimal realDecimal = new decimal(num) / new decimal(denom);
                Assert.IsTrue(frc.ToDecimal() == realDecimal, $"Conversion to double is not correct {frc.ToDecimal()} -X- {realDecimal} expected");
            }
        }

        public void ToDouble()
        {
            Random rnd = new Random(123456);
            for (int i = 0; i < 1000; i++)
            {
                var num = RandomIntWithinPlusMinus(rnd, 0, 100);
                var denom = RandomIntWithinPlusMinus(rnd, 1, 100);
                var frc = new Fraction(num, denom);
                double realDouble = (num)/((double)denom);
                Assert.IsTrue(frc.ToDouble() == realDouble,$"Conversion to double is not correct {frc.ToDouble()} -X- {realDouble} expected");
            }
        }

        private bool AreSame(decimal a, decimal b, int decimalPoints=20)
        {
            return Decimal.Round(a, decimalPoints) == Decimal.Round(b, decimalPoints);
        }

        [TestMethod]
        public void AddFraction()
        {
            Random rnd = new Random(223456);
            for (int i = 0; i < 1000; i++)
            {
                var num1 = RandomIntWithinPlusMinus(rnd, 0, 100);
                var denom1 = RandomIntWithinPlusMinus(rnd, 1, 100);
                var frc1 = new Fraction(num1, denom1);

                var num2 = RandomIntWithinPlusMinus(rnd, 0, 100);
                var denom2 = RandomIntWithinPlusMinus(rnd, 1, 100);
                var frc2 = new Fraction(num2, denom2);

                var resfrc = frc1 + frc2;
                var resDecimal = new decimal(num1) / new decimal(denom1) + new decimal(num2) / new decimal(denom2);
                Assert.IsTrue(AreSame(resfrc.ToDecimal(),resDecimal),"Adding two fractions is not the same as adding two decimals");
            }
        }

        [TestMethod]
        public void SubFraction()
        {
            Random rnd = new Random(323456);
            for (int i = 0; i < 1000; i++)
            {
                var num1 = RandomIntWithinPlusMinus(rnd, 0, 100);
                var denom1 = RandomIntWithinPlusMinus(rnd, 1, 100);
                var frc1 = new Fraction(num1, denom1);

                var num2 = RandomIntWithinPlusMinus(rnd, 0, 100);
                var denom2 = RandomIntWithinPlusMinus(rnd, 1, 100);
                var frc2 = new Fraction(num2, denom2);

                var resfrc = frc1 - frc2;
                var resDecimal = new decimal(num1) / new decimal(denom1) - new decimal(num2) / new decimal(denom2);
                Assert.IsTrue(AreSame(resfrc.ToDecimal(), resDecimal), "Subtracting two fractions is not" +
                    " the same as subtracting two decimals");
            }
        }

        [TestMethod]
        public void MultFraction()
        {
            Random rnd = new Random(423456);
            for (int i = 0; i < 1000; i++)
            {
                var num1 = RandomIntWithinPlusMinus(rnd, 0, 100);
                var denom1 = RandomIntWithinPlusMinus(rnd, 1, 100);
                var frc1 = new Fraction(num1, denom1);

                var num2 = RandomIntWithinPlusMinus(rnd, 0, 100);
                var denom2 = RandomIntWithinPlusMinus(rnd, 1, 100);
                var frc2 = new Fraction(num2, denom2);

                var resfrc = frc1 * frc2;
                var resDecimal = (new decimal(num1) / new decimal(denom1)) * (new decimal(num2) / new decimal(denom2));
                Assert.IsTrue(AreSame(resfrc.ToDecimal(), resDecimal), "Multiplying two fractions is not" +
                    " the same as multiplying two decimals");
            }
        }

        [TestMethod]
        public void DivFraction()
        {
            Random rnd = new Random(223456);
            for (int i = 0; i < 1000; i++)
            {
                var num1 = RandomIntWithinPlusMinus(rnd, 0, 100);
                var denom1 = RandomIntWithinPlusMinus(rnd, 1, 100);
                var frc1 = new Fraction(num1, denom1);

                var num2 = RandomIntWithinPlusMinus(rnd, 1, 100);
                var denom2 = RandomIntWithinPlusMinus(rnd, 1, 100);
                var frc2 = new Fraction(num2, denom2);

                var resfrc = frc1 / frc2;
                var resDecimal = (new decimal(num1) / new decimal(denom1)) / (new decimal(num2) / new decimal(denom2));
                Assert.IsTrue(AreSame(resfrc.ToDecimal(), resDecimal), "Dividing two fractions is not the same as dividing two decimals");
            }
        }

        [TestMethod]
        public void Constructor()
        {
            {
                var f = new Fraction();
                Assert.IsTrue(f.Numerator == 0, "Numerator is not 0");
                Assert.IsTrue(f.Denominator == 1, "Denominator is not 1");
            }
            for (int i = -100; i < 100; i++)
            {
                var f = new Fraction(i);
                Assert.IsTrue(f.Numerator == i, $"Numerator is not correct({f.Numerator}) - {i} expected");
                Assert.IsTrue(f.Denominator == 1, $"Denominator is not correct({f.Denominator}) - 1 expected");
            }
            {
                for (int i = -100; i < 100; i++)
                {
                    for (int j = -100; j < 100; j++)
                    {
                        if (j == 0) // Prevent div by 0
                            continue;
                        var f = new Fraction(i, j);
                        Assert.IsTrue(f.Numerator == i, $"Numerator is not correct({f.Numerator}) - {i} expected");
                        Assert.IsTrue(f.Denominator == j, $"Denominator is not correct({f.Denominator}) - {j} expected");
                    }
                }
            }
        }
    }
}
