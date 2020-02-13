using CarInsuranceApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarInsuranceApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult TotalQuote(string firstName, string lastName, string emailAddress, DateTime dateOfBirth, int carYear, string carMake, string carModel, bool hadDUI, int speedingTickets, bool fullCoverage, bool liability)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(emailAddress))
            {
                return View("~/Views/Shared/Error.cshtml");
            }
            else
            {
                using (CarInsuranceEntities db = new CarInsuranceEntities())
                {
                    var driver = new CarDriver();
                    driver.FirstName = firstName;
                    driver.LastName = lastName;
                    driver.EmailAddress = emailAddress;
                    driver.DateOfBirth = dateOfBirth;
                    driver.CarYear = carYear;
                    driver.CarMake = carMake;
                    driver.CarModel = carModel;
                    driver.HadDUI = hadDUI;
                    driver.SpeedingTickets = speedingTickets;
                    driver.FullCoverage = fullCoverage;
                    driver.Liability = liability;

                    double totalQuote = 50;

                    if ((DateTime.Now).Year - (Convert.ToDateTime(driver.DateOfBirth)).Year < 25)
                    {
                        totalQuote += 25;
                    }
                    if ((DateTime.Now).Year - (Convert.ToDateTime(driver.DateOfBirth)).Year < 18)
                    {
                        totalQuote += 100;
                    }
                    if ((DateTime.Now).Year - (Convert.ToDateTime(driver.DateOfBirth)).Year > 100)
                    {
                        totalQuote += 25;
                    }
                    if (driver.CarYear < 2000)
                    {
                        totalQuote += 25;
                    }
                    if (driver.CarYear>2015)
                    {
                        totalQuote += 25;
                    }
                    if (driver.CarMake.ToLower()=="porsche")
                    {
                        totalQuote += 25;
                    }
                    if (driver.CarMake.ToLower() == "porsche" && driver.CarModel.ToLower()=="911 carrera")
                    {
                        totalQuote += 25;
                    }
                    if (driver.SpeedingTickets!=0)
                    {
                        totalQuote += Convert.ToDouble(driver.SpeedingTickets) * 10;
                    }
                    if (Convert.ToBoolean(driver.HadDUI))
                    {
                        totalQuote *= 1.25;
                    }
                    if (Convert.ToBoolean(driver.FullCoverage))
                    {
                        totalQuote *= 1.5;
                    }

                    driver.FinalQuote = totalQuote;
                    db.CarDrivers.Add(driver);
                    db.SaveChanges();

                    ViewBag.totalQuote = totalQuote;
                }

                return View("Success");
            }
        }
    }
}