using System;

using Microsoft.AspNetCore.Mvc;
using net_core_mvc.Factories;
using net_core_mvc.Models;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;

namespace net_core_mvc.Controllers
{
    public class MessageController : Controller
    {
        private readonly MessageFactory _messageFactory;
        
        public MessageController(MessageFactory messageFactory)
        {
            _messageFactory = messageFactory;
        }
        // GET: /Home/
        [HttpGet]
        [Route("")]
        
        public IActionResult Index()
        {
           
            return View();
        }
        [HttpPost]
        [Route("GuestBook")]
           public IActionResult Add(Message model)
        {
           
           
            if(ModelState.IsValid)
            {
                 Console.WriteLine(model.Name);
                 Console.WriteLine("  Thanks for the Note!  ");
                _messageFactory.Add(model);
                
                return RedirectToAction("GuestBook", model); 
            }
            ViewBag.Errors = ModelState.Values;
            return View("Index");
        }
        [HttpGet]
        [Route("GuestBook")]
        public IActionResult GuestBook()
        {
            var user = _messageFactory.GetUserById();
            var allmessages = _messageFactory.All();
            ViewBag.allmessages = allmessages;
            return View();
        }
       
       [HttpGet]
        [Route("FormSubmit")]
       public IActionResult FormSubmit()
       {
           Console.WriteLine("Hello World!");
            MailMessage mail = new MailMessage()
            {
                From = new MailAddress("lancewellspring@gmail.com")
            };
            mail.To.Add(new MailAddress("txjoe75@gmail.com"));
            mail.CC.Add(new MailAddress("lancewellspring@gmail.com"));

            mail.Subject = ".NET Core smtp test";
            mail.Body = "hello from vscode";
            mail.IsBodyHtml = false;
            mail.Priority = MailPriority.High;
            sendMail(mail).Wait();
            return Json(new {});
       }

       static async Task sendMail(MailMessage mail)
        {
            using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
            {
                smtp.Credentials = new NetworkCredential("lancewellspring@gmail.com", "pwd");
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(mail);
            }
        }
    }
}
