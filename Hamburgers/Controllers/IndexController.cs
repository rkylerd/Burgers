using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hamburgers.Controllers
{
    public class IndexController : Controller
    {
        // GET: Index
        public ActionResult Index()
        {
            //Queue represents customers in a line to order burgers
            Queue<string> customerLine = new Queue<string>();

            //maps customer names to burgers eaten count
            Dictionary<string, int> customerInfo = new Dictionary<string, int>();
            int greatestNumber = 0;

            //simulate the last 100 people in a line ordering burgers 
            //by adding random names to a queue
            for (int lineIndex = 0; lineIndex < 100; lineIndex++) {
                customerLine.Enqueue(randomName());
            }

            //iterate through the customers in line and
            //add a random number (representing burgers eaten) to the burgers eaten count
            foreach (string name in customerLine) {

                //if the customer has already been through the line
                if (customerInfo.ContainsKey(name))
                {   
                    int newNum = randomNumberInRange();

                    //add to the burgers eaten for the given customer
                    customerInfo[name] += newNum;
                    
                    //if the new random number is greater than the previously greatest number recorded,
                    //update the greatest number variable
                    greatestNumber = customerInfo[name] > greatestNumber ? customerInfo[name] : greatestNumber;
                }
                else
                {
                    //new customer. initialize burgers eaten to 0
                    customerInfo.Add(name, 0);
                }
            }

            //make these variables available in index.cshtml
            ViewBag.customerLine = customerLine;
            ViewBag.customerInfo = customerInfo;
            ViewBag.mostBurgersEaten = greatestNumber;

            return View();
        }

        public static Random random = new Random();

        public static string randomName()
        {
            string[] names = new string[8] { "Dan Morain", "Emily Bell", "Carol Roche", "Ann Rose", "John Miller", "Greg Anderson", "Arthur McKinney", "Joann Fisher" };
            int randomIndex = Convert.ToInt32(random.NextDouble() * 7);
            return names[randomIndex];
        }

        public static int randomNumberInRange()
        {
            return Convert.ToInt32(random.NextDouble() * 20);
        }
    }
}