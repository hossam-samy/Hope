using Hope.Core.Interfaces;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hope.Core.ExternalService
{
    public class RecommendationService:IRecommendationService
    {
         public async Task<int> predict(double longitude, double latitude)
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

                
                if (predictedLabelNode != null)
                {
                    string label = predictedLabelNode.InnerText
                                                    .Replace("Predicted Label:", "")
                                                    .Trim();

                   return int.Parse(label);
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                return int.Parse(response.StatusCode.ToString());
            }
            return 0;
        }

       
    }
}
