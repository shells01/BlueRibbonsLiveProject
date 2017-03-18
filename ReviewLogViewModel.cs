using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Blue_Ribbon.Models;
using System.ComponentModel.DataAnnotations;

namespace Blue_Ribbon.ViewModels
{
    public class ReviewLogViewModel
    {
        #region Deal properties

        public int DealID { get; set; }
        /// <summary>
        /// ProductName is the mapped deal.Name of the item, ex: "Huggies Snug and Dry Disposable Baby Diapers"
        /// </summary>
        public string ProductName { get; set; }
        public bool OpenCampaign { get; set; }
        public string Category { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        /// <summary>
        /// This is a string in dB => double.Parse() which represents Blue Ribbon's Special Price, ex: $3.44
        /// </summary>
        [DisplayFormat(DataFormatString = "{0:C0}")]
        public double RetailPrice { get; set; }
        public string VendorPurchaseURL { get; set; }
        /// <summary>
        /// This is the vendor's MSRP mapped to OriginalPriceNumerical (which is of data type double), ex: $29.99
        /// </summary>
        [DisplayFormat(DataFormatString = "{0:C0}")]
        public double OriginalPrice { get; set; }
        /// <summary>
        /// Numerical value of the affiliate we are working with for the deal
        /// </summary>
        public int WebsiteAPIDataId { get; set; }
        public bool? ListDealFlag { get; set; }
        public bool? Featured { get; set; }
        //public Vendor VendorInfo { get; set; }
        public string GetVendorsURL { get; set; }
        #endregion


        #region ReviewLog properties

        public int ReviewLogId { get; set; }
        public string ASIN { get; set; }
        public int WebsiteAPIId { get; set; }
        /// <summary>
        /// When customer presses "Get coupon code" for the in-house reviews system.
        /// </summary>
        public DateTime SelectedDate { get; set; }
        public bool? CustomerReviewed { get; set; }
        public bool? AutomaticValidation { get; set; }
        public bool? NeedsAdminReview { get; set; }
        public bool? AdminReviewed { get; set; }
        //DisplayReview defaults to false until review is submited and we test it with our constraints for a pass
        public bool DisplayReview { get; set; }
        public int? Rating { get; set; }
        public DateTime? DateReviewed { get; set; }
      
        public string Email { get; set; }
        public string ReviewSubject { get; set; }
        public string ReviewBody { get; set; }
        public bool? WouldBuyAgain { get; set; }
        public bool? RecToFriend { get; set; }
        //The Deal model includes a "Vendor_VendorId" record, but seems to not be used
        //public string Vendor_VendorId { get; set; }
        #endregion
        /// <summary>
        /// Gives timespan between today and day code was given
        /// </summary>
        public TimeSpan DaysSinceCodeGiven { get; set; }
        [Display(Name = "Total Discount")]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        public double CalculatedDiscount { get; set; }

        /// <summary>
        /// Default ReviewLog constructor
        /// </summary>
        public ReviewLogViewModel()
        {
        // No code goes here
        }


        //TODO
        /// <summary>
        /// ViewModel passed to the Index and will be utilized in CRUD functionality
        /// </summary>
        /// <param name="deal">Deal Model</param>
        /// <param name="review">Individual review records in ReviewLog Model</param>
        public ReviewLogViewModel(Deal deal, ReviewLog review)
        {
            #region Deal model

            DealID = deal.DealID;
            ProductName = deal.Name;
            OpenCampaign = deal.OpenCampaign;
            Category = deal.Category;
            ImageUrl = deal.ImageUrl;
            Description = deal.Description;
            //RetailPrice stored as strin
            RetailPrice = double.Parse(deal.RetailPrice);
            VendorPurchaseURL = deal.VendorsPurchaseURL;
            OriginalPrice = deal.OriginalPriceNumerical;
            WebsiteAPIDataId = deal.WebsiteAPIDataId;
            ListDealFlag = deal.ListDealFlag;
            Featured = deal.Featured;
            CalculatedDiscount = OriginalPrice - RetailPrice;
            //VendorInfo = deal.Vendor;
            GetVendorsURL = deal.GetVendorsUrl; 
            #endregion


            #region ReviewLog model

            ReviewLogId = review.ReviewLogId;
            ASIN = review.ASIN;
            WebsiteAPIId = review.WebsiteAPIId;
            SelectedDate = DateTime.Now.Date;
            CustomerReviewed = review.CustomerReviewed;
            AutomaticValidation = review.AutomaticValidation;
            NeedsAdminReview = review.NeedsAdminReview;
            AdminReviewed = review.AdminReviewed;
            DisplayReview = review.DisplayReview;
            Rating = review.Rating;
            DateReviewed = DateTime.Now;
            Email = review.Email;
            ReviewSubject = review.ReviewSubject;
            ReviewBody = review.ReviewBody;
            WouldBuyAgain = review.WouldBuyAgain;
            RecToFriend = review.RecToFriend;
            #endregion
        }
     
    }
}