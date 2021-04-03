using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public interface ILinkRepository
    {
        Task<IEnumerable<Link>> GetLinks();
        Task<Link> GetLink(long ID);
        Task<Link> GetLinkByShortUrl(string shortUrl);
        Task<bool> AddLink(string url);

    }
}
