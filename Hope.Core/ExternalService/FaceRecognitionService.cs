using Hope.Core.Common;
using Hope.Core.Interfaces;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hope.Core.ExternalService
{
    public class FaceRecognitionService : IFaceRecognitionService
    {
        public async Task<Response> predict(string path)
        {
            string filePath = path;

            using var client = new HttpClient();
            using var content = new MultipartFormDataContent
            {
                { new StreamContent(File.OpenRead(filePath)), "file", Path.GetFileName(filePath) }
            };

            using var response = await client.PostAsync("https://883d-197-38-27-204.ngrok-free.app/upload", content);

            if (response.IsSuccessStatusCode)
            {

                var htmlContent = await response.Content.ReadAsStringAsync();

                var htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(htmlContent);
                var predictedLabelNode = htmlDocument.DocumentNode.SelectSingleNode("//h2[contains(text(), 'Recognized Name:')]");

                string label = predictedLabelNode.InnerText
                                                .Replace("Recognized Name:", "")
                                                .Trim();
                if(label=="UnKnown")
                  return await Response.FailureAsync("SomeThing wrong about image");

                return await Response.SuccessAsync(label);
            }
            else
            {
                return await Response.FailureAsync("Remote Server Error");
            }

        }
    }
}
