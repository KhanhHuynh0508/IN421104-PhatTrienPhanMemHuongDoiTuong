using EduHub.Ch02.Domain;

namespace EduHub.Ch02;

// --- DESIGN PATTERN: OBSERVER ---
public interface IEventObserver
{
	void OnEventCreated(Event ev, Club club);
}

public class LogNotifier : IEventObserver
{
	private readonly IAppLogger _logger;
	public LogNotifier(IAppLogger logger) => _logger = logger;

	public void OnEventCreated(Event ev, Club club)
	{
		_logger.Info($"Event created: '{ev.Title}' ({club.Name})");
	}
}

public class EmailNotifier : IEventObserver
{
	private readonly IMemberRepository _members;
	private readonly INotificationService _notifier;

	public EmailNotifier(IMemberRepository members, INotificationService notifier)
	{
		_members = members;
		_notifier = notifier;
	}

	public void OnEventCreated(Event ev, Club club)
	{
		var members = _members.GetByClub(club.Id);
		foreach (var m in members)
		{
			_notifier.Send(m.Email, $"New event '{ev.Title}' at {club.Name}");
		}
	}
}

public class EventService
{
	private readonly IClubRepository _clubs;
	private readonly List<IEventObserver> _observers = new();

	public EventService(IClubRepository clubs)
	{
		_clubs = clubs;
	}

	public void Subscribe(IEventObserver observer) => _observers.Add(observer);

	public Event CreateEvent(Guid clubId, string title, DateTime date)
	{
		var club = _clubs.GetById(clubId) ?? throw new InvalidOperationException("Club not found");
		var ev = new Event { Id = Guid.NewGuid(), ClubId = clubId, Title = title, Date = date };

		// Báo cho các observer (Observer pattern)
		foreach (var observer in _observers.ToList())
		{
			observer.OnEventCreated(ev, club);
		}

		return ev;
	}
}

public class ClubService
{
	private readonly IClubRepository _clubs;
	private readonly IMemberRepository _members;
	private readonly IAppLogger _logger;

	public ClubService(IClubRepository clubs, IMemberRepository members, IAppLogger logger)
	{
		_clubs = clubs;
		_members = members;
		_logger = logger;
	}

	public Club CreateClub(string name, string description, string presidentEmail, NotificationChannel channel, INotificationServiceFactory factory)
	{
		var club = Club.Create(name, description);
		_clubs.Add(club);

		_logger.Info($"Club created: {name}");

		// Dùng Factory để tạo kênh thông báo phù hợp
		var notifier = factory.Create(channel);
		notifier.Send(presidentEmail, "Club created successfully!");

		return club;
	}

	public void AddMember(Guid clubId, string memberName, string email)
	{
		var club = _clubs.GetById(clubId) ?? throw new InvalidOperationException("Club not found");

		if (_members.CountByClub(clubId) >= Club.MaxMembers)
			throw new InvalidOperationException("Club is full");

		var member = new Member { Id = Guid.NewGuid(), Name = memberName, Email = email, ClubId = clubId };
		_members.Add(member);

		_logger.Info($"Member {memberName} joined {club.Name}");
	}

	public List<Club> GetAllClubs() => _clubs.GetAll();
}