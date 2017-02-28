using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace How_To_Write_A_Unit_Test_Code.Tests
{
    [TestClass()]
    public class CalculatorTests
    {
        [TestMethod()]
        public void AddTest()
        {
            //3A原則:
            //1.Arrange:初始化目標物件、相依物件、方法參數、預期結果，或是預期與相依物件的互動方式
            //2.Act:呼叫目標物件的方法
            //3.Assert:驗證是否符合預期

            //Arrange
            Calculator target = new Calculator();
            int firstNumber = 1;
            int secondNumber = 2;
            int expected = 3;
            int actual;

            //Act
            actual = target.Add(firstNumber, secondNumber);

            //Assert
            Assert.AreEqual(expected, actual);
        }
    }
}