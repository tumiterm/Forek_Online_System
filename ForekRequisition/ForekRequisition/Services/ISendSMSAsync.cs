using ForekRequisition.Models.ViewModel;

namespace ForekRequisition.Services
{
    public interface ISendSMSNotification
    {
         Task<bool> SendSMSAsync(SMSViewModel model);
    }
}
