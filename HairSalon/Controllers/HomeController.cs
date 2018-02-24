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
            return View();
        }

        [HttpGet("/stylists/new")]
        public ActionResult AddNewStylist()
        {
            return View();
        }

        [HttpPost("/stylists")]
        public ActionResult AddStylist()
        {
            Stylist newStylist = new Stylist(Request.Form["stylist-name"]);
            newStylist.Save();
            List<Stylist> allStylists = Stylist.GetAll();
            return View("Stylists", allStylists);
        }

        [HttpGet("/stylists")]
        public ActionResult Stylists()
        {
            List<Stylist> allStylists = Stylist.GetAll();
            return View(allStylists);
        }

        [HttpGet("/stylists/{id}")]
        public ActionResult StylistDetail(int id)
        //id = 1
        {
            Dictionary<string, object> model = new Dictionary<string, object>();
            Stylist selectedStylist = Stylist.Find(id);
            List<Client> stylistClients = selectedStylist.GetStylistClients();
            model.Add("stylist", selectedStylist);
            model.Add("clients", stylistClients);
            return View(model);
        }

        [HttpGet("/stylists/{id}/clients/new")]
        public ActionResult AddClient(int id)
        {
            Stylist selectedStylist = Stylist.Find(id);
            return View(selectedStylist);
        }

        [HttpPost("/stylists/{id}/clientlist")]
        public ActionResult AddEachClient(int id)
        {
            Client newClient = new Client(Request.Form["client-name"], id);
            newClient.Save();
            Dictionary<string, object> model = new Dictionary<string, object>();
            Stylist selectedStylist = Stylist.Find(id);
            List<Client> allClients = selectedStylist.GetStylistClients();
            model.Add("stylist", selectedStylist);
            model.Add("clients", allClients);
            return View("StylistDetail", model);
        }

        [HttpGet("/stylists/{id}/clientlist")]        public ActionResult ViewClientList(int id)
        {
            Dictionary<string, object> model = new Dictionary<string, object>();
            Stylist selectedStylist = Stylist.Find(id);
            List<Client> allClients = selectedStylist.GetStylistClients();
            model.Add("stylist", selectedStylist);
            model.Add("clients", allClients);
            return View("StylistDetail", model);
        }

        [HttpGet("/clients/{id}/edit")]
        public ActionResult EditClientName(int id)
        {
            Client thisClient = Client.Find(id);
            return View(thisClient);
        }

        [HttpPost("/clients/{id}/edit")]
        public ActionResult EditedClientName(int id)
        {
            Client thisClient = Client.Find(id);
            thisClient.UpdateClientName(Request.Form["new-name"]);
            return RedirectToAction("Index");
        }

        [HttpGet("/clients/{id}/delete")]
        public ActionResult DeleteClient(int id)
        {
            Client thisClient = Client.Find(id);
            thisClient.DeleteClient();
            return RedirectToAction("Index");
        }

    }
}
