using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CmsEngine.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CmsEngine.Data.Repositories
{
    public class WebsiteRepository : Repository<Website>, IWebsiteRepository
    {
        public WebsiteRepository(CmsEngineContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Website>> GetForDataTable()
        {
            return await Get().Select(w => new Website
            {
                VanityId = w.VanityId,
                Name = w.Name,
                Tagline = w.Tagline,
                Culture = w.Culture,
                UrlFormat = w.UrlFormat,
                DateFormat = w.DateFormat,
                SiteUrl = w.SiteUrl,
                GoogleAnalytics = w.GoogleAnalytics
            }).ToListAsync();
        }

        public Website GetWebsiteInstanceByHost(string host)
        {
            return Get(q => q.SiteUrl == host).SingleOrDefault();
        }
    }
}
