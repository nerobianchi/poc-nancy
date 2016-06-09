using Nancy;
using System.Collections.Generic;

namespace NancySelfHosting
{
	public class DoctorModule : NancyModule
	{
		private readonly IDoctorService doctorService;

		public DoctorModule(IDoctorService doctorService)
		{
			this.doctorService = doctorService;
			this.Get["/doctors/"] = parameters =>
											{
												return this.doctorService.ListActiveDoctors();
											};
		}
	}

	public interface IDoctorService
	{
		List<Doctor> ListActiveDoctors();
	}

	class DoctorService : IDoctorService
	{
		public List<Doctor> ListActiveDoctors()
		{
			return new List<Doctor>() { new Doctor { Name = "mjones" } };
		}
	}

	public class Doctor
	{
		public string Name { get; set; }
	}
}