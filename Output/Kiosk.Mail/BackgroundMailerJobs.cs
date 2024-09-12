using Kiosk.Interfaces.Background;
using Kiosk.Mail.Models;
using System;

namespace Kiosk.Mail
{
    public partial class  BackgroundMailerJobs : IBackgroundMailerJobs
    {
        #region Properties

        //ToDo for add mail history
        //private readonly IMailHistoryService _mailHistoryService;
        private static readonly object MailServiceLock = new object();

        #endregion Properties

        #region Constructor

        public BackgroundMailerJobs()
        {
            //_mailHistoryService = mailHistoryService;
        }

        #endregion Constructor

        public void SendWelcomeEmail()
        {
            var welcomeEmailModel = new WelcomeEmail
            {
                RecipientMail = "Laminds@gmail.com",
                DisplayName = "La" + " " + "Minds",
            };
            var mail = new Mail<WelcomeEmail>("WelcomeEmail", welcomeEmailModel);
            lock (MailServiceLock)
            {
                var sentMailData = mail.Send(welcomeEmailModel.RecipientMail, "Welcome to LaMinds network");
                //_mailHistoryService.InsertMailHistory(sentMailData.To.ToString(), sentMailData.Subject, sentMailData.Body, MailTypeEnum.Registration.ToString());
            }
        }
    }
}