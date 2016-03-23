RouteEmailsToSlack  
==============

Ever wanted to have multiple inboxes to test emails in dev and test enviornments? Here is an alternative, send them all to slack! ( Because, why not!)

How to? 

1. Create a slack channel to receive emails
   https://get.slack.help/hc/en-us/articles/201402297-Creating-a-channel

2. Create a incoming web-hook for the channel 
   https://api.slack.com/incoming-webhooks

3. Say you have configured your email addresses with test.com domain in dev and test and inside 
   the email delivery method you can do something like below..  

      
      ``` C#

	SendEmail(string receiverName ,string receiverEmail, string subject , string body )
	{
		if(receiverEmail.EndsWith('test.com'))
		{
			var slackEmailRouter = new SlackEmailRouter("https://your/web-hook/url" , "#your-channel-name");
			slackEmailRouter.Send(receiverName,receiverEmail,subject,body);
		 }
		else
		{
			//Your real SMTP logic here
		}
	}

      ```

	  
