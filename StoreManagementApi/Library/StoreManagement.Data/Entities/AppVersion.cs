using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Data.Entities
{
	[Table("AppVersion")]
	public class AppVersion : BaseIdLogEntity
	{
		[MaxLength(256)]
		public string OperatingSystem { get; set; }
		public string MinVersion { get; set; }
	}
}
