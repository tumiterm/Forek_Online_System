
using ApprenticeApplications.Models;
using ApprenticeApplications.Models.Enums;
using ForekRequisition.Models.ViewModel;
using ForekRequisition.Services;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace ForekRequisition.Models.Helpers
{
    public static class Helper
    {
        private static Random _random = new Random();
        private static string _username = $"apprentice@forek.co.za";
        private static string _password = "P@55w0rd2022";

        public static string loggedInUser = "";
        public static int RandomNumber(int min, int max)
        {
            return _random.Next(min, max);
        }
        public static string EncryptEntry(string ToEncrypt)
        {
            return Convert.ToBase64String(Encoding.ASCII.GetBytes(ToEncrypt));
        }
        public static string DecryptInput(string cypherString)
        {
            return Encoding.ASCII.GetString(Convert.FromBase64String(cypherString));
        }
        public static void SendEmailNotification(string reciever, string subject, string message)
        {
            var senderMail = new MailAddress(_username, "Forek Apprentice");

            var recieverMail = new MailAddress(reciever, "Apprentice Applicant");

            var password = _password;

            var sub = subject;

            var body = message;

            var smtp = new SmtpClient
            {
                Host = "smtp.forek.co.za",

                Port = 587,

                EnableSsl = true,

                DeliveryMethod = SmtpDeliveryMethod.Network,

                UseDefaultCredentials = false,

                Credentials = new NetworkCredential(senderMail.Address, password)
            };

            using (var mess = new MailMessage(senderMail, recieverMail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true,

            })

            {

                //mess.Attachments.Add(new Attachment("C:\\file.zip"));

                smtp.Send(mess);
            }

        }
        public static void SendEmailWithAttachments(string file, string message)
        {
            string subject = "Applicant Attachments";
            string reciever = "fortuneismaname@gmail.com";

            var senderMail = new MailAddress(_username, "Forek Apprentice");

            var recieverMail = new MailAddress(reciever, "Apprentice Applicant");

            var password = _password;

            var sub = subject;

            var body = message;

            var smtp = new SmtpClient
            {
                Host = "smtp.forek.co.za",

                Port = 587,

                EnableSsl = true,

                DeliveryMethod = SmtpDeliveryMethod.Network,

                UseDefaultCredentials = false,

                Credentials = new NetworkCredential(senderMail.Address, password)
            };

            using (var mess = new MailMessage(senderMail, recieverMail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true,

            })

            {

                mess.Attachments.Add(new Attachment(file));

                smtp.Send(mess);
            }

        }


        private static Random random = new Random();
        public static string RandomStringGenerator(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public static string EncryptInput(string input)
        {
            return Convert.ToBase64String(
               System.Security.Cryptography.SHA256.Create()
               .ComputeHash(Encoding.UTF8.GetBytes(input))
               );
        }
        public static Guid GenerateGuid()
        {
            return Guid.NewGuid();
        }
        public static async Task OnSendSMSNotification(SMSViewModel model)
        {
            bool isGood = false;

            SMSService sms = new SMSService();

            HttpClient client = sms.Initialize();

            var postSMSTask = client.PostAsJsonAsync<SMSViewModel>("api/rest/v1/sms/outgoing/send", model);

            postSMSTask.Wait();

            var results = postSMSTask.Result;

            if (results.IsSuccessStatusCode)
            {
                isGood = true;
            }
        }
        public static List<String> ConvertProvinceEnum()
        {
            return Enum.GetValues(typeof(eProvince)).
                 Cast<eProvince>().
                 Select(m => m.ToString()).
                 ToList();
        }
        public static List<String> ConvertMunicipalityEnum()
        {
            return Enum.GetValues(typeof(eProvince)).
                 Cast<eMunicipality>().
                 Select(m => m.ToString()).
                 ToList();
        }
        public static string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }
        public static Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformatsofficedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }
        public static string AcknowledgementMessage(string name, string programme, string reference)
        {
            return $"Good day {name}<br/><br/>Thank your for applying for the <b> {programme} </b> programme 2022." +
                $" We'd like to confirm that we recieved your application and that our team is currently reviewing it along with other" +
                $" applications - we will inform you of the status of your application in due time.<br/>" +
                $"" +
                $"Your reference number is: <b>{reference}<b/><br/>Which should be used in all forms of communication.<br/><br/>Warm Regards.<br/><hr/>" +
                $" <b>Forek Institute of Technology<b/>";
        }
        public static void OnSendOfferMail(string offer, string reciever, string subject, string messageBody)
        {
            var senderMail = new MailAddress(_username, "Forek Online Applicants");

            var recieverMail = new MailAddress(reciever, "Forek Applicantions 2022");

            var password = _password;

            var sub = subject;

            var body = messageBody;

            var smtp = new SmtpClient
            {
                Host = "smtp.forek.co.za",

                Port = 587,

                EnableSsl = true,

                DeliveryMethod = SmtpDeliveryMethod.Network,

                UseDefaultCredentials = false,

                Credentials = new NetworkCredential(senderMail.Address, password)
            };

            using (var mess = new MailMessage(senderMail, recieverMail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true,

            })

            {

                //mess.Attachments.Add(new Attachment("C:\\file.zip"));

                smtp.Send(mess);
            }
        } 

        public static string OfferMessage(string name, string lastname, string ID,string status)
        {
            return $"Dear Mr L Mhlongo<br/>" +
                $"This email serves to officially notify you of the offer that I as:<br/><br/>" +
                $"<b>Name<b/>: {name} {lastname}<br/><b>RSA ID Number<b/>: {ID}<br/><br/>" +
                $"Would like to state that i am: {status} for the apprenticeship programme.";
        }

        public static string MessageRequest(string name, string lastname, string course)
        {
            return   "<!DOCTYPE html>" +
               "<html> " +
                   "<body style=\"background -color:#ff7f26;text-align:center;\"> " +
                   "<h1 style=\"color:#051a80;\">Forek Institute of Technology</h1> " +
                   "<h2 style=\"color:#051a80;\">Online Applications 2022</h2> " +
                   "<label style=\"color:blue;font-size:100px;border:5px dotted;border-radius:50px\">FIT</label> " +"<br/>"+
                   "<h2 style=\"color:#051a80;\">This notification serves to inform you that we recieved an application for</h2> " +
                   "<h2 style=\"color:#051a80;\"> Student:" + name + "" + lastname +
                   "</h2> " +
                  "<h2 style=\"color:#051a80;\">Course: " + course +
                  "</h2> " +
                 "<h2 style=\"color:#051a80;\">Regards</h2> " +

                   "</body> " +
               "</html>";

        }

        public static string ResponseMessage(string name, string lastname, string ID, string programme)
        {
            return $"Dear Sir/Madam<br/>" +
                $"This email serves to officially notify you that you've recieved a new application for:<br/><br/>" +
                $"<b>Name<b/>: {name} {lastname}<br/><b>RSA ID Number<b/>: {ID}<br/>" +
                $"<b>Programme<b/>: {programme}";
        }

    }
}