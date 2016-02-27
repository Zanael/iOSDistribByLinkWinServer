using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Xml.Linq;

namespace iPhoneUDID.Controllers
{
    public class XmlController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage PostRawXMLMessage(HttpRequestMessage request)
        {
            string plist = request.Content.ReadAsStringAsync().Result;

            string UDID = "Error";
            try
            {
                int begin = plist.IndexOf("UDID") + 20;
                int end = plist.IndexOf("<", begin);
                UDID = plist.Substring(begin, end - begin);
            }
            catch (Exception exc)
            {
                //
            }


            XDocument databaseXML;
            try
            {
                databaseXML = XDocument.Load(System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/udids.xml"));
            }
            catch (Exception exp)
            {
                databaseXML = new XDocument(new XElement("udids"));
            }

            databaseXML.Element("udids").Add(
                new XElement("iPhone",
                    new XElement("TimeAdded", DateTime.Now.ToLongTimeString() + " - " + DateTime.Now.ToShortDateString()),
                    new XElement("UDID", UDID)));
            databaseXML.Save(System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/udids.xml"));

            HttpResponseMessage response = request.CreateResponse(HttpStatusCode.MovedPermanently);
            response.Headers.Add("Location", "/Thanks");
            return response;
        }
    }
}
