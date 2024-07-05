namespace DesafioUBC.Web.UI.Application.Responses
{
    public class ResponseLoginUserIdentity
    {
        public string AccessToken { get; set; }

        public double ExpiresIn { get; set; }

        public string Message { get; set; }

        public UserToken UserToken { get; set; }
    }
    public class UserToken
    {
        /// <summary>
        /// UserId
        /// </summary>
        public string Id { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }

        //[NotMapped]
        public string FileNameImage { get; set; }

        public IEnumerable<UserClaim> Claims { get; set; }
    }

    public class UserClaim
    {
        public string Value { get; set; }

        public string Type { get; set; }
    }
}
