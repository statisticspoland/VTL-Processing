using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Core.App
{
    public static class PlantUmlVisualizer
    {
        private static readonly HttpClient client = new HttpClient();

        private static readonly string url = "http://plantuml.com/plantuml"; // we use our internal adress

        public static async Task<string> PlantUmlPostAsync(string source, bool openInBrowser = false, string svgFilename = null, string path = null)
        {
            try
            {
                string fullUrl = await PostGetUrl(url, source);

                if (openInBrowser)
                {
                    Process.Start("cmd.exe", $"/C start {fullUrl}");
                }

                if (svgFilename != null)
                {
                    await GetDiagramToFile(fullUrl, path, svgFilename);
                }

                return fullUrl;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Response error: {ex.Message}");
                Debug.WriteLine($"\nResponse error: {ex.Message}");
                return source;
            }
        }

        private static async Task<string> PostGetUrl(string url, string source)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("text", new string(source.ToCharArray()));
            var content = new FormUrlEncodedContent(dic);

            client.DefaultRequestHeaders.Add("text", string.Empty);

            Console.WriteLine($"POST to {url}/form");
            var response = await client.PostAsync($"{url}/form", content);

            CheckResponse(response);

            return $"{url}/svg/{response.RequestMessage.RequestUri.Segments.Last()}/";
        }

        private static async Task GetDiagramToFile(string fullUrl, string path, string svgFilename)
        {
            if (fullUrl.Length <= 100)
            {
                Console.WriteLine($"GET to {fullUrl}");
            }
            else
            {
                Console.WriteLine($"GET to {fullUrl.Substring(0, 99)}...");
            }
            var response = await client.GetAsync(fullUrl);

            CheckResponse(response);

            string directoryPath;

            if (path != null)
            {
                directoryPath = path;
            }
            else
            {
                directoryPath = Directory.GetCurrentDirectory();
            }

            string combinedPath = Path.Combine(directoryPath, "result", $"{svgFilename}.svg");

            Directory.CreateDirectory(Path.GetDirectoryName(combinedPath));
            File.WriteAllText(combinedPath, await response.Content.ReadAsStringAsync());

            Console.WriteLine(combinedPath);
        }

        private static void CheckResponse(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(((int)response.StatusCode).ToString());
            }

            Console.WriteLine($"Response OK");
        }
    }
}
