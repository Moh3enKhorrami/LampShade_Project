using System;
namespace _0_Framework.Application
{
	public class OperationResult
	{
		public bool IsSuccedded { get; set; }
		public string Message { get; set; }

		public OperationResult()
		{
			IsSuccedded = false;
		}

		public OperationResult Succedded(string message = "mission accomplished.")
		{
			IsSuccedded = true;
			this.Message = message;
			return this;
		}

		public OperationResult Failed(string message)
		{
			IsSuccedded = false;
			this.Message = message;
			return this;
		}
	}
}

