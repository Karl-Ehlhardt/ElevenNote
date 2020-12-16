using ElevenNote.Models;
using ElevenNote.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElevenNote.WebMVC.Controllers
{
    [Authorize]
    public class NoteController : Controller
    {
        private NoteService CreateNoteService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var noteService = new NoteService(userId);
            return noteService;
        }

        // GET: Note
        public ActionResult Index()
        {
            NoteService service = CreateNoteService();

            var model = service.GetNotes();
            return View(model);
        }

        //Add method here VVVV
        //GET
        public ActionResult Create()
        {
            return View();
        }

        //Add method here VVVV
        //GET
        public ActionResult Details(int id)
        {
            NoteService service = CreateNoteService();

            var model = service.GetNoteById(id);
            return View(model);
        }

        //Add method here VVVV
        //GET
        public ActionResult Edit(int id)
        {
            NoteService service = CreateNoteService();

            var model = service.GetNoteById(id);
            return View(model);
        }

        //Add method here VVVV
        //GET
        public ActionResult Delete(int id)
        {
            NoteService service = CreateNoteService();

            var model = service.GetNoteById(id);
            return View(model);
        }

        //Add code here vvvv
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NoteCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateNoteService();

            if (service.CreateNote(model))
            {
                TempData["SaveResult"] = "Your note was created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Note could not be created.");

            return View(model);
        }

        //Add method here VVVV
        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(NoteEdit note)
        {
            if (!ModelState.IsValid) return View(note);

            var service = CreateNoteService();

            if (service.UpdateNote(note))
            {
                TempData["SaveResult"] = "Your note was edited.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Note could not be edited.");

            return View(note);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {

            var service = CreateNoteService();

            if (service.DeleteNote(id))
            {
                TempData["SaveResult"] = "Your note was delete.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Note could not be deleted.");

            return Delete(id);
        }
    }
}