using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TaskManager.Models;
using TaskManager.Tasks;

namespace TaskManager.Pages.Tasks
{
    public class EditModel : PageModel
    {
        private readonly TaskManager.Models.TaskManagerContext _context;

        public EditModel(TaskManager.Models.TaskManagerContext context)
        {
            _context = context;
        }

        [BindProperty]
        public TaskEntity TaskEntity { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TaskEntity = await _context.TaskEntity.FirstOrDefaultAsync(m => m.Id == id);

            if (TaskEntity == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(TaskEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskEntityExists(TaskEntity.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool TaskEntityExists(int id)
        {
            return _context.TaskEntity.Any(e => e.Id == id);
        }
    }
}
