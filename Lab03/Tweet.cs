using System;

namespace Lab03;

public class Tweet
{
    public string Text { get; set; }
    public string UserName { get; set; }
    public string LinkToTweet { get; set; }
    public string FirstLinkUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public string TweetEmbedCode { get; set; }
}