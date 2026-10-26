using EduHub.Ch02.Domain;

namespace EduHub.Ch02;

public interface IMemberRepository
{
	void Add(Member member);
	List<Member> GetByClub(Guid clubId);
	int CountByClub(Guid clubId);
}

public interface IClubRepository
{
	void Add(Club club);
	List<Club> GetAll();
	Club? GetById(Guid id);
}

// Kho lưu trữ trong bộ nhớ (In-Memory)
public class InMemoryClubRepository : IClubRepository
{
	private readonly List<Club> _clubs = new();

	public void Add(Club club) => _clubs.Add(club);
	public List<Club> GetAll() => _clubs;
	public Club? GetById(Guid id) => _clubs.FirstOrDefault(c => c.Id == id);
}

public class InMemoryMemberRepository : IMemberRepository
{
	private readonly List<Member> _members = new();

	public void Add(Member member) => _members.Add(member);
	public List<Member> GetByClub(Guid clubId) => _members.Where(m => m.ClubId == clubId).ToList();
	public int CountByClub(Guid clubId) => _members.Count(m => m.ClubId == clubId);
}

// --- DESIGN PATTERN: DECORATOR ---
// Bọc lấy IClubRepository gốc để ghi log mỗi khi thêm hoặc gọi dữ liệu mà không làm thay đổi class gốc
public class LoggingClubRepository : IClubRepository
{
	private readonly IClubRepository _inner;
	private readonly IAppLogger _logger;

	public LoggingClubRepository(IClubRepository inner, IAppLogger logger)
	{
		_inner = inner;
		_logger = logger;
	}

	public void Add(Club club)
	{
		_logger.Info($"[repo] Add club '{club.Name}'");
		_inner.Add(club);
	}

	public List<Club> GetAll()
	{
		_logger.Info("[repo] Get all clubs");
		return _inner.GetAll();
	}

	public Club? GetById(Guid id)
	{
		_logger.Info($"[repo] Get club by id: {id}");
		return _inner.GetById(id);
	}
}