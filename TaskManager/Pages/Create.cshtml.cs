using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TaskManager.Models;
using TaskManager.Tasks;

namespace TaskManager.Pages.Tasks
{
    public class CreateModel : PageModel
    {
        private readonly TaskManager.Models.TaskManagerContext _context;

        public CreateModel(TaskManager.Models.TaskManagerContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public TaskEntity TaskEntity { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.TaskEntity.Add(TaskEntity);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}