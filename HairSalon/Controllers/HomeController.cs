using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;
using System.Collections.Generic;
using System;

namespace HairSalon.Controllers
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
    
}
