namespace rest_api_custom_jwt_auth.Models
{
    public class UserRole
    {
        public int IdUser { get; set; }
        public int IdRole { get; set; }

        public virtual User IdUserNavigation { get; set; }
        public virtual Role IdRoleNavigation { get; set; }
    }
}
