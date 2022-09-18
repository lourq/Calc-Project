using CalcProject.App;
using System.Diagnostics.Metrics;

namespace TestProject1
{
    [TestClass]
    public class AppTest
    {
        private Resources Resources { get; set; } = new();
        public AppTest()
        {
            RomanNumber.Resources = Resources;
        }

        [TestMethod]
        public void CalcTest()
        {
            CalcProject.App.Calc calc = new(Resources);
            Assert.IsNotNull(calc);
        }

        [TestMethod]
        public void EvalTest()
        {
            CalcProject.App.Calc calc = new(Resources);
            Assert.IsNotNull(calc.EvalExpression("XI + IV"));
            Assert.AreEqual(new RomanNumber(10), calc.EvalExpression("XI - I"));
            Assert.ThrowsException<ArgumentException>(() => calc.EvalExpression("2 + 3"));
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

            var exc = Assert.ThrowsException<ArgumentException>(
                () => { RomanNumber.Parse("XXA"); });

            var exp = new ArgumentException(Resources.GetInvalidCharMessage('A'));

            Assert.AreEqual(exp.Message, exc.Message);

           
            Assert.AreEqual(
                Resources.GetInvalidCharMessage('2'),
                Assert.ThrowsException<ArgumentException>(
                    () => { RomanNumber.Parse("X2X"); }
                ).Message
            );

           
            Assert.IsTrue(
                Assert.ThrowsException<ArgumentException>(
                    () => { RomanNumber.Parse("2X X1"); }
                ).Message.StartsWith(
                    Resources.GetInvalidCharMessage(' ')[..7])
            );
        }

        [TestMethod]
        public void RomanNumberParseEmpty()
        {

            Assert.AreEqual(
                Resources.GetEmptyStringMessage(),
                Assert.ThrowsException<ArgumentException>(
                    () => RomanNumber.Parse("")
                ).Message
            );

            Assert.ThrowsException<ArgumentNullException>(
                () => RomanNumber.Parse(null!)
            );
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
            RomanNumber romanNumber = new();
            Assert.AreEqual("N", romanNumber.ToString());

            romanNumber = new(10);
            Assert.AreEqual("X", romanNumber.ToString());

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
            for (int n = 0; n <= 2022; ++n)
            {
                num.Value = n;
                Assert.AreEqual(n, RomanNumber.Parse(num.ToString()));
            }
        }

        [TestMethod]
        public void RomanNumberTypeTest()
        {
           
            RomanNumber rn1 = new() { Value = 10 };
            RomanNumber rn2 = rn1;
            Assert.AreSame(rn1, rn2);   
            
            RomanNumber rn3 = rn1 with { };  
            Assert.AreNotSame(rn3, rn1); 
            Assert.AreEqual(rn3, rn1);
            Assert.IsTrue(rn1 == rn3);

            RomanNumber rn4 = rn1 with { Value = 20 };

            Assert.AreNotSame(rn4, rn1);
            Assert.AreNotEqual(rn4, rn1);
            Assert.IsFalse(rn1 == rn4);
            Assert.IsFalse(rn1.Equals(rn4));
        }

        [TestMethod]
        public void RomanNumberNegative()
        {
            Assert.AreEqual(-10, RomanNumber.Parse("-X"));
            Assert.AreEqual(-400, RomanNumber.Parse("-CD"));
            Assert.AreEqual(-1900, RomanNumber.Parse("-MCM"));

            RomanNumber rn = new() { Value = -10 };
            Assert.AreEqual("-X", rn.ToString());
            rn.Value = -90;
            Assert.AreEqual("-XC", rn.ToString());

            Assert.ThrowsException<ArgumentException>(() => RomanNumber.Parse("M-CM"));
            Assert.ThrowsException<ArgumentException>(() => RomanNumber.Parse("M-"));
            Assert.ThrowsException<ArgumentException>(() => RomanNumber.Parse("-"));
            Assert.ThrowsException<ArgumentException>(() => RomanNumber.Parse("-N"));
            Assert.ThrowsException<ArgumentException>(() => RomanNumber.Parse("--X"));
            Assert.ThrowsException<ArgumentException>(() => RomanNumber.Parse("-C-X"));
            Assert.ThrowsException<ArgumentException>(() => RomanNumber.Parse("--"));
        }
    }

    [TestClass]
    public class OperationsTest
    {
        private Resources Resources { get; set; } = new();
        public OperationsTest()
        {
            RomanNumber.Resources = Resources;
        }

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

            Assert.ThrowsException<ArgumentNullException>(() => rn2.Add((RomanNumber)null!));

        }

        [TestMethod]
        public void AddValueTest()
        {
            var rn = new RomanNumber(10);
            Assert.AreEqual(20,  rn.Add(10).Value);
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
            RomanNumber rn5  = RomanNumber.Add(2, 3);
            RomanNumber rn8  = RomanNumber.Add(rn5, 3);
            RomanNumber rn10 = RomanNumber.Add("I", "IX");
            RomanNumber rn9  = RomanNumber.Add(rn5, "IV");
            RomanNumber rn13 = RomanNumber.Add(rn5, rn8);
       
            Assert.AreEqual(5, rn5.Value);
            Assert.AreEqual(8, rn8.Value);
            Assert.AreEqual(9, rn9.Value);
            Assert.AreEqual(10, rn10.Value);
            Assert.AreEqual(13, rn13.Value);
        }

        [TestMethod]
        public void SubstractionTest()
        {
            RomanNumber rn10 = new(10);
            RomanNumber rn3 = new(3);
            RomanNumber rn15 = new(15);
            RomanNumber rn_7 = new(-7);
            RomanNumber rn_9 = new(-9);

            Assert.AreEqual(-7, rn3.Sub(rn10).Value);
            Assert.AreEqual(5, rn15.Sub(rn10).Value);
            Assert.AreEqual(22, rn15.Sub(rn_7).Value);
            Assert.AreEqual(2, rn_7.Sub(rn_9).Value);
            Assert.AreEqual(7, rn10.Sub(rn3).Value);
            
        }
    }
}
