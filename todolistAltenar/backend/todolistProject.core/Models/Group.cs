namespace todolistProject.Core.Models
{
    public class Group
    {
        public Guid idGroup { get; set; }
        public User user { get; set; }
        public string titleGroup { get; set; }

        public Group (Guid idGroup, User user, string titleGroup)
        {
            this.idGroup = idGroup;
            this.user = user;
            this.titleGroup = titleGroup;
        }
    }
}
