namespace gptSiteBuilder;

public static class ClipBoardExtractor
{
    public static string GetClipboardText()
    {
        string result = string.Empty;
        Thread staThread = new Thread(
            delegate ()
            {
                try
                {
                    result = Clipboard.GetText();
                }
                catch (Exception ex)
                {
                    // Handle exceptions here if needed
                }
            });
        staThread.SetApartmentState(ApartmentState.STA);
        staThread.Start();
        staThread.Join();
        return result;
    }

    

    
}