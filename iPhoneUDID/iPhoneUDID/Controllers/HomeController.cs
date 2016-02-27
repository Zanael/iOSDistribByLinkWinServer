using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Xml.Linq;

namespace iPhoneUDID.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            XDocument databaseXML;
            try
            {
                databaseXML = XDocument.Load(System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/udids.xml"));
            }
            catch (Exception exp)
            {
                databaseXML = new XDocument(new XElement("udids"));
            }

            List<Models.iPhoneUDID> UDIDs = new List<Models.iPhoneUDID>();
            XElement iPhones = databaseXML.Element("udids");
            foreach (XElement e in iPhones.Elements("iPhone"))
            {
                UDIDs.Add(new Models.iPhoneUDID { TimeAdded = e.Element("TimeAdded").Value, UDID = e.Element("UDID").Value });
            }
            ViewBag.UDIDs = UDIDs;

            return View();
        }

        [HttpGet]
        public ActionResult ClearHistory()
        {
            ViewBag.Title = "Home Page";

            XDocument databaseXML = new XDocument(new XElement("udids"));
            databaseXML.Save(Server.MapPath("/App_Data/udids.xml"));

            ViewBag.UDIDs = new List<Models.iPhoneUDID>();

            return View("Index");
        }
    }
}
