using Restaurant.Application.Services;
using Restaurant.Application.ViewModels.StoreDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.Interfaces.Store
{
    public interface IStoreService
    {
        Task<ServiceResponse<IEnumerable<StoreDTO>>> GetAllStoreAsync(string name);
        Task<ServiceResponse<StoreDTO>> CreateStoreAsync(AddStoreDTO AddStoreDTO);
        Task<ServiceResponse<StoreDTO>> UpdateStoreAsync(int id, AddStoreDTO StoreDTO);
        Task<ServiceResponse<bool>> DeleteStoreAsync(int id);
    }
}
