using Hope.Core.Common;
using Hope.Core.Interfaces;
using HtmlAgilityPack;

namespace Hope.Core.ExternalService
{
    public class RecommendationService:IRecommendationService
    {
         public async Task<Response> predict(double longitude, double latitude)
        {

            string baseUrl = "https://hope-recommendation-app.onrender.com"; 

       
            using var httpClient = new HttpClient();
            var postData = new Dictionary<string, string>
               {
                    { "longitude", $"{longitude}" },
                    { "latitude", $"{longitude}" } 
               };

      
            var response = await httpClient.PostAsync($"{baseUrl}/predict", new FormUrlEncodedContent(postData));

        
            if (response.IsSuccessStatusCode)
            {
         
                var htmlContent = await response.Content.ReadAsStringAsync();


                var htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(htmlContent);

               
                var predictedLabelNode = htmlDocument.DocumentNode.SelectSingleNode("//h2[contains(text(), 'Predicted Label:')]");

               
                string label = predictedLabelNode.InnerText
                                                    .Replace("Predicted Label:", "")
                                                    .Trim();

                return await Response.SuccessAsync(label);
                
            }
            else
            {
                return await Response.FailureAsync("Remote Server Error");
            }
        }

       
    }
}
