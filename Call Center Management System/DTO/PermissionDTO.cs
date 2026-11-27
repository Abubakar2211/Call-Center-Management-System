using System.Collections.ObjectModel;

namespace Call_Center_Management_System.DTO
{
    public class CreatePermissionDTO
    {
        public string role { get; set; }
        public Collection Permission { get; set; }
    }
}
