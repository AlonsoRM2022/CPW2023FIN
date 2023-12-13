using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ParkerWeb.Models.PayPalModel
{
    public class CN_PayPal
    {
        private static string urlpaypal = ConfigurationManager.AppSettings["UrlPaypal"];
        private static string clientId = ConfigurationManager.AppSettings["ClientId"];
        private static string secret = ConfigurationManager.AppSettings["Secret"];

        public async Task<Response_PayPal<Response_Checkout>> CreateRequest(Checkout_Order orden)
        {
            Response_PayPal<Response_Checkout> response_paypal = new Response_PayPal<Response_Checkout>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlpaypal);
                var authToken = Encoding.ASCII.GetBytes($"{clientId}:{secret}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authToken));
                var json = JsonConvert.SerializeObject(orden);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("/v2/checkout/orders", data);
                response_paypal.Status = response.IsSuccessStatusCode;
                if (response.IsSuccessStatusCode)
                {
                    string jsonRespuesta = response.Content.ReadAsStringAsync().Result;
                    Response_Checkout checkout = JsonConvert.DeserializeObject<Response_Checkout>(jsonRespuesta);
                    response_paypal.Response = checkout;
                }
                return response_paypal;
            }
        }

        public async Task<Response_PayPal<Response_Capture>> ApprovePayment(string token)
        {
            Response_PayPal<Response_Capture> response_paypal = new Response_PayPal<Response_Capture>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlpaypal);
                var authToken = Encoding.ASCII.GetBytes($"{clientId}:{secret}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authToken));
                var data = new StringContent("{}", Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync($"/v2/checkout/orders/{token}/capture", data);
                response_paypal.Status = response.IsSuccessStatusCode;
                if (response.IsSuccessStatusCode)
                {
                    string jsonRespuesta = response.Content.ReadAsStringAsync().Result;
                    Response_Capture capture = JsonConvert.DeserializeObject<Response_Capture>(jsonRespuesta);
                    response_paypal.Response = capture;
                }
                return response_paypal;
            }
        }
    }
}