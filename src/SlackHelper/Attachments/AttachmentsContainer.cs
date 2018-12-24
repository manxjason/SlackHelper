using System;
using Newtonsoft.Json;

namespace ManxJason.SlackHelper.Attachments
{
    [Serializable]
    internal sealed class AttachmentsContainer
    {
        [JsonProperty("attachments")]
        public Attachment[] Attachments { get; set; }
    }
}