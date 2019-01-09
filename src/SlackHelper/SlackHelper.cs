using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ManxJason.SlackHelper.Attachments;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

// ReSharper disable AsyncConverter.AsyncAwaitMayBeElidedHighlighting

namespace ManxJason.SlackHelper
{
    public sealed class SlackHelper : ISlackHelper
    {
        private readonly Uri _incomingWebHook;

        /// <summary>
        /// Obtain the incoming web hook from your Slack configuration options.
        /// </summary>
        /// <param name="incomingWebHook"></param>
        public SlackHelper(Uri incomingWebHook) =>
            _incomingWebHook = incomingWebHook ?? throw new ArgumentNullException();

        /// <summary>
        /// Send a regular string value via web hook.
        /// </summary>
        /// <param name="message">Plain old text value.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> SendAsync(
            string message,
            CancellationToken cancellationToken = default(CancellationToken)) =>
            await PostToSlackAsync(
                    _incomingWebHook,
                    new
                    {
                        text = message
                    },
                    cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Send a single attachment via web hook.
        /// </summary>
        /// <param name="attachment"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> SendAsync(
            Attachment attachment,
            CancellationToken cancellationToken = default(CancellationToken)) =>
            await PostToSlackAsync(
                    _incomingWebHook,
                    new AttachmentsContainer
                    {
                        Attachments = new[]
                        {
                            attachment
                        }
                    },
                    cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Send an array of attachments via web hook.
        /// </summary>
        /// <param name="attachments"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> SendAsync(
            Attachment[] attachments,
            CancellationToken cancellationToken = default(CancellationToken)) =>
            await PostToSlackAsync(
                    _incomingWebHook,
                    new AttachmentsContainer
                    {
                        Attachments = attachments
                    },
                    cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Build your own anonymous object following Slack documentation to send your customized message.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>HttpResponse</returns>
        public Task<HttpResponseMessage> SendAsync(
            object message,
            CancellationToken cancellationToken = default(CancellationToken)) =>
            PostToSlackAsync(_incomingWebHook, message, cancellationToken);

        private static async Task<HttpResponseMessage> PostToSlackAsync(
            Uri slackHook,
            object content,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            using (HttpClient client = new HttpClient())
            {
                string serializeObject = JsonConvert.SerializeObject(
                    content,
                    Formatting.None,
                    new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        ContractResolver = new CamelCasePropertyNamesContractResolver
                        {
                            NamingStrategy = new CamelCaseNamingStrategy()
                        }
                    });
                StringContent stringContent = new StringContent(serializeObject, Encoding.UTF8, "application/json");

                return await client.PostAsync(slackHook, stringContent, cancellationToken).ConfigureAwait(false);
            }
        }
    }
}
