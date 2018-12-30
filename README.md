# SlackHelper
Lightweight async .Net Standard library for posting Slack messages or attachments via webhook integration.

## Install
`Install-Package ManxJason.SlackHelper`

## Usage

### Send a plain text message
```
SlackHelper helper = new SlackHelper(
        new Uri("your slack webhook uri"));

HttpResponseMessage result = await helper.SendAsync("your message");
```
### Send attachment(s)
Supports single `Attachment` or `Attachment[]`
```
SlackHelper helper = new SlackHelper(
        new Uri("your slack webhook uri"));

Attachment attachment = new Attachment
    {
        Fallback = "Fallback message",
        PreText = "Pre-text at the start of message",
        Text = "Enter text here",
        Color = "warning or #####",
        Fields = new Field[]
            {
                new Field("title", "value", true), 
                new Field("title", "value")
            },
                AuthorName = "manxjason"
                //additional properties..
            };

 HttpResponseMessage result = await helper.SendAsync(attachment);
```

### Send your own slack compliant message (see Slack documentation)
```
SlackHelper helper = new SlackHelper(
        new Uri("your slack webhook uri"));

var myMessage = new {text = "your message"};

 HttpResponseMessage result = await helper.SendAsync(attachment);
 ```