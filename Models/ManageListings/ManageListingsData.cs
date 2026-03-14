namespace MarsAdvancedReqnRollAutomation.Models.ManageListings
{
    public class ManageListingsData
    {
        public string TitlePrefix { get; set; } = "";
    }

    public class DeleteListingData : ManageListingsData
    {
        public string Confirm { get; set; } = "No";
    }
}
