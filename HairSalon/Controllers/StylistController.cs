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

      // Console.WriteLine("Beginning of StylistDetail method, the stylist id is:" + id);

      Stylist selectedStylist = Stylist.Find(id);

      List<Specialty> stylistSpecialties = selectedStylist.GetSpecialty();
      List<Specialty> allSpecialties = Specialty.GetAll();

      model.Add("stylist", selectedStylist);
      model.Add("stylistSpecialties", stylistSpecialties);
      model.Add("allSpecialties", allSpecialties);

      // Dictionary<string, object> model2 = new Dictionary<string, object>();
      //
      // Stylist selectedStylist2 = Stylist.Find(id);
      //
      // List<Client> stylistClients = selectedStylist.GetClients();
      // List<Client> allClients = Client.GetAll();
      //
      // model2.Add("client", selectedStylist2);
      // model2.Add("stylistClients", stylistClients);
      // model2.Add("allClients", allClients);

      return View("StylistDetails", model);
      // return View("StylistDetails", model2);
    }

    [HttpPost("/stylists/{stylistId}/specialties/new")]
    public ActionResult StylistSpecialty(int stylistId)
    {
      // Console.WriteLine("Beginning of StylistSpecialty method, the stylist id is:" + stylistId);
      Stylist thisStylist = Stylist.Find(stylistId);

      // Console.WriteLine("special id is: " + Request.Form["specialty-id"]);
      Specialty newSpecialty = Specialty.Find(Int32.Parse(Request.Form["specialty-id"]));
      thisStylist.AddSpecialty(newSpecialty);

      return RedirectToAction("StylistDetail", new {id = stylistId});
    }

    // [HttpPost("/stylists/{id}/addclient")]
    // public ActionResult AddStylistClient(int id)
    // {
    //   Stylist thisStylist = Stylist.Find(id);
    //
    //   Client newClient = Client.Find(Int32.Parse(Request.Form["client-id"]));
    //   thisStylist.AddClient(newClient);
    //
    //   return RedirectToAction("StylistDetails", id);
    // }

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
