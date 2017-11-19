using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ContactForm.Models;
using ContactForm.Service;
using ContactForm.Repository;

namespace ContactForm.Controllers
{
    public class ContactFormsController : Controller
    {
        private EmailService _emailService;
        private ContactFormRepository _contactFormRepository; // nowa instanccja 


        public ContactFormsController()
        {
            _emailService = new EmailService();
            _contactFormRepository = new ContactFormRepository();
        }
        //private ApplicationDbContext db = new ApplicationDbContext(); // tutaj jest strzał do bazy bezpośredniou z dbContext  DOBRE PRAKTYKI - unikamy tego!!

        // GET: ContactForms
        public ActionResult Index()
        {
            return View(_contactFormRepository.GetWhere(x => x.Id>0)); // zwróci wszystkie rekordy, których id jest większe od 0.
        }

        // GET: ContactForms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.ContactForm contactForm = _contactFormRepository.GetWhere(x=>x.Id==id.Value).FirstOrDefault(); //po wykonaniu tego zapytania zwraca jeden element 
            // znajdź mi w repozytorium obiekt, który ma określone ID. ID to nullable - dlatego podajemy value

            if (contactForm == null)
            {
                return HttpNotFound();
            }
            return View(contactForm);
        }

        // GET: ContactForms/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Models.ContactForm contactForm)
        {
            if (ModelState.IsValid)
            {
                _contactFormRepository.Create(contactForm);
                var message = _emailService.CreateMailMessage(contactForm);
                _emailService.SendEmail(message);
                return RedirectToAction("Index");
            }

            return View(contactForm);
        }




        // GET: ContactForms/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //wyszukanie konkretnego obiektu do usunięcia
            Models.ContactForm contactForm = _contactFormRepository.GetWhere(x => x.Id == id.Value).FirstOrDefault();
            if (contactForm == null)
            {
                return HttpNotFound();
            }
            return View(contactForm);
        }

        // POST: ContactForms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Models.ContactForm contactForm = _contactFormRepository.GetWhere(x => x.Id == id).FirstOrDefault();
            _contactFormRepository.Delete(contactForm);
           
            return RedirectToAction("Index");
        }
    }
}
