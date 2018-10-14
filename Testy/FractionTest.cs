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

        private int RandomIntWithinPlusMinus(Random rnd,int minInc,int maxExc)
        {
            return rnd.Next(minInc, maxExc) * (rnd.Next(1, 3) == 1 ? 1 : -1);
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

        [TestMethod]
        public void AddFraction()
        {

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
                Assert.IsTrue(f.Denominator == i, $"Denominator is not correct({f.Denominator}) - 1 expected");
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
