using DataLayer.Entites;

namespace BookStore.ViewModel
{
    public class AddBookViewModel
    {
        public List<Author>? Authors { get; set; }
        public string? Title { get; set; }
        public double Price { get; set; }
        public int AuthorId { get; set; }
    }
}
