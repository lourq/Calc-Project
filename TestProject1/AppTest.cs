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
        public void RomanNumberParse()
        {
            Assert.AreEqual(RomanNumber.Parse("I") , 1 , "I == 1");
            Assert.AreEqual(RomanNumber.Parse("IV"), 4 , "IV == 4");
            Assert.AreEqual(RomanNumber.Parse("V") , 5 , "V == 5");
            Assert.AreEqual(RomanNumber.Parse("XV") , 15);
        }
        
        [TestMethod]
        public void RomanNumberParseN()
        {
            Assert.AreEqual("Invalid char N", Assert.ThrowsException<ArgumentException>(() => RomanNumber.Parse("XN")).Message);
            Assert.AreEqual("Invalid char N", Assert.ThrowsException<ArgumentException>(() => RomanNumber.Parse("XNX")).Message);
            Assert.AreEqual("Invalid char N", Assert.ThrowsException<ArgumentException>(() => RomanNumber.Parse("NX")).Message);
            Assert.AreEqual("Invalid char N", Assert.ThrowsException<ArgumentException>(() => RomanNumber.Parse("NVII")).Message);
            Assert.AreEqual("Invalid char N", Assert.ThrowsException<ArgumentException>(() => RomanNumber.Parse("NN")).Message);
        }
}
