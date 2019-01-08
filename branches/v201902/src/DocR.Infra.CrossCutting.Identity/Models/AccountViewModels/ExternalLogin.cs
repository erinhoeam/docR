namespace DocR.Infra.CrossCutting.Identity.Models.AccountViewModels
{
    public class ExternalLogin
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string AccessToken { get; set; }
    }
}