using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;
using System;

namespace HairSalon.Controllers
{
  public class SpecialtyController : Controller
  {
    [HttpGet("/specialties")]
    public ActionResult SpecialtyIndex()
    {
      List<Specialty> allSpecialties = Specialty.GetAll();

      return View(allSpecialties);
    }

    [HttpGet("/specialties/new")]
    public ActionResult SpecialtyCreateForm()
    {
      return View();
    }

    [HttpPost("/specialties")]
    public ActionResult SpecialtyCreate()
    {
      string name = Request.Form["specialty-name"];

      Specialty newSpecialty = new Specialty(name);
      newSpecialty.Save();

      return RedirectToAction("SpecialtyIndex");
    }

    [HttpGet("/specialties/{id}")]
    public ActionResult SpecialtyDetail(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();

      Specialty selectedSpecialty = Specialty.Find(id);

      List<Stylist> stylistSpecialties = selectedSpecialty.GetStylists();
      List<Stylist> allStylists = Stylist.GetAll();

      model.Add("specialty", selectedSpecialty);
      model.Add("stylistSpecialties", stylistSpecialties);
      model.Add("allStylists", allStylists);

      return View("SpecialtyDetails", model);
    }

    [HttpPost("/specialties/{specialtyId}/stylists/new")]
    public ActionResult AddStylist(int specialtyId)
    {
      Specialty specialty = Specialty.Find(specialtyId);

      Stylist stylist = Stylist.Find(Int32.Parse(Request.Form["stylist-id"]));

      return View("Success");
    }

    [HttpGet("/specialties/{id}/update")]
    public ActionResult SpecialtyUpdateForm(int id)
    {
      Specialty thisSpecialty = Specialty.Find(id);

      return View("SpecialtyUpdate", thisSpecialty);
    }

    [HttpPost("/specialties/{id}/update")]
    public ActionResult Update(int id)
    {
      string newName = Request.Form["newname"];

      Specialty thisSpecialty = Specialty.Find(id);

      thisSpecialty.UpdateName(newName);

      return RedirectToAction("SpecialtyIndex");
    }

    [HttpGet("/specialties/{specialtyId}/delete")]
    public ActionResult DeleteOne(int specialtyId)
    {
      Specialty thisSpecialty = Specialty.Find(specialtyId);

      thisSpecialty.DeleteOne();

      return RedirectToAction("SpecialtyIndex");
    }
    [HttpPost("/specialties/delete")]
    public ActionResult DeleteAll(int clientId)
    {
      Client.DeleteAll();

      return RedirectToAction("SpecialtyIndex");
    }
  }
}
