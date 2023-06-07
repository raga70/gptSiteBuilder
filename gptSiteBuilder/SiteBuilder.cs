namespace gptSiteBuilder;

public class SiteBuilder
{
    private IGPTServiceProvider _gptServiceProvider;

    // private  string websiteDescription;
    private string websiteName;

    public SiteBuilder(IGPTServiceProvider gptServiceProvider)
    {
        _gptServiceProvider = gptServiceProvider;
    }


    public void Build(string websiteName, string websiteDescription)
    {
        this.websiteName = websiteName;

        System.Diagnostics.Process.Start($"CMD.exe",
            $"/C npx create-react-app {websiteName} && cd {websiteName}  && npm install react-router-dom");


        string prePrompt = "You will be generating the code for a react app. the website is about: " +
                           websiteDescription + $"and is named {websiteName} " + "" +
                           " start by listing the page names  you will be generating, please as a response reply ONLY  with the page names enclosed in ``` and SEPARATED BY COMMAS";


        string resp = _gptServiceProvider.Prompt(prePrompt);
        string extractedPageNamesSTR =
            resp.Substring(resp.IndexOf("```") + 3, resp.LastIndexOf("```") - resp.IndexOf("```") - 3);
        string[] extractedPageNames_list =
            extractedPageNamesSTR.Split(",").Select(pageName => pageName.Trim()).ToArray();


        foreach (string pageName in extractedPageNames_list)
        {
            GeneratePage(pageName);
        }


        GenerateInitialBaseFilesForReact();
    }


    void GeneratePage(string pageName)
    {
        string prompt = "generate the code for the page " + pageName +
                        " by stating the file name and enclosing the code in ```  please do not generate any other output ";
        string resp = _gptServiceProvider.Prompt(prompt);
        string extractedCode =
            resp.Substring(resp.IndexOf("```") + 3, resp.LastIndexOf("```") - resp.IndexOf("```") - 3);
        extractedCode = extractedCode.Substring(extractedCode.IndexOf("\r\n"));
        SaveFile(extractedCode, pageName + ".js");
    }

    void GenerateInitialBaseFilesForReact()
    {
        string prompt =
            "generate the code for App.js by stating the file name and enclosing the code in ```  also include ./App.css  please do not generate any other output  generate only the code for the App.js file";
        string resp2 = _gptServiceProvider.Prompt(prompt);
        string extractedCode =
            resp2.Substring(resp2.IndexOf("```") + 3, resp2.LastIndexOf("```") - resp2.IndexOf("```") - 3);
        extractedCode = extractedCode.Substring(extractedCode.IndexOf("\r\n"));
        SaveFile(extractedCode, "App.js");

        prompt =
            "generate the code for App.css in it add really fancy css covering every element and component   by stating the file name and enclosing the code in ```  please do not generate any other output ";
        resp2 = _gptServiceProvider.Prompt(prompt);
        extractedCode = resp2.Substring(resp2.IndexOf("```") + 3, resp2.LastIndexOf("```") - resp2.IndexOf("```") - 3);
        extractedCode = extractedCode.Substring(extractedCode.IndexOf("\r\n"));
        SaveFile(extractedCode, "App.css");


        prompt =
            "generate the index.js file for the react app including react router by stating the file name and enclosing the code in ```  please do not generate any other output ";
        string resp = _gptServiceProvider.Prompt(prompt);
        extractedCode = resp.Substring(resp.IndexOf("```") + 3, resp.LastIndexOf("```") - resp.IndexOf("```") - 3);
        extractedCode = extractedCode.Substring(extractedCode.IndexOf("\r\n"));
        SaveFile(extractedCode, "index.js");
        // prompt = "generate the package.json file for the react app including react router by stating the file name and enclosing the code in ```  please do not generate any other output ";
        // resp = _gptServiceProvider.Prompt(prompt);
        // extractedCode = resp.Substring(resp.IndexOf("```") + 3, resp.LastIndexOf("```") - resp.IndexOf("```") - 3);
        // SaveFile(extractedCode, "package.json");
        //
        // prompt = "generate the package-lock.json file for the react app including react router by stating the file name and enclosing the code in ```  please do not generate any other output ";
        // resp = _gptServiceProvider.Prompt(prompt);
        // extractedCode = resp.Substring(resp.IndexOf("```") + 3, resp.LastIndexOf("```") - resp.IndexOf("```") - 3);
        // SaveFile(extractedCode, "package-lock.json");
    }


    void SaveFile(string content, string fileName)
    {
        if (!Directory.Exists(Path.Combine(websiteName, "src")))
        {
            Directory.CreateDirectory(Path.Combine(websiteName, "src"));
        }

        using (var file = new StreamWriter(Path.Combine(websiteName, "src", fileName)))
        {
            file.Write(content);
        }
    }
}