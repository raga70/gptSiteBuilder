namespace gptSiteBuilder;

public interface IGPTServiceProvider
{
    string Prompt(string input);
    void ClearChat();   
}