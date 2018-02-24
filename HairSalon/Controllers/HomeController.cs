using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;
using System.Collections.Generic;
using System;

namespace HairSalon.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("/")]
        public ActionResult Index()
        {
            return View(Client.GetAll());
        }

        [HttpGet("/stylist/add")]
        public ActionResult StylistForm()
        {
            return View();
        }

        [HttpGet("/stylists")]
        public ActionResult Stylists()
        {
            return View(Stylist.GetAll());
        }

        [HttpGet("/stylist/{id}")]
        public ActionResult StylistDetail(int id)
        {
            Dictionary<string, object> model = new Dictionary<string, object>{};
            model.Add("stylist", Stylist.Find(id));
            model.Add("clients", Stylist.Find(id).GetAllClients());

            return View(model);
        }

        [HttpGet("/stylist/{id}/client/add")]
        public ActionResult ClientForm(int id)
        {
            Stylist newStylist = Stylist.Find(id);
            return View(newStylist);
        }

        [HttpGet("/stylist/{id}/update")]
        public ActionResult StylistUpdate(int id)
        {
            Stylist newStylist = Stylist.Find(id);

            return View(newStylist);
        }

        [HttpGet("/stylist/{id}/clients/{id2}/update")]
        public ActionResult ClientUpdate(int id, int id2)
        {
            Dictionary<string, object> model = new Dictionary<string, object>{};
            Stylist newStylist = Stylist.Find(id);
            Client newClient = Client.Find(id2);
            model.Add("stylist", newStylist);
            model.Add("client", newClient);
            return View(model);
        }

        [HttpGet("/stylist/{id}/remove")]
        public ActionResult StylistRemove(int id)
        {
            Stylist newStylist = Stylist.Find(id);
            newStylist.DeleteThis();

            return View("Stylists", Stylist.GetAll());
        }

        [HttpGet("/stylist/{id}/client/{id2}/remove")]
        public ActionResult ClientRemove(int id, int id2)
        {
            Dictionary<string, object> model = new Dictionary<string, object>{};
            Client newClient = Client.Find(id2);
            newClient.DeleteThis();
            model.Add("stylist", Stylist.Find(id));
            model.Add("clients", Stylist.Find(id).GetAllClients());

            return View("StylistDetail", model);
        }

        [HttpPost("/stylists")]
        public ActionResult StylistsNew()
        {
            Stylist newStylist = new Stylist(Request.Form["stylist-name"]);
            newStylist.Save();

            return View("Stylists", Stylist.GetAll());
        }

        [HttpPost("/stylist/{id}")]
        public ActionResult ClientNew(int id)
        {
            Dictionary<string, object> model = new Dictionary<string, object>{};
            Client newClient = new Client(Request.Form["client-name"], id);
            newClient.Save();

            model.Add("clients", Stylist.Find(id).GetAllClients());
            model.Add("stylist", Stylist.Find(id));

            return View("StylistDetail", model);
        }

        [HttpPost("/stylist/{id}/update")]
        public ActionResult StylistUpdatePost(int id)
        {
            Stylist newStylist = Stylist.Find(id);
            newStylist.UpdateName(Request.Form["stylist-name"]);

            return View("Stylists", Stylist.GetAll());
        }

        [HttpPost("/stylist/{id}/clients/{id2}/update")]
        public ActionResult ClientUpdatePost(int id, int id2)
        {
            Dictionary<string, object> model = new Dictionary<string, object>{};

            Client newClient = Client.Find(id2);
            newClient.UpdateName(Request.Form["client-name"]);

            model.Add("stylist", Stylist.Find(id));
            model.Add("clients", Stylist.Find(id).GetAllClients());

            return View("StylistDetail", model);
        }
    }
}
