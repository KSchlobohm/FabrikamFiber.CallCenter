namespace FabrikamFiber.Web.Controllers
{
using System.Web;
    using System.Web.Mvc;

    using FabrikamFiber.DAL.Data;
    using FabrikamFiber.Web.ViewModels;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
    using Microsoft.Owin.Security;

    public class HomeController : Controller
    {
        private readonly IServiceTicketRepository serviceTickets;
        private readonly IMessageRepository messageRepository;
        private readonly IAlertRepository alertRepository;
        private readonly IScheduleItemRepository scheduleItemRepository;

        public HomeController(
                              IServiceTicketRepository serviceTickets,
                              IMessageRepository messageRepository,
                              IAlertRepository alertRepository,
                              IScheduleItemRepository scheduleItemRepository)
        {
            this.serviceTickets = serviceTickets;
            this.messageRepository = messageRepository;
            this.alertRepository = alertRepository;
            this.scheduleItemRepository = scheduleItemRepository;
        }

        public ActionResult Index()
        {
            var viewModel = new DashboardViewModel
            {
                ScheduleItems = this.scheduleItemRepository.All,
                Messages = this.messageRepository.All,
                Alerts = this.alertRepository.All,
                Tickets = this.serviceTickets.AllIncluding(serviceticket => serviceticket.Customer, serviceticket => serviceticket.CreatedBy, serviceticket => serviceticket.AssignedTo),
            };

            return View(viewModel);
        }

        /// <summary>
        /// Send an OpenID Connect sign-in request.
        /// Alternatively, you can just decorate the SignIn method with the [Authorize] attribute
        /// </summary>
        public void SignIn()
        {
            if (!Request.IsAuthenticated)
            {
                HttpContext.GetOwinContext().Authentication.Challenge(
                    new AuthenticationProperties { RedirectUri = "/" },
                    OpenIdConnectAuthenticationDefaults.AuthenticationType);
            }
        }

        /// <summary>
        /// Send an OpenID Connect sign-out request.
        /// </summary>
        public void SignOut()
        {
            HttpContext.GetOwinContext().Authentication.SignOut(
                    OpenIdConnectAuthenticationDefaults.AuthenticationType,
                    CookieAuthenticationDefaults.AuthenticationType);
        }
    }
}
