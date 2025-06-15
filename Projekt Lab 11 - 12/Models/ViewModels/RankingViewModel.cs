namespace Projekt_Lab_11___12.Models.ViewModels
{
    public class RankingViewModel
    {
        public double Iron { get; set; }
        public double Gold { get; set; }
        public double Diamond { get; set; }
        public double Emerald { get; set; }
        public List<UserViewModel> UserList { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}
