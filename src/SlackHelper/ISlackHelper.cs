using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using ManxJason.SlackHelper.Attachments;

namespace ManxJason.SlackHelper
{
    public interface ISlackHelper
    {
        Task<HttpResponseMessage> SendAsync(string message, CancellationToken cancellationToken);
        Task<HttpResponseMessage> SendAsync(Attachment attachment, CancellationToken cancellationToken);
        Task<HttpResponseMessage> SendAsync(Attachment[] attachments, CancellationToken cancellationToken);
        Task<HttpResponseMessage> SendAsync(object message, CancellationToken cancellationToken);
    }
}