using System;
using System.Collections.Generic;

namespace Stub_Mock_Fack_Object_Introduction
{
    public interface ICheckInFee
    {
        decimal GetFee(Customer customer);
    }

    public class Customer
    {
        public bool IsMale { get; set; }
        public int Seq { get; set; }      
    }

    public class Pub
    {
        private ICheckInFee _checkInFee;
        private decimal _inCome = 0;

        public Pub(ICheckInFee checkInFee)
        {
            _checkInFee = checkInFee;
        }

        /// <summary>
        /// 入場
        /// </summary>
        /// <param name="customers"></param>
        /// <returns>收費的人數</returns>
        public int CheckIn(List<Customer> customers)
        {
            var result = 0;

            foreach (var customer in customers)
            {
                var isFemale = !customer.IsMale;
                //for fake
                var isLadyNight = DateTime.Today.DayOfWeek == DayOfWeek.Friday;
                //星期五女生免費入場
                if (isLadyNight && isFemale)
                {
                    continue;
                }
                else
                {
                    //for stub, validate status: income value
                    //for mock, validate only male
                    _inCome += _checkInFee.GetFee(customer);

                    result++;
                }
            }

            //for stub, validate return value
            return result;
        }

        public decimal GetInCome()
        {
            return _inCome;
        }
    }
}
