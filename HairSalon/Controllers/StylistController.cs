using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;
using System;

namespace HairSalon.Controllers
{
  public class StylistController : Controller
  {
    [HttpGet("/stylists")]
    public ActionResult StylistIndex()
    {
      List<Stylist> allStylists = Stylist.GetAll();
      return View(allStylists);
    }

    [HttpGet("/stylists/new")]
    public ActionResult StylistCreateForm()
    {
      return View();
    }

    [HttpPost("/stylists")]
    public ActionResult StylistCreate()
    {
      string name = Request.Form["stylist-name"];

      Stylist newStylist = new Stylist(name);
      newStylist.Save();

      return RedirectToAction("StylistIndex");
    }

    [HttpGet("/stylists/{id}")]
    public ActionResult StylistDetail(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();

      Stylist selectedStylist = Stylist.Find(id);

      List<Client> stylistClients = selectedStylist.GetClients();
      List<Client> allClients = Client.GetAll();

      model.Add("stylist", selectedStylist);
      model.Add("stylistClients", stylistClients);
      model.Add("allClients", allClients);

      return View("StylistDetails", model);
    }

    // [HttpGet("/stylists/{id}")]
    // public ActionResult StylistSpecialty(int id)
    // {
    //   Dictionary<string, object> model = new Dictionary<string, object>();
    //
    //   Stylist selectedStylist = Stylist.Find(id);
    //
    //   List<Specialty> stylistSpecialties = selectedStylist.GetSpecialty();
    //   List<Specialty> allSpecialties = Specialty.GetAll();
    //
    //   model.Add("stylist", selectedStylist);
    //   model.Add("stylistSpecialties", stylistSpecialties);
    //   model.Add("allSpecialties", allSpecialties);
    //
    //   return View("StylistDetails", model);
    // }

    [HttpPost("/stylists/{id}/addclient")]
    public ActionResult AddStylistClient(int id)
    {
      Stylist thisStylist = Stylist.Find(id);

      Client newClient = Client.Find(Int32.Parse(Request.Form["client-id"]));
      thisStylist.AddClient(newClient);

      return RedirectToAction("StylistDetails", id);
    }

    [HttpGet("/stylists/{id}/update")]
    public ActionResult StylistUpdateForm(int id)
    {
      Stylist thisStylist = Stylist.Find(id);

      return View("StylistUpdate", thisStylist);
    }

    [HttpPost("/stylists/{id}/update")]
    public ActionResult Update(int id)
    {
      string newName = Request.Form["newname"];
      Stylist thisStylist = Stylist.Find(id);

      thisStylist.UpdateName(newName);
      return RedirectToAction("StylistIndex");
    }

    [HttpGet("/stylists/{stylistId}/delete")]
    public ActionResult DeleteOne(int stylistId)
    {
      Stylist thisStylist = Stylist.Find(stylistId);
      thisStylist.DeleteOne();

      return RedirectToAction("StylistIndex");
    }

    // [HttpPost("/stylists/delete")]
    // public ActionResult DeleteAll(int stylistId)
    // {
    //   Stylist.DeleteAll();
    //
    //   return RedirectToAction("StylistIndex");
    // }
    [HttpGet("/stylists/delete")]
    public ActionResult DeleteAll()
    {
      Stylist.DeleteAll();

      return RedirectToAction("StylistIndex");
    }
  }
}
