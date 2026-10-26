namespace EduHub.Ch02;

public enum NotificationChannel { Email, Sms, Slack }

public interface INotificationService
{
    void Send(string to, string message);
}

public class EmailNotificationService : INotificationService
{
    public void Send(string to, string message) => Console.WriteLine($"[EMAIL] to {to}: {message}");
}

public class SmsNotificationService : INotificationService
{
    public void Send(string to, string message) => Console.WriteLine($"[SMS] to {to}: {message}");
}

public class SlackNotificationService : INotificationService
{
    public void Send(string to, string message) => Console.WriteLine($"[SLACK] to {to}: {message}");
}

// --- DESIGN PATTERN: FACTORY ---
public interface INotificationServiceFactory
{
    INotificationService Create(NotificationChannel channel);
}

public class NotificationServiceFactory : INotificationServiceFactory
{
    public INotificationService Create(NotificationChannel channel) => channel switch
    {
        NotificationChannel.Email => new EmailNotificationService(),
        NotificationChannel.Sms => new SmsNotificationService(),
        NotificationChannel.Slack => new SlackNotificationService(),
        _ => throw new ArgumentOutOfRangeException(nameof(channel))
    };
}