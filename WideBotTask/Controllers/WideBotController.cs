
using Microsoft.AspNetCore.Mvc;
using Nancy.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using WideBotProject.Models;
namespace WideBotTask.Controllers
{
    public class WidebotController : Controller
    {
        [Route("widebot")]
        public string Index()
        {
            return "Hey there";
            //return View();
        }


        [Route("widebot/get")]
        public string Get()
        {
            Uri targetUri = new Uri("https://reqres.in/api/users");

            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest) System.Net.HttpWebRequest.Create(targetUri);

            var response = request.GetResponse() as HttpWebResponse;

            var reader = new StreamReader(response.GetResponseStream());

            var objText = reader.ReadToEnd();

            JavaScriptSerializer serializer = new JavaScriptSerializer();

            var ser = serializer.Deserialize<JsonModel>(objText);

           
            Attributes attribute =getAttribute(ser);

            List<FacebookCard> facebookCards = convertToWideBot(ser);

            WideBotAPI wideBotAPI = new WideBotAPI { attributes = attribute, flowName = ser.support, FacebookResponse = facebookCards };

            var json = new JavaScriptSerializer().Serialize(wideBotAPI);

            return json;
        }


        public Attributes getAttribute(JsonModel ser)
        {
            int page = ser.page;
            int per_page = ser.page;
            int total = ser.total;
            int total_pages = ser.total_pages;

            Attributes attribute = new Attributes { page = page, per_page = per_page, total = total, total_pages = total_pages };

            return attribute;
        }

        public List<FacebookCard> convertToWideBot(JsonModel ser)
        {
            List<User> users = ser.data;

            string email = null;
            string first_name = null;
            string last_name = null;
            string avatar = null;
            DefaultAction default_action = null;
            Button button = null;
            FacebookCard facebook_card = null;

            List<FacebookCard> facebookCards = new List<FacebookCard>();

            List<Button> buttons;

            foreach (var user in users)
            {
                email = user.email;
                first_name = user.first_name;
                last_name = user.last_name;
                avatar = user.avatar;
                buttons = new List<Button>();

                default_action = new DefaultAction(email);
                button = new Button(email);

                buttons.Add(button);

                facebook_card = new FacebookCard
                {
                    title = first_name,
                    image_url = avatar,
                    subtitle = last_name,
                    default_action = default_action,
                    buttons = buttons
                };

                facebookCards.Add(facebook_card);

            }

            return facebookCards;
        }
    }
    
    
   
}
