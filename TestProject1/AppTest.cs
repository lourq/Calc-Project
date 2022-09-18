using CalcProject.App;

namespace TestProject1
{
    [TestClass]
    public class AppTest
    {
        [TestMethod]
        public void CalcTest()
        {
            // Testing Calc ......

            Calc calc = new();

            Assert.IsNotNull(calc);
        }

        [TestMethod]
        public void RomanNumberParse1Digit()
        {
            Assert.AreEqual(1, RomanNumber.Parse("I"));
            Assert.AreEqual(5, RomanNumber.Parse("V"));
            Assert.AreEqual(10, RomanNumber.Parse("X"));
            Assert.AreEqual(50, RomanNumber.Parse("L"));
            Assert.AreEqual(100, RomanNumber.Parse("C"));
            Assert.AreEqual(500, RomanNumber.Parse("D"));
            Assert.AreEqual(1000, RomanNumber.Parse("M"));
        }

        [TestMethod]
        public void RomanNumberParse2Digits()
        {
            Assert.AreEqual(4, RomanNumber.Parse("IV"));
            Assert.AreEqual(15, RomanNumber.Parse("XV"));
            Assert.AreEqual(900, RomanNumber.Parse("CM"));
            Assert.AreEqual(400, RomanNumber.Parse("CD"));
            Assert.AreEqual(55, RomanNumber.Parse("LV"));
            Assert.AreEqual(40, RomanNumber.Parse("XL"));
        }

        [TestMethod]
        public void RomanNumberParse3MoreDigits()
        {
            Assert.AreEqual(30, RomanNumber.Parse("XXX"));
            Assert.AreEqual(401, RomanNumber.Parse("CDI"));
            Assert.AreEqual(1999, RomanNumber.Parse("MCMXCIX"));
        }

        [TestMethod]
        public void RomanNumberParseInvalidDigit()
        {
            var ex = Assert.ThrowsException<ArgumentException>(() => { RomanNumber.Parse("ASDASZ"); });
            var exs = new ArgumentException($"Invalid input data: A");

            Assert.AreEqual(exs.Message, ex.Message);
        }

        [TestMethod]
        public void RomanNumberParseN()
        {
            Assert.AreEqual("Invalid input data: N",
                Assert.ThrowsException<ArgumentException>(() => RomanNumber.Parse("XN")).Message);
            Assert.AreEqual("Invalid input data: N",
                Assert.ThrowsException<ArgumentException>(() => RomanNumber.Parse("XNX")).Message);
            Assert.AreEqual("Invalid input data: N",
                Assert.ThrowsException<ArgumentException>(() => RomanNumber.Parse("NX")).Message);
            Assert.AreEqual("Invalid input data: N",
                Assert.ThrowsException<ArgumentException>(() => RomanNumber.Parse("NVII")).Message);
            Assert.AreEqual("Invalid input data: N",
                Assert.ThrowsException<ArgumentException>(() => RomanNumber.Parse("NN")).Message);
        }

        [TestMethod]
        public void RomanNumberCtor()
        {
            RomanNumber romanNumber = new();
            Assert.IsNotNull(romanNumber);

            romanNumber = new(10);
            Assert.IsNotNull(romanNumber);

            romanNumber = new(0);
            Assert.IsNotNull(romanNumber);
        }

        [TestMethod]
        public void RomanNumberToString()
        {
            RomanNumber romanNumber = new(0);
            Assert.AreEqual("N", romanNumber.ToString());

            romanNumber = new(90);
            Assert.AreEqual("XC", romanNumber.ToString());

            romanNumber = new(20);
            Assert.AreEqual("XX", romanNumber.ToString());

            romanNumber = new(1999);
            Assert.AreEqual("MCMXCIX", romanNumber.ToString());

        }

        [TestMethod]
        public void RomanNumberToStringParseCrossTest()
        {
            RomanNumber num = new();
            for (int n = 1; n <= 2022; ++n)
            {
                num.Value = n;
                Assert.AreEqual(n, RomanNumber.Parse(num.ToString()));
            }
        }

        [TestMethod]
        public void ToStringNegative()
        {
            RomanNumber rn = new() { Value = -10 };
            Assert.AreEqual("-X", rn.ToString());
            rn.Value = -90;
            Assert.AreEqual("-XC", rn.ToString());
        }

        [TestClass]
        public class OperationsTest
        {
            [TestMethod]
            public void AddRNTest()
            {
                RomanNumber rn2 = new(2);
                RomanNumber rn5 = new(5);
                RomanNumber rn7 = new(7);
                RomanNumber rn10 = new() { Value = 10 };
                RomanNumber rn_5 = new() { Value = -5 };
                RomanNumber rn_7 = new() { Value = -7 };

                Assert.AreEqual(9, rn2.Add(rn7).Value);
                Assert.AreEqual(20, rn10.Add(rn10).Value);
                Assert.AreEqual(5, rn10.Add(rn_5).Value);
                Assert.AreEqual(3, rn10.Add(rn_7).Value);
                Assert.AreEqual(10, rn5.Add(rn5).Value);
                Assert.AreEqual(7, rn7.Add(new RomanNumber(0)).Value);
                Assert.AreEqual(5, rn5.Add(new RomanNumber()).Value);
                Assert.AreEqual(25, rn5.Add(new RomanNumber(20)).Value);
                Assert.AreEqual(6, rn5.Add(new RomanNumber(1)).Value);
                Assert.AreEqual(19, rn10.Add(new RomanNumber(9)).Value);
                Assert.AreEqual(-5, rn5.Add(new RomanNumber(-10)).Value);
                Assert.AreEqual(rn7, rn2.Add(rn5));
                Assert.AreEqual(rn_5, rn_7.Add(rn2));
                Assert.AreEqual("XVII", rn7.Add(rn10).ToString());
                Assert.AreEqual("III", rn_7.Add(rn10).ToString());
                Assert.AreEqual("-V", rn_7.Add(rn2).ToString());
                Assert.AreEqual("-XII", rn_7.Add(rn_5).ToString());

            }

            [TestMethod]
            public void AddValueTest()
            {
                var rn = new RomanNumber(10);
                Assert.AreEqual(20, rn.Add(10).Value);
                Assert.AreEqual("V", rn.Add(-5).ToString());
                Assert.AreEqual(rn, rn.Add(0));
            }

            [TestMethod]
            public void AddStringTest()
            {
                var rn = new RomanNumber(10);
                Assert.AreEqual(30, rn.Add("XX").Value);
                Assert.AreEqual("-XL", rn.Add("-L").ToString());
                Assert.AreEqual(rn, rn.Add("N"));

                Assert.ThrowsException<ArgumentException>(() => rn.Add(""));
                Assert.ThrowsException<ArgumentException>(() => rn.Add("-"));
                Assert.ThrowsException<ArgumentException>(() => rn.Add("10"));
                Assert.ThrowsException<ArgumentNullException>(() => rn.Add((String)null!));
            }

            [TestMethod]
            public void AddStaticTest()
            {
                RomanNumber rn5 = RomanNumber.Add(2, 3);
                RomanNumber rn8 = RomanNumber.Add(rn5, 3);
                RomanNumber rn10 = RomanNumber.Add("I", "IX");
                RomanNumber rn9 = RomanNumber.Add(rn5, "IV");
                RomanNumber rn13 = RomanNumber.Add(rn5, rn8);
                Assert.AreEqual(5, rn5.Value);
                Assert.AreEqual(8, rn8.Value);
                Assert.AreEqual(9, rn9.Value);
                Assert.AreEqual(10, rn10.Value);
                Assert.AreEqual(13, rn13.Value);
            }
        }
    }

}
