namespace Kiosk.Interfaces.Background
{
    public interface IBackgroundMailerJobs : IBackgroundJobs
    {
        void SendWelcomeEmail();
    }
}