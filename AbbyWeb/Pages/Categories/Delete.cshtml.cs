using AbbyWeb.Data;
using AbbyWeb.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;

namespace AbbyWeb.Pages.Categories
{
    [BindProperties]
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public Category Category { get; set; }
        public DeleteModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public void OnGet(int id)
        {
            Category = _db.Category.Find(id); // doesn't find, throws exception
        }

        public async Task<IActionResult> OnPost()
        {

            var categoryFromDb = _db.Category.Find(Category.Id);
            if (categoryFromDb != null)
            {
                _db.Category.Remove(categoryFromDb);
                await _db.SaveChangesAsync();
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}