# LiveProject
Samples of some code from my Live Project (open source)
Live Project: Adding a new internal review feature to the Blue Ribbons Review website (blueribbonsreview.com)

<b>DashboardIndex.cshtml</b>
Customer dashboard for the Blue Ribbons website (incomplete).
- made some visual revisions
- added 2 new tabs for the new internal review feature (BRR Deals, and BRR Reviews), to show the partial views ItemsToReview and InHouseReviews

<b>DashboardController.cs</b>
Controller for customer dashboard
- added 2 PartialViews to the customer dashboard, InHouseReviews and ItemsToReview

<b>InHouseReviews.cshtml</b>
Partial view for the items that the customer reviewed
- created a partial view to display the internal customer reviews on the customer dashboard

<b>ItemsToReview.cshtml</b>
Partial view for items that the customer needs to review
- created a partial view to display the items that need reviews on the customer dashboard

<b>ReviewLog.cs</b>
ReviewLog Model, database for local Blue Ribbons reviews
- worked in a team of three to write and scaffold a ReviewLog model. 

<b>ReviewLogViewModel.cs</b>
ViewModel for the ReviewLog and Deal models.
- worked in a team of three to add some properties to the ViewModel








