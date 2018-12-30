using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ManxJason.SlackHelper.Attachments;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ManxJason.SlackHelper
{
    public sealed class SlackHelper
    {
        /// <summary>
        /// Send a single attachment via webhook.
        /// </summary>
        /// <param name="incomingWebHook"></param>
        /// <param name="attachment"></param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> SendAsync(Uri incomingWebHook, Attachment attachment) =>
            await PostToSlack(
                    incomingWebHook,
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
        /// <param name="incomingWebHook"></param>
        /// <param name="attachments"></param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> SendAsync(Uri incomingWebHook, Attachment[] attachments) =>
            await PostToSlack(
                    incomingWebHook,
                    new AttachmentsContainer
                    {
                        Attachments = attachments
                    })
                .ConfigureAwait(false);

        /// <summary>
        /// Send a regular string value via webhook.
        /// </summary>
        /// <param name="incomingWebHook"></param>
        /// <param name="message">Plain old text value.</param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> SendAsync(Uri incomingWebHook, string message) =>
            await PostToSlack(
                    incomingWebHook,
                    new
                    {
                        text = message
                    })
                .ConfigureAwait(false);

        /// <summary>
        /// Build your own anonymous object following Slack documentation to send your customised message.
        /// </summary>
        /// <param name="incomingWebHook"></param>
        /// <param name="message"></param>
        /// <returns>HttpResponse</returns>
        public static async Task<HttpResponseMessage> SendAsync(Uri incomingWebHook, object message) =>
            await PostToSlack(incomingWebHook, message).ConfigureAwait(false);

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
