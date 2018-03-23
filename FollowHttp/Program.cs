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
            test.Login("http://www.redlist-db.be/index.php", "tybbow@gmail.com", "134679852");

            Console.WriteLine("Cookie OK");

            string result = test.RequestPage("http://www.redlist-db.be/index.php?page=panelmembre&type=compte");
            Console.WriteLine(result);
        }
    }
}
