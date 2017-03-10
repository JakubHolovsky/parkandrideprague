using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParkAndRidePrague.Core.Interfaces
{
    public interface IParkingApi
    {
        Task<ApiResult<List<IParking>>> GetParkings();
    }
}
