using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Domain;

namespace Shortener.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILinkRepository _linkRepository;
        public HomeController(ILinkRepository linkRepository)
        {
            _linkRepository = linkRepository;
        }
        public async Task<ActionResult> Index()
        {
            var items = await _linkRepository.GetLinks();
            return View(items);
        }
        [HttpPost]
        public async Task<ActionResult> Index(string link)
        {
            if (await _linkRepository.AddLink(link))
            {
                return View(await _linkRepository.GetLinks());
            }
            else
            {
                //Logger.Log();
                return View(await _linkRepository.GetLinks());
            }
        }
        public async Task<ActionResult> LinkRedirect(string shortPart)
        {
            var linkItem = await _linkRepository.GetLinkByShortUrl(shortPart);
            if (linkItem != null)
            {
                return Redirect(linkItem.Url);
            }
            else
            {
                return View("Error", model: shortPart);
            }
        }
    }
}