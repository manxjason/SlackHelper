using System.Net.Http;
using System.Threading.Tasks;
using ManxJason.SlackHelper.Attachments;

namespace ManxJason.SlackHelper
{
    public interface ISlackHelper
    {
        Task<HttpResponseMessage> SendAsync(string message);
        Task<HttpResponseMessage> SendAsync(Attachment attachment);
        Task<HttpResponseMessage> SendAsync(Attachment[] attachments);
        Task<HttpResponseMessage> SendAsync(object message);
    }
}