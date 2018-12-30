using System;
using System.Net.Http;
using System.Text;
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
        /// Obtain the incoming webhook from your Slack configuration options.
        /// </summary>
        /// <param name="incomingWebHook"></param>
        public SlackHelper(Uri incomingWebHook) =>
            _incomingWebHook = incomingWebHook ?? throw new ArgumentNullException();

        /// <summary>
        /// Send a regular string value via webhook.
        /// </summary>
        /// <param name="message">Plain old text value.</param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> SendAsync(string message) =>
            await PostToSlack(
                    _incomingWebHook,
                    new
                    {
                        text = message
                    })
                .ConfigureAwait(false);

        /// <summary>
        /// Send a single attachment via webhook.
        /// </summary>
        /// <param name="attachment"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> SendAsync(Attachment attachment) =>
            await PostToSlack(
                    _incomingWebHook,
                    new AttachmentsContainer
                    {
                        Attachments = new[]
                        {
                            attachment
                        }
                    })
                .ConfigureAwait(false);

        /// <summary>
        /// Send an array of attachments via webhook.
        /// </summary>
        /// <param name="attachments"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> SendAsync(Attachment[] attachments) =>
            await PostToSlack(
                    _incomingWebHook,
                    new AttachmentsContainer
                    {
                        Attachments = attachments
                    })
                .ConfigureAwait(false);

        /// <summary>
        /// Build your own anonymous object following Slack documentation to send your customised message.
        /// </summary>
        /// <param name="message"></param>
        /// <returns>HttpResponse</returns>
        public Task<HttpResponseMessage> SendAsync(object message) =>
            PostToSlack(_incomingWebHook, message);

        private static async Task<HttpResponseMessage> PostToSlack(Uri slackHook, object content)
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

                return await client.PostAsync(slackHook, stringContent).ConfigureAwait(false);
            }
        }
    }
}
