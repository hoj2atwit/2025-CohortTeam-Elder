namespace SmartGym.Services
{
	public class DatabaseConfiguration : IDatabaseConfiguration
	{
		public const string ConnectionStrings = "ConnectionStrings";
		public string DBConnectionString { get; set; }
	}
}