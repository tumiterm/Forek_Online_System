namespace ForekRequisition.Services
{
    public class SMSService
    {
        public HttpClient Initialize()
        {
            var client = new HttpClient();

            client.BaseAddress = new Uri("https://www.winsms.co.za/");

            return client;
        }
    }
}
