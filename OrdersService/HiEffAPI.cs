using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OrdersService
{
    class HiEffAPI
    {
        private string apiUrlBase;
        private string loginUrl;
        private string currentFIFOUrl;
        private string changeRobotStatusUrl;
        private string webSocketUrl;

        readonly string _accountSid;
        readonly string _secretKey;

        public HiEffAPI()
        {
            apiUrlBase = ConfigurationManager.AppSettings["ApiUrlBase"];
            loginUrl = ConfigurationManager.AppSettings["LoginUrl"];
            currentFIFOUrl = ConfigurationManager.AppSettings["CurrentFIFOUrl"];
            changeRobotStatusUrl = ConfigurationManager.AppSettings["ChangeRobotStatusUrl"];
            webSocketUrl = ConfigurationManager.AppSettings["WebSocketUrl"];

           // _accountSid = "robot@hiefficiencybar.com";
           // _secretKey = "password1231";

            _accountSid = ConfigurationManager.AppSettings["Username"];
            _secretKey = ConfigurationManager.AppSettings["Password"];

        }

        public T Execute<T>(RestRequest request) where T : new()
        {
            var client = new RestClient();
            client.BaseUrl = new System.Uri(apiUrlBase);
            client.Authenticator = new HttpBasicAuthenticator(_accountSid, _secretKey);
            request.AddParameter("AccountSid", _accountSid, ParameterType.UrlSegment); // used on every request
            var response = client.Execute<T>(request);

            if (response.ErrorException != null)
            {
                const string message = "Error retrieving response.  Check inner details for more info.";
                var myException = new ApplicationException(message, response.ErrorException);
                throw myException;
            }
            return response.Data;
        }

        public List<Order> GetOrders()
        {
            var request = new RestRequest();
            request.Resource = currentFIFOUrl;
            request.RootElement = "Order";

            var client = new RestClient();
            client.BaseUrl = new System.Uri(apiUrlBase);
            client.Authenticator = new HttpBasicAuthenticator(_accountSid, _secretKey);
            request.AddParameter("AccountSid", _accountSid, ParameterType.UrlSegment); // used on every request
            var response = client.Execute<OrdersObj>(request);

            if (response.ErrorException != null)
            {
                const string message = "Error retrieving response.  Check inner details for more info.";
                var myException = new ApplicationException(message, response.ErrorException);
                throw myException;
            }
            List<Order> orders = response.Data.results;
            return orders;
            //return response.Data;

            //request.AddParameter("CallSid", callSid, ParameterType.UrlSegment);

           // return Execute<List<Order>>(request);
        }

        public string GetOrdersJSON()
        {
            var request = new RestRequest();
            request.Resource = currentFIFOUrl;
            request.RootElement = "Order";

            var client = new RestClient();
            client.BaseUrl = new System.Uri(apiUrlBase);
            client.Authenticator = new HttpBasicAuthenticator(_accountSid, _secretKey);
            request.AddParameter("AccountSid", _accountSid, ParameterType.UrlSegment); // used on every request
            var response = client.Execute<OrdersObj>(request);

            if (response.ErrorException != null)
            {
                const string message = "Error retrieving response.  Check inner details for more info.";
                var myException = new ApplicationException(message, response.ErrorException);
                throw myException;
            }
            return response.Content;
            
            //return response.Data;

            //request.AddParameter("CallSid", callSid, ParameterType.UrlSegment);

            // return Execute<List<Order>>(request);
        }
    }
}
