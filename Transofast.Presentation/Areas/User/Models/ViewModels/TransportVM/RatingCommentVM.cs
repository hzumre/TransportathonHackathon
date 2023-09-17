namespace Transofast.Presentation.Areas.User.Models.ViewModels.TransportVM
{
    public class RatingCommentVM
    {
        public int TransportId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; } = string.Empty;
    }
}
