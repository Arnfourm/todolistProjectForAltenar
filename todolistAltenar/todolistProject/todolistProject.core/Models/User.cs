namespace todolistProject.Core.Models
{
    public class User
    {
        public int idUser { get; set; }
        public string username { get; set; }
        public string userEmail { get; set; }
        public string userPassword { get; set; }

        public User(int idUser, string username, string userEmail, string userPassword)
        {
            this.idUser = idUser;
            this.username = username;
            this.userEmail = userEmail;
            this.userPassword = userPassword;
        }
    }
}
