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
            Assert.AreEqual(1,    RomanNumber.Parse("I") );
            Assert.AreEqual(5,    RomanNumber.Parse("V") );
            Assert.AreEqual(10,   RomanNumber.Parse("X") );
            Assert.AreEqual(50,   RomanNumber.Parse("L") );
            Assert.AreEqual(100,  RomanNumber.Parse("C") );
            Assert.AreEqual(500,  RomanNumber.Parse("D") );
            Assert.AreEqual(1000, RomanNumber.Parse("M") );            
        }

        [TestMethod]
        public void RomanNumberParse2Digits()
        {
            Assert.AreEqual(4,   RomanNumber.Parse("IV") );
            Assert.AreEqual(15,  RomanNumber.Parse("XV") );
            Assert.AreEqual(900, RomanNumber.Parse("CM") );
            Assert.AreEqual(400, RomanNumber.Parse("CD") );
            Assert.AreEqual(55,  RomanNumber.Parse("LV") );
            Assert.AreEqual(40,  RomanNumber.Parse("XL") );
        }

        [TestMethod]
        public void RomanNumberParse3MoreDigits()
        {
            Assert.AreEqual(30,   RomanNumber.Parse("XXX") );
            Assert.AreEqual(401,  RomanNumber.Parse("CDI") );
            Assert.AreEqual(1999, RomanNumber.Parse("MCMXCIX") );
        }

        [TestMethod]
        public void RomanNumberParseInvalidDigit()
        {
            var ex = Assert.ThrowsException<ArgumentException>(() => { RomanNumber.Parse("ASDASZ");});
            var exs = new ArgumentException($"Invalid input data: A");
            
            Assert.AreEqual(exs.Message,ex.Message);
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
            Assert.AreEqual("N",romanNumber.ToString());
            
            romanNumber = new(90);
            Assert.AreEqual("XC",romanNumber.ToString());
            
            romanNumber = new(20);
            Assert.AreEqual("XX",romanNumber.ToString());
            
            romanNumber = new(1999);
            Assert.AreEqual("MCMXCIX",romanNumber.ToString());
            
        }

        [TestMethod]
        public void RomanNumberToStringParseCrossTest()
        {
            RomanNumber num = new();
            for (int n = 1; n <= 2022; ++n)
            {
                num.Value = n;
                Assert.AreEqual(n , RomanNumber.Parse(num.ToString()));
            }
        }
    }
}
