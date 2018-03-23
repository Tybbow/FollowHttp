using System;
using System.Net;
using System.Text;
using System.IO;

namespace FollowHttp
{
    public class HttpWeb
    {

        string cHeader;

        public HttpWeb()
        {
        }

        public void Login(string url, string login, string password)
        {
            ASCIIEncoding encoding = new ASCIIEncoding();
			// Postdata peut varifier en fonction des informations besoins à la connection.
            string postData = "email=" + login + "&password=" + password + "&connect=1&x=11&y=13";
            byte[] postDataBytes = encoding.GetBytes(postData);


            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
			// On ajoute les méthodes et les informations pour l'entête HTTP. Mieux vaut en rajouter le plus possible.
			// Tu peux voir les requêtes avec Wireshark (Attention au HTTPS), burpsuite ou le meilleur Fiddler pour Windows.
            httpWebRequest.Method = "POST";
            httpWebRequest.Host = "www.redlist-db.be";
            httpWebRequest.ContentType = "application/x-www-form-urlencoded";
            httpWebRequest.Referer = "http://www.redlist-db.be/index.php";
            httpWebRequest.UserAgent = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_12_6) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/65.0.3325.162 Safari/537.36";
            httpWebRequest.Headers.Add("Accept-Language", "en-US,en;q=0.9,fr;q=0.8");
            httpWebRequest.ServicePoint.Expect100Continue = false;
            httpWebRequest.ContentLength = postDataBytes.Length;
            httpWebRequest.AllowAutoRedirect = false;

            using (var stream = httpWebRequest.GetRequestStream())
            {
                stream.Write(postDataBytes, 0, postDataBytes.Length);
                stream.Close();
            }

			//Attention, le HttpWebResponse doit être déclaré avec les informations envoyés.
			
            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
			
			// Seul moyen que j'ai trouvé de récupérer le cookie.
            cHeader = httpWebResponse.Headers.Get("Set-Cookie");
            Console.WriteLine(" Cookie recu :");
            Console.WriteLine(cHeader.ToString());
        }

        public  string RequestPage(string url)
        {
            ASCIIEncoding encoding = new ASCIIEncoding();

            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            CookieContainer TempCookie = new CookieContainer();

			// On part sur une méthode GET pour la suite des pages.
            httpWebRequest.Method = "GET";
            httpWebRequest.Host = "www.redlist-db.be";
            httpWebRequest.ContentType = "application/x-www-form-urlencoded";
            httpWebRequest.UserAgent = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_12_6) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/65.0.3325.162 Safari/537.36";
            httpWebRequest.Headers.Add("Accept-Language", "en-US,en;q=0.9,fr;q=0.8");
            TempCookie.SetCookies(new Uri("http://www.redlist-db.be"), cHeader);
            httpWebRequest.CookieContainer = TempCookie;
            httpWebRequest.UseDefaultCredentials = true;
            httpWebRequest.AllowAutoRedirect = false;

            string responseString;

			// HttpWebResponse toujours après les informations.
            HttpWebResponse HttpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            Stream dataStream = HttpWebResponse.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            responseString = reader.ReadToEnd();
            reader.Close();
            HttpWebResponse.Close();

            return (responseString);
        }
    }
}
