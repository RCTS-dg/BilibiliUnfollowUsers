using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BilibiliUnfollowUsers
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("disable-blink-features=AutomationControlled");
            ChromeDriver driver = new ChromeDriver(options);
            driver.Navigate().GoToUrl("https://www.bilibili.com/");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(5000);
            Console.WriteLine("登陆账号后输入uid继续...");
            string uid = Console.ReadLine();
            driver.Navigate().GoToUrl("https://space.bilibili.com/" + uid + "/fans/follow");
            for (int i = 0; i < 200; i++)
            {
                try
                {
                    for (int k = 0; k < 15; k++)
                    {
                        Console.WriteLine("第" + k + "次尝试取关");
                        var selectMenu = driver.FindElement(By.XPath("//*[contains(text(),'已关注') or contains(text(),'已互粉')]"));
                        Actions action = new Actions(driver);
                        action.MoveToElement(selectMenu).Perform();
                        //selectMenu.Click();
                        //Thread.Sleep(100);
                        Thread.Sleep(100);
                        var cancelButton = driver.FindElement(By.XPath("//*[contains(text(),'取消关注')]"));
                        cancelButton.Click();
                        Console.WriteLine("第" + k + "次成功取关");
                        Thread.Sleep(1000);
                    }
                    Console.WriteLine("切页");
                    driver.Navigate().GoToUrl("https://space.bilibili.com/" + uid + "/fans/follow");
                    Thread.Sleep(3000);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ERROR " + ex.ToString());
                    driver.Navigate().GoToUrl("https://space.bilibili.com/" + uid + "/fans/follow");
                    Thread.Sleep(3000);
                }

            }
        }
    }
}
