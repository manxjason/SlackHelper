using System;
using Newtonsoft.Json;

namespace ManxJason.SlackHelper.Attachments
{
    /// <summary>
    /// https://api.slack.com/docs/message-attachments
    /// </summary>
    [Serializable]
    public sealed class Attachment
    {
        public string Fallback { get; set; }
        public string Color { get; set; }
        public string PreText { get; set; }

        [JsonProperty("author_name")]
        public string AuthorName { get; set; }

        [JsonProperty("author_link")]
        public string AuthorLink { get; set; }

        [JsonProperty("author_icon")]
        public string AuthorIcon { get; set; }

        public string Title { get; set; }

        [JsonProperty("title_link")]
        public string TitleLink { get; set; }

        public string Text { get; set; }
        public Field[] Fields { get; set; }

        [JsonProperty("image_url")]
        public string ImageUrl { get; set; }

        [JsonProperty("thumb_url")]
        public string ThumbUrl { get; set; }

        public string Footer { get; set; }

        [JsonProperty("footer_icon")]
        public string FooterIcon { get; set; }

        public string Ts { get; set; }
    }
}