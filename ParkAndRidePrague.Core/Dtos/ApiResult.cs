using System;
using ParkAndRidePrague.Core.Interfaces;

namespace ParkAndRidePrague.Core
{
	public class ApiResult<T> : IApiResult<T> where T : class 
	{
		public bool Error { get; set; }
		public T Result { get; set; }
		public DateTime UpdatedAt { get; set; }
	}
}
