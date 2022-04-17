using S2021A6QP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace S2021A6QP.Controllers
{
    [Authorize()]
    public class TrackController : Controller
    {
        private Manager m = new Manager();

        // GET: Track
        public ActionResult Index()
        {
            return View(m.TrackGetAll());
        }

        // GET: Track/Details/5
        public ActionResult Details(int? id)
        {
            var track = m.TrackGetByID(id.GetValueOrDefault());

            if(track == null)
            {
                return HttpNotFound();
            }

            return View(track);
        }

        // GET: Track/Edit/5
        [Authorize(Roles = "Admin,Clerk")]
        public ActionResult Edit(int? id)
        {
            var track = m.TrackGetByID(id.GetValueOrDefault());

            if(track == null)
            {
                return HttpNotFound();
            }

            var editForm = m.mapper.Map<TrackEditFormViewModel>(track);

            return View(editForm);
        }

        // POST: Track/Edit/5
        [HttpPost]
        [Authorize(Roles = "Admin,Clerk")]
        public ActionResult Edit(int? id, TrackEditViewModel editTrack)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Edit", new { id = editTrack.Id });
            }

            if (id.GetValueOrDefault() != editTrack.Id)
            {
                return RedirectToAction("Index");
            }

            //Attempt to update
            var editedPlaylist = m.TrackEdit(editTrack);

            if (editedPlaylist == null)
            {
                return RedirectToAction("Edit", new { id = editTrack.Id });
            }
            else
            {
                return RedirectToAction("Details", new { id = editTrack.Id });
            }
        }

        // GET: Track/Delete/5
        [Authorize(Roles = "Admin,Clerk")]
        public ActionResult Delete(int? id)
        {
            var deleteTrack = m.TrackGetByID(id.GetValueOrDefault());

            if(deleteTrack == null)
            {
                return RedirectToAction("Index");
            }

            return View(deleteTrack);
        }

        // POST: Track/Delete/5
        [Authorize(Roles = "Admin,Clerk")]
        [HttpPost]
        public ActionResult Delete(int? id, FormCollection collection)
        {
            var result = m.TrackDelete(id.GetValueOrDefault());

            return RedirectToAction("Index");
        }
    }
}
