namespace API
{
    public class ApiRepo
    {
        public static async Task<string> GetDataAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("x-functions-key", "lprgi_api_key_2023");
                try
                {
                    HttpResponseMessage response = await client.GetAsync("https://api-lprgi.natono.biz/api/GetConfig");
                    var jsonString = await response.Content.ReadAsStringAsync();
                    return jsonString;
                }
                catch (HttpRequestException ex)
                {
                    throw new Exception("An HTTP request exception occurred while getting data from the API.", ex);
                }
                catch (Exception ex)
                {
                    throw new Exception("An exception occurred while getting data from the API.", ex);
                }
            }
        }
    }
}
