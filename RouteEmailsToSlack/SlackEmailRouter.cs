using System;
using System.Threading.Tasks;

namespace RouteEmailsToSlack
{
    public class SlackEmailRouter
    {
        private readonly string _channelName;
        private readonly string _urlWithAccessToken;

        public SlackEmailRouter(string urlWithAccessToken, string channelName)
        {
            _urlWithAccessToken = urlWithAccessToken;
            _channelName = channelName;
        }

        public async Task SendAsync(string toEmail, string toName, string subject, string body)
        {
            if (string.IsNullOrEmpty(_urlWithAccessToken))
            {
                throw new ArgumentException("url with access token not supplied");
            }
            if (string.IsNullOrEmpty(_channelName))
            {
                throw new ArgumentException("channel name not supplied");
            }

            var client = new SlackClient(_urlWithAccessToken);

            await Task.Run(() =>
            {
                client.PostMessage(username: $"{toName} ({toEmail})",
                    text: $"{subject} \r\n {body}",
                    channel: _channelName);
            });
        }
    }
}