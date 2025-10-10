using ContosoUniversity.Data;
using ContosoUniversity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ContosoUniversity.Pages.Students
{
    public class EditModel : PageModel
    {
        private readonly SchoolContext _context;
        public EditModel(SchoolContext context) { _context = context; }

        [BindProperty]
        public Student Student { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            Student = await _context.Students.AsNoTracking().FirstOrDefaultAsync(m => m.ID == id);

            if (Student == null) return NotFound();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null) return NotFound();

            var studentToUpdate = await _context.Students.FirstOrDefaultAsync(s => s.ID == id);
            if (studentToUpdate == null) return NotFound();

            if (await TryUpdateModelAsync<Student>(
                studentToUpdate,
                "student",   // form prefix
                s => s.FirstMidName, s => s.LastName, s => s.EnrollmentDate))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}
