using System;
using System.Collections.Generic;

namespace Linq_Lambda_Introduction
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("I am for Factory A");

            var momGeneration = new List<Mom> { new Mom_CSharp_1(), new Mom_CSharp_2(), new Mom_CSharp_3() };

            foreach (var mom in momGeneration)
            {
                mom.Cooking(100);
            }

            Console.ReadKey();
        }
    }

    #region C#1.0的媽媽，透過委派，assign instance的方式or靜態方式給委派變數，執行委派
    public class Mom_CSharp_1 : Mom
    {
        public delegate Salt GetSalt(int money); //定義委派的簽章

        public override void Cooking(int money)
        {
            #region 宣告變數，assign joey的BuySalt()給變數
            var joey = new Joey();
            GetSalt getSalt = new GetSalt(joey.BuySalt);
            #endregion
            Console.WriteLine("C#1.0的媽媽，花{1}元，透過Joey的BuySalt()結果，拿{0}炒菜", getSalt(money).Name, money.ToString());
                                                                                        //--------------------
                                                                                        //實際執行委派   
        }
    }

    public class Joey
    {
        #region 實際執行的方法內容
        public Salt BuySalt(int money)
        {
            if (money > 100)
            {
                return new RockSalt();
            }
            else
            {
                return new SeaSalt();
            }
        }
        #endregion
    }
    #endregion

    #region C#2.0的媽媽，透過匿名委派
    public class Mom_CSharp_2 : Mom
    {
        public delegate Salt GetSalt(int money); //定義委派的簽章

        public override void Cooking(int money)
        {
            GetSalt getSalt = new GetSalt
                (   
                    delegate (int moneyForSalt)     //直接撰寫委派方法的內容，assign給getSalt變數
                    {
                        if (moneyForSalt > 100)
                        {
                            return new RockSalt();
                        }
                        else
                        {
                            return new SeaSalt();
                        }
                    }
                );

            Console.WriteLine("C#2.0的媽媽，花{1}元，透過Joey的BuySalt()結果，拿{0}炒菜", getSalt(money).Name, money.ToString());
                                                                                          //--------------------
                                                                                          //實際執行委派   
        }
    }
    #endregion

    #region C#3.0的媽媽，透過Lambda
    public class Mom_CSharp_3 : Mom
    {
        public delegate Salt GetSalt(int money);

        public override void Cooking(int money)
        {
            GetSalt getSalt = new GetSalt(
                moneyForSalt =>                 //1.不再透過delegate關鍵字宣告，而透過=>代表
                {                               //2.可以不必宣告參數型別(除非推斷不出來)
                    if (moneyForSalt > 100)     //3.只有一個參數，可以不用加小括號()
                    {
                        return new RockSalt();
                    }
                    else
                    {
                        return new SeaSalt();
                    }
                });

            Console.WriteLine("C#1.0的媽媽，花{1}元，透過Joey的BuySalt()結果，拿{0}炒菜", getSalt(money).Name, money.ToString());
                                                                                         //--------------------
                                                                                         //實際執行委派 
        }
    }
    #endregion
}
