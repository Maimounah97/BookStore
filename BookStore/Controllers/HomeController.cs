using BookStore.Models;
using BookStore.ViewModel;
using BusinessLogicLayer;
using DataLayer;
using DataLayer.Entites;
using DataLayer.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BookStore.Controllers
{
    public class HomeController : Controller
    {
        //private readonly IBaseRepository<Author> _authorsRepository;
        //private readonly IBooksRepository _booksRepository;
        private readonly IUnitOfWork _unitOfWork;

        //public HomeController(IBaseRepository<Author> authorsRepository , IBooksRepository booksRepository)
        //{
        //    _authorsRepository = authorsRepository;
        //    _booksRepository = booksRepository;
        //}

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var Books = _unitOfWork.Books.GetAll();
            var Authors = _unitOfWork.Authors.GetAll();
            if (Books != null)
            {
                RetriveDataViewModel vm = new RetriveDataViewModel
                {
                    Books = Books,
                    Authors = Authors

                };
                return View(vm);

            }
            return Ok();
        }
       
        public IActionResult AuthorDetails(int id)
        {
            var Author = _unitOfWork.Authors.GetById(id);
            var Books = _unitOfWork.Books.FindAll(b => b.AuthorId == id);
            if (Books != null)
            {
                AuthorDetailsViewModel vm = new AuthorDetailsViewModel
                {
                    Author = Author,
                    Books = Books,

                };
                return View(vm);

            }
            return Ok();
        }
        public IActionResult CreateAuthor()
        {
            return View();

        }

        [HttpPost]
        public IActionResult CreateAuthor(AddAuthorViewModel vm)
        {
            if (vm != null)
            {
                Author author = new Author();
                author.Name = vm.Name;
                _unitOfWork.Authors.Add(author);

            }

			return RedirectToAction("Index");

		}
        public IActionResult CreateBook()
		{
			var Authors = _unitOfWork.Authors.GetAll();

            if (Authors != null)
			{
				AddBookViewModel vm = new AddBookViewModel
				{
					Authors = Authors,
				};
				return View(vm);

			}
			return RedirectToAction("Index");

		}
		[HttpPost]
        public IActionResult CreateBook(AddBookViewModel vm)
        {
            if (vm != null && vm.AuthorId !=null)
            {
				Book book = new Book();
				book.Title = vm.Title;
				book.Price = vm.Price;
				var author = _unitOfWork.Authors.GetById(vm.AuthorId);
				book.Author = author;
                _unitOfWork.Books.Add(book);

			}
            
           return View(vm);

        }

        public IActionResult Edit(int id)
        {
            var book = _unitOfWork.Books.GetById(id);
            if ( book != null)
            {
                UpdateBookPriceVM vm = new UpdateBookPriceVM
                {
                    bookId = id,
                    Price = book.Price,
                };
                return View(vm);

            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Edit(UpdateBookPriceVM vm)
        {

            //    Book book
            ////}
            //if (ModelState.IsValid)
            //{
            if (vm != null)
            {

                var book = _unitOfWork.Books.GetById(vm.bookId);
                book.Price = vm.Price;

                _unitOfWork.Books.Update(book);

                //_context.Entry(vm).State = EntityState.Modified;
                return RedirectToAction("Index");
            }
            return View(vm);
            //_context.Books.Update();
            //_context.SaveChanges();
        }
		public IActionResult Delete(int id)
		{
			var book = _unitOfWork.Books.GetById(id);
			if (book != null)
			{
                _unitOfWork.Books.Delete(book);
			}

			return RedirectToAction("Index");
		}
		public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
