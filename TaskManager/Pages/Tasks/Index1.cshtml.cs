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
using TaskManager.Pages;

namespace TaskManager.Pages.Tasks
{
    public class IndexModel : PageModel
    {
        private readonly TaskManager.Models.TaskManagerContext _context;

        public IndexModel(TaskManager.Models.TaskManagerContext context)
        {
            _context = context;
        }

        public IList<TaskEntity> TaskEntity { get;set; }
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; } // contains the name that the user searchs for
        public SelectList Names { get; set; } // contains the list of names
        [BindProperty(SupportsGet = true)]
        public string DescrSearch { get; set; } // contains the datetime that user searchs
        public async Task OnGetAsync()
        {
            IQueryable<string> descrQuery = from m in _context.TaskEntity
                                              orderby m.Description
                                              select m.Description;

            var tasks = from m in _context.TaskEntity
                         select m;
            if (!string.IsNullOrEmpty(SearchString))
            {
                tasks = tasks.Where(s => s.Name.Contains(SearchString));
            }
            if (!string.IsNullOrEmpty(DescrSearch))
            {
                tasks = tasks.Where(x => x.Description == DescrSearch);
            }
            Names = new SelectList(await descrQuery.Distinct().ToListAsync());
            TaskEntity = await tasks.ToListAsync();
            //TaskEntity = await _context.TaskEntity.ToListAsync();
        }
    }
}
