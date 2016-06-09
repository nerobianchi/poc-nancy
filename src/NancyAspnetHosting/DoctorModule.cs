#region licence

// <copyright file="DoctorModule.cs" company="Ciceksepeti">
// </copyright>
// <summary>
// 	Project Name:	NancyAspnetHosting
// 	Created By: 	erdem.ozdemir
// 	Create Date:	14.02.2016 14:11
// 	Last Changed By:	erdem.ozdemir
// 	Last Change Date:	14.02.2016 14:37
// </summary>

#endregion licence

using Nancy;
using System;
using System.Collections.Generic;

using Nancy.ModelBinding;

namespace NancyAspnetHosting
{
	public class DoctorModule : NancyModule
	{
		private readonly IDoctorService doctorService;

		private readonly Func<Request, bool> isAvailable = request => request.Headers.UserAgent.ToLower().StartsWith("curl");

		public DoctorModule(IDoctorService doctorService)
		{
			this.doctorService = doctorService;

			this.Get["/doctors/", ctx => isAvailable(ctx.Request)] = parameters => "hello curl";

			this.Get["/doctors/"] = parameters => this.doctorService.ListActiveDoctors();
			this.Get["/doctors/{id}"] = parameters => this.doctorService.GetDoctor(parameters.id);
			this.Get["/doctors/{id}/slots"] = parameters => this.doctorService.GetDoctorSlots(parameters.id.ToString(), this.Request.Query["date"], this.Request.Query["status"]);
			this.Get["/doctors/{id}/array_input"] = parameters =>
																 {
																	 var s = parameters.id.ToString();
																	 var array = this.Request.Query["id"];

																	 var bind = this.Bind<Hede>();

																	 return this.doctorService.GetDoctorArrayInput();
																 };
			this.Post["/doctors/{id}/array_input"] = parameters =>
																 {
																	 var s = parameters.id.ToString();
																	 var array = this.Request.Query["id"];

																	 var bind = this.Bind<Hede>();

																	 return this.doctorService.GetDoctorArrayInput();
																 };

		}
	}

	public class Hede
	{
		public string ContainerType { get; set; }
		public IEnumerable<Item> Items { get; set; }

		//public string[] MyStringArray { get; set; }
	}

	public class Item
	{
		public string Name { get; set; }
		public double Amount { get; set; }
	}

	public interface IDoctorService
	{
		List<Doctor> ListActiveDoctors();

		Doctor GetDoctor(string id);

		//	OpenSlotList GetDoctorSlots(string id);

		OpenSlotList GetDoctorSlots(string id, string date, string status);

		int GetDoctorArrayInput();
	}

	internal class DoctorService : IDoctorService
	{
		public List<Doctor> ListActiveDoctors()
		{
			return new List<Doctor>()
					 {
						 new Doctor
						 {
							 Name = "mjones"
						 }
					 };
		}

		public Doctor GetDoctor(string id)
		{
			return new Doctor()
			{
				Name = id
			};
		}

		public OpenSlotList GetDoctorSlots(string id, string date, string status)
		{
			return new OpenSlotList()
					 {
						 new OpenSlot
						 {
							 Id = "1234",
							 Doctor = "mjones",
							 Start = "1400",
							 End = "1450",
							 Link = new Link
									  {
										  Rel = "/linkrels/slot/book",
										  Uri = "/slots/1234"
									  }
						 },
						 new OpenSlot
						 {
							 Id = "5678",
							 Doctor = "mjones",
							 Start = "1600",
							 End = "1650",
							 Link = new Link
									  {
										  Rel = "/linkrels/slot/book",
										  Uri = "/slots/5678"
									  }
						 }
					 };
		}

		public int GetDoctorArrayInput()
		{
			return 201;
		}
	}

	public class Link
	{
		public string Rel { get; set; }

		public string Uri { get; set; }
	}

	public class OpenSlot
	{
		public string Id { get; set; }

		public string Doctor { get; set; }

		public string Start { get; set; }

		public string End { get; set; }

		public Link Link { get; set; }
	}

	public class OpenSlotList : List<OpenSlot>
	{
	}

	public class Doctor
	{
		public string Name { get; set; }
	}
}