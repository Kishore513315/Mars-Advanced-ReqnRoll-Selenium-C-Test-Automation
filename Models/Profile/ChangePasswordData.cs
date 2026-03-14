namespace MarsAdvancedReqnRollAutomation.Models.Profile
{
    public class ChangePasswordData
    {
        public string Email { get; set; } = "";
        public string OldPassword { get; set; } = "";
        public string NewPassword { get; set; } = "";
        public string ExpectedToast { get; set; } = "";
    }
}
