using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using tourBD.Core.Seeds;
using tourBD.Forum.Contexts;

namespace tourBD.Forum.Seeds
{
    public class ForumSeed : DataSeed
    {
        public ForumSeed(ForumContext forumContext)
            : base(forumContext)
        {
        }

        public override Task SeedAsync()
        {
            throw new NotImplementedException();
        }
    }
}
