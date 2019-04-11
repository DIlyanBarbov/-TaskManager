using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TaskManager.Models;

namespace TaskManager.Tasks
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new TaskManagerContext(serviceProvider.GetRequiredService<DbContextOptions<TaskManagerContext>>()))
            {
                if (context.TaskEntity.Any())
                { return; }
                context.TaskEntity.AddRange
                    (
                    new TaskEntity
                    {
                        Name = "Second Exam",
                        Description = "Mathematics",
                        StartTime = DateTime.Parse("23.05.2019 8:00"),
                        EndTime = DateTime.Parse("23.05.2019 12:00")
                    },
                    new TaskEntity
                    {
                        Name = "First Exam",
                        Description = "Bulgarian and Litherature",
                        StartTime = DateTime.Parse("21.05.2019 8:00"),
                        EndTime = DateTime.Parse("21.05.2019 12:00")
                    },
                    new TaskEntity
                    {
                        Name = "Playoff Game",
                        Description = "First of Three versus Balkan",
                        StartTime = DateTime.Parse("07.04.2019 18:45"),
                        EndTime = DateTime.Parse("07.04.2019 20:00")
                    }
                    );
                context.SaveChanges();
            }
        }
    }
}
