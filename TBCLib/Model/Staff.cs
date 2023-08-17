namespace Model
{
    public class Staff
    {
        public int? StaffId { set; get; }
        public string StaffName { set; get; }
        public string Password { set; get; }

        public Staff()
        {
            StaffName = "";
            Password = "";
        }

    }
}