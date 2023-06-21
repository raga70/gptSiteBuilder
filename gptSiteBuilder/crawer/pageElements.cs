using OpenQA.Selenium;

namespace gptSiteBuilder;

public static class pageElements
{
   public static By chatBox = By.XPath("""//*[@id="prompt-textarea"]""");
   //                                               
   public static By submitButton_btn = By.XPath("""//*[@id="__next"]/div[1]/div/div/main/div[3]/form/div/div/button""");
   public static By copyCode_btn = By.XPath("//*[text()='Get started free']");//multiple
   public static By copyFullMassage_btn = By.XPath("//button[@class='flex ml-auto gap-2 rounded-md p-1 hover:bg-gray-100 hover:text-gray-700 dark:text-gray-400 dark:hover:bg-gray-700 dark:hover:text-gray-200 disabled:dark:hover:text-gray-400']");
   public static By switchToGpt4_btn =
      By.XPath("/html/body/div[1]/div[2]/div[2]/div/main/div[2]/div/div/div[1]/div/div/ul/li[2]/button/div");
   public static By newChat_btn = By.XPath("""//*[@id="__next"]/div[2]/div[1]/div/div/div/nav/div[1]/a""");
   public static By StopGeneration_btn = By.XPath("//*[text()='Stop generating']");
}