using System;
namespace ParkAndRidePrague.Core.Interfaces
{
	public interface IApiResult<T>
	{
		bool Error { get; set; }
		T Result { get; set; }
		DateTime UpdatedAt { get; set; }
	}
}
