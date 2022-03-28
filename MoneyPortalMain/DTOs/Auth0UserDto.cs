namespace MoneyPortalMain.DTOs
{
    public class Auth0UserDto
    {
        public string _id { get; set; }
        public string email { get; set; }
        public bool email_verified { get; set; }
        public string given_name { get; set; }
        public string family_name { get; set; }
        public string name { get; set; }
        public string nickname { get; set; }
    }
}
