using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParkAndRidePrague.Core.Interfaces
{
    public interface IParkingApi
    {
        Task<List<IParking>> GetParkings();
    }
}
