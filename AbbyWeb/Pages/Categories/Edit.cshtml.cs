using AbbyWeb.Data;
using AbbyWeb.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;

namespace AbbyWeb.Pages.Categories
{
    [BindProperties]
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public Category Category { get; set; }
        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public void OnGet(int id)
        {
            Category = _db.Category.Find(id); // doesn't find, throws exception
            //Category = _db.Category.FirstOrDefault(u => u.Id == id); // doesn't find returns null
            //Category = _db.Category.Single(u => u.Id == id); // when you expect only 1 entity to be returned, throws exc when not found
            //Category = _db.Category.SingleOrDefault(u => u.Id == id); // returns null when not found
            //Category = _db.Category.Where(u => u.Id == id).FirstOrDefault(); // not favorite approach with entity, returns all records where id matches. 
        }
        
        public async Task<IActionResult> OnPost()
        {
            if (Category.Name == Category.DisplayOrder.ToString() )
            {
                ModelState.AddModelError("Category.Name", "The DisplayOrder cannot exactly match the Name.");
            }
            if (ModelState.IsValid)
            {
                _db.Category.Update(Category);
                await _db.SaveChangesAsync();
                TempData["success"] = "Category updated successfully.";
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
