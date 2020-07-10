using CarInsurance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarInsurance.Controllers
{
    public class HomeController : Controller
    {
        readonly InsuranceEntities insuranceQuote = new InsuranceEntities();
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult TheirInsuranceQuote(string firstName, string lastName, string emailAddress, DateTime dateOfBirth, int carYear, string carMake, string carModel, bool dui, int speedingTickets, string coverageType)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(emailAddress) || string.IsNullOrEmpty(carModel)) // If they don't enter a string, it redirects to the error page.
            {
                return View("~/Views/Shared/Error.cshtml");
            }
            else
            {
                int thyDate = DateTime.Now.Year;
                int total = 0;

                if (thyDate - dateOfBirth.Year > 25) // Checks age is greater than 25.If it is,  It then adds $25 to the total.
                {
                    total = 50 + 25;
                }
                else if (thyDate - dateOfBirth.Year > 18 && thyDate - dateOfBirth.Year < 25) // Checks if =age is greater than 18 AND less than 25. If it is, It then adds $50 to the total.
                {
                    total = 50 + 50;
                }
                else if (thyDate - dateOfBirth.Year <= 18) // Checks if age is less than 18. If it is, It adds $100 to the total.
                {
                    total = 50 + 100;
                };


                if (carYear <= 2000 || carYear >= 2015) // Checks if Year is less than 2000 or more than 2015. If either is true, it adds $25 to the total.
                {
                    total += 25;
                    if (carMake.ToLower() == "Porsche") // Checks if the car make is a Porsche, if it is, it adds an extra $25.
                    {
                        total += 25;
                        if (carModel.ToLower() == "911 Carrerra" || carModel.ToLower() == "911Carrerra" || carModel.ToLower() == "911" || carModel.ToLower() == "Carrerra") // Checks if the car make is a 911 in multiple ways, if it is, it adds an extra $25.
                        {
                            total += 25;
                        }
                    }
                };
                if (speedingTickets > 0) // Checks if they have speeding tickets. Takes the amount and multiplies it by 10, then adds the total to the total.
                {
                    total += (speedingTickets * 10);
                };
                if (dui == true) // Checks if DUI is set to true, if it is, id adds 25% of their current total, to the total.
                {
                    total = (total / 4) + total;
                };


                if (coverageType.ToLower() == "full coverage" || coverageType.ToLower() == "full") // Checks if they chose full coverage, if they have, it divides the current standing total in half, and adds that to the total.
                {
                    total = (total / 2) + total;
                };

                int quote = total; // Displays final total.
                using (InsuranceEntities db = new InsuranceEntities())
                {
                    {
                        var theirQuote = new Table
                        {
                            FirstName = firstName,
                            LaststName = lastName,
                            EmailAddress = emailAddress,
                            DateOfBirth = dateOfBirth,
                            CarYear = carYear,
                            CarMake = carMake,
                            CarModel = carModel,
                            DUI = dui,
                            SpeedingTickets = speedingTickets,
                            CoverageType = coverageType,
                            Quote = quote
                        };

                        db.Tables.Add(theirQuote);
                        db.SaveChanges();
                    }

                }
            }


            var listerino = insuranceQuote.Tables.OrderByDescending(i => i.Id).First();

            return View("insranceQuotePost", listerino); 
        }
    }
}