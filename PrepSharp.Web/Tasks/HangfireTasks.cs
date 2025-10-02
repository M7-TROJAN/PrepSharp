using Microsoft.AspNetCore.Identity.UI.Services;
using PrepSharp.Web.Services;

namespace PrepSharp.Web.Tasks
{
    public class HangfireTasks
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IEmailBodyBuilder _emailBodyBuilder;
        private readonly IEmailSender _emailSender;

        public HangfireTasks(ApplicationDbContext context,
            IWebHostEnvironment webHostEnvironment,
            IEmailBodyBuilder emailBodyBuilder,
            IEmailSender emailSender)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _emailBodyBuilder = emailBodyBuilder;
            _emailSender = emailSender;
        }

        // here 
    }
}