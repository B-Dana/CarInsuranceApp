using CarInsuranceApp.Models;
using CarInsuranceApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewsletterAppMVC.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            using (CarInsuranceEntities db = new CarInsuranceEntities())
            {
                //var signups = db.SignUps.Where(x => x.Removed == null).ToList();
                //var signups = (from c in db.SignUps
                //               where c.Removed == null
                //               select c).ToList();
                var driverVms = new List<CardriverVm>();
                var drivers = db.CarDrivers;
                foreach (var driver in drivers)
                {
                    var driverVm = new CardriverVm();
                    driverVm.Id = driver.Id;
                    driverVm.FirstName = driver.FirstName;
                    driverVm.LastName = driver.LastName;
                    driverVm.EmailAddress = driver.EmailAddress;
                    driverVm.FinalQuote = Convert.ToDouble(driver.FinalQuote);
                    
                    driverVms.Add(driverVm);
                }
                return View(driverVms);
            }
        }  
    }
}