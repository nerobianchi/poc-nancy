namespace NancySelfHostingMono
{
	using Nancy;

	public class IndexModule : NancyModule
	{
		public IndexModule()
		{
			Get["/"] = parameters => "Hello World!";
		}
	}
}