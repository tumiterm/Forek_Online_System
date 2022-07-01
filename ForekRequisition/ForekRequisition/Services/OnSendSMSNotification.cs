using ForekRequisition.Models.ViewModel;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ForekRequisition.Services
{
    public class OnSendSMSNotification : ISendSMSNotification
    {
        
        public async Task<bool> SendSMSAsync(SMSViewModel model)
        {
            var httpClient = new HttpClient();

            var json = JsonConvert.SerializeObject(model);

            HttpContent httpContent = new StringContent(json);

            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var httppResponse = await httpClient.PostAsync("www.winsms.co.za/api/rest/v1", httpContent);

            if (httppResponse.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
