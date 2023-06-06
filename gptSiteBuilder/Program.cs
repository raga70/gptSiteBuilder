using gptSiteBuilder;




Console.WriteLine("Please enter the description of the website you want to generate: ");
string websiteDescription = Console.ReadLine();
Console.WriteLine("Please enter the name of the website you want to generate: ");
string websiteName = Console.ReadLine();

SiteBuilder siteBuilder = new SiteBuilder(new gptSeleniumProvider());
siteBuilder.Build(websiteName, websiteDescription);
Console.WriteLine("Done , probably, idk");