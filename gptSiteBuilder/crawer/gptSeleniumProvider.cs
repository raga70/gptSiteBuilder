using OpenQA.Selenium;
using SeleniumUndetectedChromeDriver;
using System.Windows.Forms;
using Keys = OpenQA.Selenium.Keys;

namespace gptSiteBuilder;

public class gptSeleniumProvider : IGPTServiceProvider
{
    private bool isGPT4;
     IWebDriver _driver = UndetectedChromeDriver.Create(driverExecutablePath:@"assets\chromedriver.exe");

    public gptSeleniumProvider()
    {
        setup();
    }

    void setup()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Do you own chatGPT premium? (y/n)");
        string answer = Console.ReadLine();
        if(answer != "y" && answer != "n") setup();
        if(answer == "y") isGPT4 = true;
        else isGPT4 = false;
        
        
        _driver.Navigate().GoToUrl("https://chat.openai.com/");
       // Thread.Sleep(10000);
        // _driver.FindElement(By.XPath("""//*[@id="__next"]/div[1]/div[1]/div[4]/button[1]""")).Click(); //click on login
        // _driver.FindElement(By.XPath("""//*[@id="__next"]/div[1]/div[1]/div[4]/button[1]""")).Click(); //click on login

        Console.WriteLine("Please login to openAI and press enter when you are on an empty (new) chat page");
        Console.ReadLine();
        if(isGPT4) _driver.FindElement(pageElements.switchToGpt4_btn).Click();
    }

/// <summary>
/// chat history will be preserved
/// </summary>
/// <param name="input"></param>
/// <returns></returns>
    public string Prompt(string input)
    {
        _driver.FindElement(pageElements.chatBox).Click();
        _driver.FindElement(pageElements.chatBox).SendKeys(input);
        _driver.FindElement(pageElements.submitButton_btn).Click();
       Thread.Sleep(3000);
        while (_driver.FindElements(pageElements.StopGeneration_btn).Count >0) //TODO: warning might me skipped
        {
            Thread.Sleep(1000);
            if (_driver.FindElements(pageElements.StopGeneration_btn).Count == 0) break;
          
        }
        Thread.Sleep(1000);
        try
        {
            _driver.FindElements(pageElements.copyFullMassage_btn).Last().Click();
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e.Message);
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine("OPEN BROWSER AND/OR SCROLL DOWN  and press enter when you are done");
            Console.ReadLine();
            _driver.FindElements(pageElements.copyFullMassage_btn).Last().Click();

        }

        return ClipBoardExtractor.GetClipboardText();
    }

    public void ClearChat()
    {
        _driver.FindElement(pageElements.newChat_btn).Click();
    }
}