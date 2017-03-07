using System;
using Blue_Ribbon.DAL;

namespace Blue_Ribbon.Models
{
    public class ReviewLog
    {
        /// <summary>
        /// This is the Id for each review, this is the primary key
        /// </summary>
        public int ReviewLogId { get; set; }

        /// <summary>
        /// The ASIN "Amazon Standard Identification Number"
        /// </summary>
        public string ASIN { get; set; }

        public int WebsiteAPIId { get; set; }
       /// <summary>
       /// The time when the customer recieves code
       /// </summary>
        public DateTime DateCodeGiven { get { return DateTime.Now; } }

        /*[DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime DateCodeGiven { get; set; } */
        public bool CustomerReviewed { get; set; }
        public bool AutomaticValidation { get; set; }

        /// <summary>
        /// Only triggered if 3 rating or less or foul language
        /// </summary>
        public bool AdminReviewed { get; set; }
        public bool DisplayReview { get; set; }

        /// <summary>
        /// A rating of 1-5, Five being the highest
        /// </summary>
        public int Rating { get; set; }

        /// <summary>
        /// The date the user created the review
        /// </summary>
        public DateTime DateReviewed { get { return DateTime.Now; } }

        /// <summary>
        /// This will be their email
        /// </summary>
        public string CustomerId { get; set; }
        public string ReviewSubject { get; set; }

        /// <summary>
        /// Minimum of 70 words, represents the customer review
        /// </summary>
        public string ReviewBody { get; set; }

        /// <summary>
        /// Yes is True
        /// </summary>
        public bool WouldBuyAgain { get; set; }

        /// <summary>
        /// Yes is True
        /// </summary>
        public bool RecToFriend { get; set; }

        private BRContext db = new BRContext();
    }

   

    
}