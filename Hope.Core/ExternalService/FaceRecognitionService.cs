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
        public async Task<string> predict(string path)
        {
            string filePath = path; 

            using (var client = new HttpClient())
            {
                using (var content = new MultipartFormDataContent())
                {
                    content.Add(new StreamContent(File.OpenRead(filePath)), "file", Path.GetFileName(filePath));

                    using (var response = await client.PostAsync("https://883d-197-38-27-204.ngrok-free.app/upload", content))
                    {
                      
                            var htmlContent = await response.Content.ReadAsStringAsync();

                            var htmlDocument = new HtmlDocument();
                            htmlDocument.LoadHtml(htmlContent);
                            var predictedLabelNode = htmlDocument.DocumentNode.SelectSingleNode("//h2[contains(text(), 'Recognized Name:')]");
                           
                                string label = predictedLabelNode.InnerText
                                                                .Replace("Recognized Name:", "")
                                                                .Trim();

                                return label;

                            
                    }
                }
            }
           
        }
    }
}
