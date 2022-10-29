﻿namespace Pizzeria.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Pizzeria.Data.Models;

    public class SizesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Sizes.Any())
            {
                return;
            }

            await dbContext.Sizes.AddAsync(new Size() { Name = "Medium ∅ 25cm" });
            await dbContext.Sizes.AddAsync(new Size() { Name = "Large ∅ 30cm" });
            await dbContext.Sizes.AddAsync(new Size() { Name = "Jumbo ∅ 38cm" });


            await dbContext.SaveChangesAsync();
        }
    }
}
