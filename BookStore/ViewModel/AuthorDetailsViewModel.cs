using DataLayer.Entites;

namespace BookStore.ViewModel
{
    public class AuthorDetailsViewModel
    {
        public Author Author { get; set; }
        public List<Book> Books { get; set; }
    }
}
