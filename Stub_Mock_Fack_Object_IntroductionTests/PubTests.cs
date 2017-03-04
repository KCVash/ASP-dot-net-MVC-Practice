using System;
using System.Collections.Generic;
using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace Stub_Mock_Fack_Object_Introduction.Tests
{
    [TestClass()]
    public class PubTests
    {
        [TestMethod()]
        public void Test_ChargeCustomer_Count()
        {
            //arrange
            ICheckInFee stubCheckInFee = MockRepository.GenerateStub<ICheckInFee>();
            Pub target = new Pub(stubCheckInFee);
            
            var customers = new List<Customer>
            {
                new Customer() {IsMale = true},
                new Customer() {IsMale = false},
                new Customer() {IsMale = false},
            };

            decimal expected = 1;

            //act
            var actual = target.CheckIn(customers);

            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_Income()
        {
            //arrange
            ICheckInFee stubCheckInFee = MockRepository.GenerateStub<ICheckInFee>();
            Pub target = new Pub(stubCheckInFee);

            stubCheckInFee.Stub(x => x.GetFee(Arg<Customer>.Is.Anything)).Return(100);

            var customers = new List<Customer>
            {
                new Customer() {IsMale = true},
                new Customer() {IsMale = false},
                new Customer() {IsMale = false},
            };

            var inComeBeforeCheckIn = target.GetInCome();
            Assert.AreEqual(0, inComeBeforeCheckIn);

            decimal expectedIncome = 100;

            //act
            var chargeCustomerCount = target.CheckIn(customers);
            var actualIncome = target.GetInCome();

            //assert
            Assert.AreEqual(expectedIncome, actualIncome);
        }

        [TestMethod]
        public void Test_CheckIn_Charge_Only_Male()
        {
            //arrange mock
            var customers = new List<Customer>();
            //2男1女
            var customer1 = new Customer() {IsMale = true};
            var customer2 = new Customer() {IsMale = true};
            var customer3 = new Customer() {IsMale = false};

            customers.Add(customer1);
            customers.Add(customer2);
            customers.Add(customer3);

            MockRepository mock = new MockRepository();
            ICheckInFee stubCheckInFee = mock.StrictMock<ICheckInFee>();

            using (mock.Record())
            {
                //期望呼叫ICheckInFee的GetFee()次數為2次
                stubCheckInFee.GetFee(customer1);

                LastCall
                    .IgnoreArguments()
                    .Return((decimal) 100)
                    .Repeat.Times(2);
            }

            using (mock.Playback())
            {
                var target = new Pub(stubCheckInFee);
                var count = target.CheckIn(customers);
            }
        }

        [TestMethod]
        public void Test_Friday_Charge_Customer_Count()
        {
            using (ShimsContext.Create())
            {
                System.Fakes.ShimDateTime.TodayGet = () =>
                {
                    //2012/10/19為Friday
                    return new DateTime(2012, 10, 19);
                };


                //arrange
                ICheckInFee stubCheckInFee = MockRepository.GenerateStub<ICheckInFee>();
                Pub target = new Pub(stubCheckInFee);

                stubCheckInFee.Stub(x => x.GetFee(Arg<Customer>.Is.Anything)).Return(100);

                var cusomers = new List<Customer>
                {
                    new Customer() {IsMale = true},
                    new Customer() {IsMale = false},
                    new Customer() {IsMale = false},
                };

                decimal expected = 1;

                //act
                var actual = target.CheckIn(cusomers);

                //assert
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod]
        public void Test_Saturday_Charge_Customer_Count()
        {

            using (ShimsContext.Create())
            {
                System.Fakes.ShimDateTime.TodayGet = () =>
                {
                    //2012/10/20為Saturday
                    return new DateTime(2012, 10, 20);
                };

                //arrange
                ICheckInFee stubCheckInFee = MockRepository.GenerateStub<ICheckInFee>();
                Pub target = new Pub(stubCheckInFee);

                stubCheckInFee.Stub(x => x.GetFee(Arg<Customer>.Is.Anything)).Return(100);

                var customers = new List<Customer>
                {
                    new Customer{ IsMale=true},
                    new Customer{ IsMale=false},
                    new Customer{ IsMale=false},
                };

                decimal expected = 3;

                //act
                var actual = target.CheckIn(customers);

                //assert
                Assert.AreEqual(expected, actual);
            }
        }
    }
}