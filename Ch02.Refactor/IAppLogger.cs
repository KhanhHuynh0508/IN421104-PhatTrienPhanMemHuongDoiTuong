namespace EduHub.Ch02;

public interface IAppLogger
{
	void Info(string message);
}

public class ConsoleLogger : IAppLogger
{
	public void Info(string message)
	{
		Console.WriteLine($"[LOG] {message}");
	}
}