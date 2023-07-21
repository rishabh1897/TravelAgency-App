using Microsoft.AspNetCore.Mvc;
using UPYatra.Models;

namespace UPYatra.ViewComponents
{
    [ViewComponent]
    public class MonthlySpecialViewComponent : ViewComponent
    {
        private readonly BlogDataContext db;

        public MonthlySpecialViewComponent(BlogDataContext db) 
        {
            this.db = db;
        }
        public IViewComponentResult Invoke()
        {
            var specials = db.MonthlySpecials.ToArray();
            return View(specials);
        }
    }
}
