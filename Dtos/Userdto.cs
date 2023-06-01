namespace ZwajApp.API.Dtos
{
    public class Userdto
    {
        public int UserId { get; set; }
        public string Gender { get; set; }
        public bool Likees { get; set; } = false;
        public bool Likers { get; set; } = false;
        public int MinAge { get; set; } = 18;
        public int MaxAge { get; set; } = 99;
       // public string OrderBy { get; set; }

    }
}