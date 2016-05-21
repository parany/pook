using System.Data.Entity;

namespace Pook.Data
{
    public class PookDbInitializer : DropCreateDatabaseIfModelChanges<PookDbContext>
    {
        protected override void Seed(PookDbContext context)
        {
            // TODO perform db initialization here
            base.Seed(context);
        }
    }
}
