using IReport.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace IReportWeb.Controllers
{
    public class MastersController : ApiController
    {
        public IEnumerable<ClientInfoModel> GetClients()
        {
            List<ClientInfoModel> clientInfo = new List<ClientInfoModel>();
            clientInfo.Add(new ClientInfoModel
            {
                Identifier = 4,
                ClientAddress = "TEST"
            });

            return clientInfo;
        }
    }
}
