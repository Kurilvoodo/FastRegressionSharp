namespace FRS.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public byte[] HashPassword { get; set; }
    }
}