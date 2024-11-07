using HairHarmonySalon.Controllers;
using HairHarmonySalon.Models;
using Harmony.Repositories.Interfaces;
using Harmony.Services.Interfaces;
using HarmonySalon.Reponsitories.Entities;

namespace HairHarmonySalon.Models
{
	public class ErrorViewModel
	{
		public string? RequestId { get; set; }

		public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
	}
}
