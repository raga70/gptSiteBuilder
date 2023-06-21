using System.Diagnostics;
using gptSiteBuilder;




Console.WriteLine("Please enter the description of the website you want to generate: ");
string websiteDescription = Console.ReadLine();
Console.WriteLine("Please enter the name of the website you want to generate: ");
string websiteName = Console.ReadLine();

SiteBuilder siteBuilder = new SiteBuilder(new gptSeleniumProvider());

await Task.Run(() => Process.Start($"CMD.exe",
    $"/C npx create-react-app {websiteName} && cd {websiteName}  && npm install react-router-dom@5.0.0"));
siteBuilder.Build(websiteName, websiteDescription);
Console.WriteLine("Done , probably, idk");
