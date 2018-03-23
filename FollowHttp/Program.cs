using System;
using System.Net;

namespace FollowHttp
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Follow Http v1");
            Console.WriteLine("Follow Http With WebRequest");

            HttpWeb test = new HttpWeb();
			// On envoie le lien, le login et le mot de passe.
            test.Login("http://www.redlist-db.be/index.php", "tybbow@gmail.com", "134679852");

			// une fois que la connection est bonne, on peut continuer sur le site.
            string result = test.RequestPage("http://www.redlist-db.be/index.php?page=panelmembre&type=compte");
            Console.WriteLine(result);
        }
    }
}
