using CalcProject.App;

namespace TestProject1
{
    [TestClass]
    public class AppTest
    {
        [TestMethod]
        public void CalcTest()
        {
            // Testing Calc ...

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
    }
}

// TDD - Test Drive Dev
/*
 TDD - Test Driven Development - розроблення кероване тестами
 * Суть - спочатку пишуться тести, потім створюється ПЗ, яке
 * задовольняє цим тестам. XP додає - мінімальним шляхом (без
 *  "запасів")
 */