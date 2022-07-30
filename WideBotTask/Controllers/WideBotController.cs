
using Microsoft.AspNetCore.Mvc;
using Nancy.Json;
using System.Net;
using WideBotProject.Models;
namespace WideBotTask.Controllers
{
    public class WidebotController : Controller
    { 

        [Route("widebot/get")]
        public string Get()
        {
            Uri targetUri = new Uri("https://reqres.in/api/users");

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(targetUri);

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


            List<FacebookCard> facebookCards = new List<FacebookCard>();
            List<Button> buttons;

            foreach (var user in users)
            {
               
                buttons = new List<Button>();
                buttons.Add(new Button(user.email));

                facebookCards.Add(new FacebookCard
                {
                    title = user.first_name,
                    image_url = user.avatar,
                    subtitle = user.last_name,
                    default_action = new DefaultAction(user.email),
                    buttons = buttons
            }) ;

            }

            return facebookCards;
        }
    }
    
    
   
}
