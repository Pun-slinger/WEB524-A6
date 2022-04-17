using S2021A6QP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace S2021A6QP.Controllers
{
    [Authorize()]
    public class ArtistController : Controller
    {

        private Manager m = new Manager();

        // GET: Artist
        public ActionResult Index()
        {
            return View(m.ArtistGetAll());
        }

        // GET: Artist/Details/5
        public ActionResult Details(int? id)
        {
            var artist = m.ArtistGetByIdWithMediaInfoViewModel(id.GetValueOrDefault());

            if (artist == null)
            {
                return HttpNotFound();
            }

            return View(artist);
        }

        // GET: Artist/Create
        [Authorize(Roles = "Admin,Executive")]
        public ActionResult Create()
        {
            var addform = new ArtistAddFormViewModel();

            addform.GenreList = new SelectList(m.GenreGetAll(), "Id", "Name");

            return View(addform);
        }

        // POST: Artist/Create
        [Authorize(Roles = "Admin,Executive")]
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(ArtistAddViewModel newArtist)
        {
            if (!ModelState.IsValid)
            {
                return View(newArtist);
            }

            var addedArtist = m.ArtistAdd(newArtist);

            if (addedArtist == null)
            {
                return View(newArtist);
            }
            else
            {
                return RedirectToAction("Details", new { id = addedArtist.Id });
            }
        }

        [Authorize(Roles = "Admin,Coordinator")]
        [Route("Artist/{id}/AddAlbum")]
        public ActionResult AddAlbum(int? id)
        {
            var artist = m.ArtistGetByID(id.GetValueOrDefault());

            if (artist == null)
            {
                return HttpNotFound();
            }

            var addform = new AlbumAddFormViewModel();
            addform.ArtistName = artist.Name;
            addform.GenreList = new SelectList(m.GenreGetAll(), "Id", "Name");

            return View(addform);
        }

        [Route("Artist/{id}/AddAlbum")]
        [ValidateInput(false)]
        [Authorize(Roles = "Admin,Coordinator")]
        [HttpPost]
        public ActionResult AddAlbum(AlbumAddViewModel newAlbum)
        {
            if (!ModelState.IsValid)
            {
                return View(newAlbum);
            }

            var addedAlbum = m.AlbumAdd(newAlbum);

            if (addedAlbum == null)
            {
                return View(newAlbum);
            }
            else
            {
                return RedirectToAction("Details", "Album", new { id = addedAlbum.Id });
            }
        }

        [Authorize(Roles = "Admin,Executive")]
        [Route("Artist/{id}/AddMediaItem")]
        public ActionResult AddMediaItem(int? id)
        {
            var artist = m.ArtistGetByID(id.GetValueOrDefault());

            if (artist == null)
            {
                return HttpNotFound();
            }

            var addform = new ArtistMediaItemAddFormViewModel();
            addform.ArtistId = artist.Id;
            addform.ArtistInfo = artist.Name;

            return View(addform);
        }

        [Route("Artist/{id}/AddMediaItem")]
        [Authorize(Roles = "Admin,Executive")]
        [HttpPost]
        public ActionResult AddMediaItem(ArtistMediaItemAddViewModel newArtistMediaItem)
        {
            if (!ModelState.IsValid)
            {
                return View(newArtistMediaItem);
            }

            var addedArtistMediaItem = m.ArtistMediaItemAdd(newArtistMediaItem);

            if (addedArtistMediaItem == null)
            {
                return View(newArtistMediaItem);
            }
            else
            {
                return RedirectToAction("Details", "Artist", new { id = addedArtistMediaItem.Id });
            }
        }
    }
}
