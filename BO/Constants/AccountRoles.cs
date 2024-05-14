namespace BO.Constants
{
    public class Role
    {
        public required string RoleName { get; set; }

        public int Value { get; set; }
    }

    public static class AccountRoles
    {
        /// <summary>
        /// Staff 
        /// </summary>
        public const int StaffRole = 1;

        /// <summary>
        /// Lecturer
        /// </summary>
        public const int LecturerRole = 2;
    }
}
