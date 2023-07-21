using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using UPYatra.Models;

namespace UPYatra.Controllers
{
    [Route("blog")]
    public class BlogController : Controller
    {
        private readonly BlogDataContext _blogDataContext;
        public BlogController(BlogDataContext db)
        {
            _blogDataContext = db;
        }
        [Route("")]
        public IActionResult Index(int page = 0)
        {
            //var posts = _blogDataContext.Posts.OrderByDescending(x => x.Posted).Take(5).ToArray();
            //return View(posts);
            var pageSize = 2;
            var totalPosts = _blogDataContext.Posts.Count();
            var totalPages = totalPosts / pageSize;
            var previousPage = page - 1;
            var nextPage = page + 1;

            ViewBag.PreviousPage = previousPage;
            ViewBag.HasPreviousPage = previousPage >= 0;
            ViewBag.NextPage = nextPage;
            ViewBag.HasNextPage = nextPage < totalPages;

            var posts =
                _blogDataContext.Posts
                    .OrderByDescending(x => x.Posted)
                    .Skip(pageSize * page)
                    .Take(pageSize)
                    .ToArray();

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                return PartialView(posts);

            return View(posts);
        }

        //[Route("/post")]
        [Route("{year:min(2010)}/{month:range(1,12)}/{key?}")]
        public IActionResult Post(int year, int month, string key)
        {
            var post = _blogDataContext.Posts.FirstOrDefault(x => x.Key == key);
            //var post = new Post{
            //    Title = "My blog post",
            //    Posted = DateTime.Now,
            //    Author = "Me myself",
            //    Body = "rdsrhstrjnyt"
            //};
            //ViewBag.Title = "My blog post";
            //ViewBag.Posted = DateTime.Now;
            //ViewBag.Author = "Me myself";
            //ViewBag.Body = "rdsrhstrjnyt";
            return View(post);
        }
        [Authorize]
        [HttpGet,Route("create")]
        public IActionResult Create()
        {
            return View();
        }
        [Authorize]
        [HttpPost,Route("create")]
        public IActionResult Create(Post post)
        {
            post.Author = User.Identity.Name;
            post.Posted = DateTime.Now;
            if (!ModelState.IsValid)
            {
                return View();
            }

            _blogDataContext.Posts.Add(post);
            _blogDataContext.SaveChanges();
            return RedirectToAction("Post","Blog",new
            {
                year = post.Posted.Year,
                month = post.Posted.Month,
                key = post.Key
            });
        }
    }
}
