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
      List<Client> allClients = Client.GetAll();
      return View();
    }

    [HttpGet("/Home/Success")]
    public ActionResult Success()
    {
      return View();
    }
  }
}
