using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HarrisContactManager.Data
{
    public class DInitializer
    {
        public static void Initialize(HarrisDbContext context)
        {
            context.Database.EnsureCreated();
        }

        
    }
}
