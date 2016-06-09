using Nancy.Bootstrapper;
using Nancy.TinyIoc;

namespace NancySelfHosting
{
	using Nancy;

	public class Bootstrapper : DefaultNancyBootstrapper
	{
		// The bootstrapper enables you to reconfigure the composition of the framework,
		// by overriding the various methods and properties.
		// For more information https://github.com/NancyFx/Nancy/wiki/Bootstrapper
		protected override void ConfigureApplicationContainer(TinyIoCContainer container)
		{
			//container.Register<IProductService, ProductService>().AsMultiInstance();
			base.ConfigureApplicationContainer(container);
		}

		protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
		{
			base.ApplicationStartup(container, pipelines);
		}
	}
}