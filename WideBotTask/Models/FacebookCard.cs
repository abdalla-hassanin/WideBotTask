

namespace WideBotProject.Models
{
    public class FacebookCard
    {
        public string title { get; set; }
        public string image_url { get; set; }
        public string subtitle { get; set; }
        public DefaultAction default_action { get; set; }
        public List<Button> buttons { get; set; }

    }

    public class DefaultAction
    {
        public string type { get; set; }
        public Uri targetUri { get; set; }
        public string webview_height_ratio { get; set; }
        public DefaultAction(string email)
        {
          

            this.type = "web_url";

            this.targetUri = new Uri("https://mail.google.com/mail/u/0/?fs=1&tf=cm&to=" + email + "&su=Hello&body=Send%20Email");

            this.webview_height_ratio = "tall";
        }
    }

    public class Button
    {
        
        public string type { get; set; }
        public Uri targetUri { get; set; }
        public string title { get; set; }
        public Button(string email)
        {

            this.type = "web_url";

            this.targetUri = new Uri("https://mail.google.com/mail/u/0/?fs=1&tf=cm&to=" + email + "&su=Hello&body=Send%20Email");

            this.title = "Send Email";
        }
    }
}
