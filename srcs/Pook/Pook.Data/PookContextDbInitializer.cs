using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pook.Data
{
    public class PookContextDbInitializer : DropCreateDatabaseIfModelChanges<PookDbContext>
    {
        protected override void Seed(PookDbContext context)
        {
            // TODO add seed here

            base.Seed(context);
        }
    }
}
