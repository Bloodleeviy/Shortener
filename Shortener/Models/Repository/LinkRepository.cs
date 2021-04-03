using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Domain;
using Infrastructure;
using System.Security.Cryptography;


namespace Shortener.Models.Repository
{
    public class LinkRepository : ILinkRepository, IDisposable
    {
        private LinksContext db = new LinksContext();
        public async Task<IEnumerable<Link>> GetLinks()
        {
            var data = await db.Links.ToListAsync();
            if (data.Count == 0)
            {
                var IsAddYa = await AddLink("https://ya.ru");
                var IsAddGmail = await AddLink("https://gmail.com");
                var IsAddMail = await AddLink("https://mail.ru");
                var IsAddhabr = await AddLink("https://habr.com");
                var IsAddSO = await AddLink("https://stackoverflow.com");
            }
            return await db.Links.ToListAsync();
        }
        public async Task<Link> GetLink(long ID)
        {
            return await db.Links.FindAsync(ID);
        }
        public async Task<Link> GetLinkByShortUrl(string LinkHash)
        {
            return await db.Links.Where(x => x.LinkHash == LinkHash).FirstOrDefaultAsync();
        }
        public async Task<bool> AddLink(string url)
        {
            Link link = new Link();
            link.Url = url;
            var md5 = Convert.ToBase64String(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(url)));
            link.LinkHash = string.Join("", md5.Take(6));
            link.ShortUrL = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + $"/{link.LinkHash}";
            try
            {
                db.Links.Add(link);
                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                //Logger.Log()
                return false;
            }
        }
        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}