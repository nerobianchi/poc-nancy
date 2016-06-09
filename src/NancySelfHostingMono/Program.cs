using Mono.Unix;
using Mono.Unix.Native;

namespace NancySelfHostingMono
{
	using Nancy.Hosting.Self;
	using System;

	class Program
	{
		static void Main(string[] args)
		{
			var uri = "http://localhost:8888";
			Console.WriteLine("Starting Nancy on " + uri);

			// initialize an instance of NancyHost
			var host = new NancyHost(new Uri(uri));
			host.Start();  // start hosting

#if __MonoCS__
      Console.WriteLine ("Compiled with the Mono compiler");
#else
			Console.WriteLine("Compiled with something else");
#endif
			int p = (int)Environment.OSVersion.Platform;
			if ((p == 4) || (p == 6) || (p == 128))
			{
				Console.WriteLine("Running on Unix");

				// check if we're running on mono
				if (Type.GetType("Mono.Runtime") != null)
				{
					// on mono, processes will usually run as daemons - this allows you to listen
					// for termination signals (ctrl+c, shutdown, etc) and finalize correctly
					UnixSignal.WaitAny(new[] {
						  new UnixSignal(Signum.SIGINT),
						  new UnixSignal(Signum.SIGTERM),
						  new UnixSignal(Signum.SIGQUIT),
						  new UnixSignal(Signum.SIGHUP)
					 });
				}
				else
				{
					Console.ReadLine();
				}
			}
			else {
				Console.WriteLine("NOT running on Unix");
			}

			//// check if we're running on mono
			//if (Type.GetType("Mono.Runtime") != null)
			//{
			//	// on mono, processes will usually run as daemons - this allows you to listen
			//	// for termination signals (ctrl+c, shutdown, etc) and finalize correctly
			//	UnixSignal.WaitAny(new[] {
			//			  new UnixSignal(Signum.SIGINT),
			//			  new UnixSignal(Signum.SIGTERM),
			//			  new UnixSignal(Signum.SIGQUIT),
			//			  new UnixSignal(Signum.SIGHUP)
			//		 });
			//}
			//else
			//{
			//	Console.ReadLine();
			//}

			Console.WriteLine("Stopping Nancy");
			host.Stop();  // stop hosting
		}
	}
}