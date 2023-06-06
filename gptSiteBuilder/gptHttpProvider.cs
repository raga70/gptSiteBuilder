namespace gptSiteBuilder;

public class gptHttpProvider : IGPTServiceProvider
{
  
        private readonly HttpClient httpClient;
        
        public gptHttpProvider()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://localhost:8081");
        }
        
        public string Prompt(string input)
        {
            var response = SendPostRequestAsync("/prompt", input).Result;
            return response;
        }
        
        public void ClearChat()
        {
            SendPostRequestAsync("/clear", "").Wait();
        }
        
        private async Task<string> SendPostRequestAsync(string endpoint, string input)
        {
            try
            {
                var content = new StringContent(input);
                var response = await httpClient.PostAsync(endpoint, content);
                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();
                return responseContent;
            }
            catch (Exception ex)
            {
                // Handle any exceptions here
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }
    


}