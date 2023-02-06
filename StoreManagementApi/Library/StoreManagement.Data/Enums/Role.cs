using System.ComponentModel;

namespace StoreManagement.Data.Enums
{
	public enum Role
	{
		SUP = 1,
		[Description("Admin")]
		Admin = 2,
		[Description("Manager")]
		Manager = 3,
		[Description("Viewer")]
		Viewer = 4
	}
}
