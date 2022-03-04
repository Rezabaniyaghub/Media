using Media.Data;
using Media.Models;
using Media.Models.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Media.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            var folderList = _db.Folders.Where(x => x.ParentId == null).ToList();
            return View(folderList);
        }

        [HttpGet]
        public IActionResult AddNewFolder(int? parentId)
        {
            ViewBag.ParentId = parentId;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddNewFolder(Folder model)
        {
            if (ModelState.IsValid)
            {
                _db.Folders.Add(model);
                var SaveResult = _db.SaveChanges();
                if (SaveResult > 0)
                    if (model.ParentId == null)
                        return RedirectToAction(nameof(Index));
                    else
                        return Redirect($"/home/folder?folderId={model.ParentId}");
            }
            return View(model);
        }

        public IActionResult Folder(int folderId)
        {
            var folder = _db.Folders
                .Include(x => x.Files)
                .Include(x => x.Childs)
                .FirstOrDefault(x => x.Id == folderId);
            return View(folder);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}