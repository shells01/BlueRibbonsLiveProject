namespace Blue_Ribbon.Controllers
{
	 // NEW: Partial View for ReviewLogViewModel items-to-be-reviewed on customer dashboard (Tab 4, BRR Deals)
        public ActionResult ItemsToReview()
        {
            
            // Get customer Email from user's name
            string user = User.Identity.Name;
            
            string customerEmail = (from cust in db.Customers
                                    where cust.CustomerID.Equals(user)
                                    select cust.Email).First();

            // Pull data from ReviewLog where ReviewLog Email = Customer Email
            // TODO: log.CustomerReviewed == false, change to log.DisplayReview == false
            List<ReviewLog> Logs = (from log in db.ReviewLog
                                              where log.Email.Equals(customerEmail) && log.CustomerReviewed == false
                                              select log).ToList();

            List<Deal> Deals = db.Deal.ToList();

            // Create instance of ReviewLogViewModel, where Deal ASIN = ReviewLog ASIN
            List<ReviewLogViewModel> ItemsToReview = new List<ReviewLogViewModel>();

            for (var i = 0; i < Logs.Count; i++)
            {
                var vm = new ReviewLogViewModel(Deals.Where(a => a.ASIN == Logs[i].ASIN).FirstOrDefault(), Logs[i]);
                ItemsToReview.Add(vm);
            }

            return PartialView(ItemsToReview); 
        }

        // NEW: Partial View for ReviewLogViewModel reviews on customer dashboard (Tab 5, BRR Reviews)
        public ActionResult InHouseReviews()
        {

            // get customer's email from user's name
            string user = User.Identity.Name;

            string customerEmail = (from cust in db.Customers
                                    where cust.CustomerID.Equals(user)
                                    select cust.Email).First();

            // Pull data from ReviewLog model where ReviewLog Email = Customer Email
            // TODO: Change log.CustomerReviewed == true -> log.DisplayReview == true
            List<ReviewLog> Logs = (from log in db.ReviewLog
                                    where log.Email.Equals(customerEmail) && log.CustomerReviewed == true
                                    select log).ToList();

            List<Deal> Deals = db.Deal.ToList();

            // Create instance of ReviewLogViewModel, where Deal ASIN = ReviewLog ASIN
            List<ReviewLogViewModel> InHouseReviews = new List<ReviewLogViewModel>();

            for (var i = 0; i < Logs.Count; i++)
            {
                var vm = new ReviewLogViewModel(Deals.Where(a => a.ASIN == Logs[i].ASIN).FirstOrDefault(), Logs[i]);
                InHouseReviews.Add(vm);
            }

            return PartialView(InHouseReviews);
        }
}