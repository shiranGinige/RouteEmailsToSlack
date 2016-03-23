using System;
using System.Text;
using System.Threading.Tasks;

namespace Klabs.RouteEmailsToSlack
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

            string lineBreak =  "----------------------------------------------";
            String messageStartEnd = "==============================================";
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(messageStartEnd);
            sb.AppendLine($"To :  {toName} ({toEmail})");
            sb.AppendLine(lineBreak);

            sb.AppendLine($"Subject : {subject}");
            sb.AppendLine(lineBreak);

            sb.AppendLine(body);

            sb.AppendLine(messageStartEnd);

            await Task.Run(() =>
            {
                client.PostMessage(username: $"{toName} ({toEmail})",
                    text: sb.ToString(),
                    channel: _channelName);
            });
        }
    }
}