using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarInsurance.Models;
using CarInsurance.ViewModels;

namespace CarInsurance.Controllers
{
    public class AdminController : Controller
        {
            public ActionResult Index()
            {
                using (
                    InsuranceEntities db = new InsuranceEntities())
                {

                    var theirquotes = (from x in db.Tables
                                        where x.Id > 0
                                        select x).ToList();

                    var quoteforAdmin = new List<CustomerQuotes>();
                    foreach (var quote in theirquotes)
                    {
                    var adminlist = new CustomerQuotes
                    {
                        FirstName = quote.FirstName,
                        LastName = quote.LaststName,
                        EmailAddress = quote.EmailAddress,
                        Quote = (int)quote.Quote
                    };
                    quoteforAdmin.Add(adminlist);
                    }


                    return View(quoteforAdmin);
                }
            }
        }
    }