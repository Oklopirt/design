using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;

namespace DependencyElimination
{
    internal class Page
    {
//        public string url { get; private set; }
//        public int count { get; private set; }
//
        public string log { get; private set; }
        public IEnumerable<string > URLs { get; private set; }
        public Page(string url)
        {
            log = url + "\n";
            var response = GetPageFromUrl(url);
            if (response.IsSuccessStatusCode)
            {
                var URLs = ExtractLinksFromHtml(GetStringResponse(response));
                log += URLs.Count();
                this.URLs = URLs;
            }
            else
            {
                log += String.Format(@"Error: {0} {1}", response.StatusCode, response.ReasonPhrase);
            }

        }

        private HttpResponseMessage GetPageFromUrl(string link)
        {
            using (var http = new HttpClient())
            {
                return http.GetAsync(link).Result;
            }
        }

        private string GetStringResponse(HttpResponseMessage response)
        {
            return response.Content.ReadAsStringAsync().Result;
        }

        private IEnumerable<string> ExtractLinksFromHtml(string page)
        {
            return Regex.Matches(page, @"\Whref=[""'](.*?)[""'\s>]")
                .Cast<Match>()
                .Select(match => match.Groups[1].Value);
        }
    }

    internal class Program
	{

	    private static void Main(string[] args)
		{
			var sw = Stopwatch.StartNew();
            int totalLinks = 0;
			    
            foreach (var url in GetURLs("http://habrahabr.ru/top/page", 1, 6))
			{
			    var page = new Page(url);
                Console.WriteLine(page.log);
                File.AppendAllLines("links.txt", page.URLs);
			}
			
			Console.WriteLine("Total URLs found: {0}", totalLinks);
			Console.WriteLine("Finished");
			Console.WriteLine(sw.Elapsed);
		}

	    private static IEnumerable<string> GetURLs(string url, int startPage, int endPage)
	    {
            for (int page = startPage; page <= endPage; page++)
            {
                yield return url + page;
            }
	    }
	}
}