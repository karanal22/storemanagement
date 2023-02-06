using System.ComponentModel;

namespace StoreManagement.Common.ExceptionHandler
{
	public enum AppErrorCode
	{

		#region Bad request

		//Common validations
		[Description("Invalid Parameters")]
		InvalidParameters = 2000,

		//Common validations
		[Description("No file uploaded")]
		NoFileUploaded = 2001,

		[Description("File is empty")]
		EmptyFIle = 2002,

		[Description("Invalid file type")]
		InvalidFileType = 2003,

		[Description("{0} is required")]
		Required = 2004,

		#endregion

		#region NotFound

		[Description("StoreProdct not found for {0}")]
		NotFoundStoreProdct = 4001,



		#endregion


		#region Exception

		[Description("Internal Server Error - {0}")]
		Exception = 9001,

		[Description("MySql Error - {0}")]
		MySqlException = 9002

		#endregion

	}
}
