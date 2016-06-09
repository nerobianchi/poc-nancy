using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;

namespace NancySelfHosting
{
	using Nancy;

	public class IndexModule : NancyModule
	{
		public IndexModule(IProductService iProductService)
		{
			Get["/"] = parameters =>
			{
				return iProductService.ListActiveProducts();
			};
		}
	}

	public interface IProductService
	{
		IList<Product> ListActiveProducts();
	}

	class ProductService : IProductService
	{
		public IList<Product> ListActiveProducts()
		{
			return new List<Product>() { new Product() { Name = "Product1" }, new Product() { Name = "Product 2" } };
		}
	}

	public class Product
	{
		public string Name { get; set; }
	}
}