using System;
namespace ParkAndRidePrague.Core
{
	public class ApiResult<T> where T : class 
	{
		public bool Error { get; set; }
		public T Result { get; set; }
	}
}
