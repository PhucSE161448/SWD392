using AutoMapper;
using Restaurant.Application.Interfaces;
using Restaurant.Application.Interfaces.Store;
using Restaurant.Application.ViewModels.StoreDTO;
using Restaurant.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.Services.Stores
{
    public class StoreService : IStoreService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        
        public StoreService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<StoreDTO>> CreateStoreAsync(AddStoreDTO CreateStoreDTO)
        {
            var response = new ServiceResponse<StoreDTO>();
            try
            {
                var s = await _unitOfWork.StoreRepository.GetAllAsync();
                if(s.Any(x => x.Name == CreateStoreDTO.Name))
                {
                    response.Success = false;
                    response.Message = "Store already exists";
                    return response;
                }
                else
                {
                    var store = _mapper.Map<Store>(CreateStoreDTO);
                    await _unitOfWork.StoreRepository.AddAsync(store);
                    var isSuccessful = await _unitOfWork.SaveChangeAsync() > 0;
                    if (isSuccessful)
                    {
                        var StoreDTO = _mapper.Map<StoreDTO>(store);
                        response.Data = StoreDTO;
                        response.Message = "Store created successfully";
                        response.Success = true;
                    }
                    else
                    {
                        response.Success = false;
                        response.Message = "Store creation failed";
                    }
                }
            }catch(DbException ex)
            {
                response.Success = false;
                response.Message = "Database error occured.";
                response.ErrorMessages = new List<string> { ex.Message };
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error";
                response.ErrorMessages = new List<string> { ex.Message };
            }
            return response;
        }

        public async Task<ServiceResponse<bool>> DeleteStoreAsync(int id)
        {
            var response = new ServiceResponse<bool>();
            var exits = await _unitOfWork.StoreRepository.GetByIdAsync(id);
            if (exits == null)
            {
                response.Success = false;
                response.Message = "Store not found";
                return response;
            }
            try
            {
                _unitOfWork.StoreRepository.SoftRemove(exits);
                var isSuccessful = await _unitOfWork.SaveChangeAsync() > 0;
                if(isSuccessful)
                {
                    response.Message = "Store deleted successfully";
                    response.Success = true;
                }
                else
                {
                    response.Success = false;
                    response.Message = "Store deletion failed";
                }

            }catch(DbException ex)
            {
                response.Success = false;
                response.Message = "Database error occured.";
                response.ErrorMessages = new List<string> { ex.Message };
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error";
                response.ErrorMessages = new List<string> { ex.Message };
            }
            return response;
        }

        public async Task<ServiceResponse<IEnumerable<StoreDTO>>> GetAllStoreAsync(string name)
        {
            var response = new ServiceResponse<IEnumerable<StoreDTO>>();
            try
            {
                List<Store> Storess;
                if(name != null)
                {
                    Storess = await _unitOfWork.StoreRepository.GetAllAsync(x => x.Name.Contains(name));
                }
                else
                {
                    Storess = await _unitOfWork.StoreRepository.GetAllAsync();
                }
                var StoreDTOs = new List<StoreDTO>();
                foreach (var store in Storess)
                {
                    if ((bool)store.IsDeleted)
                    {
                        StoreDTOs.Add(_mapper.Map<StoreDTO>(store));
                    }
                }
                if(StoreDTOs.Count != 0)
                {
                    response.Data = StoreDTOs;
                    response.Message = "Stores retrieved successfully";
                    response.Success = true;
                }
                else
                {
                    response.Success = false;
                    response.Message = "No Store found";
                }
                
            }
            catch (DbException ex)
            {
                response.Success = false;
                response.Message = "Database error occured.";
                response.ErrorMessages = new List<string> { ex.Message };
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error";
                response.ErrorMessages = new List<string> { ex.Message };
            }
            return response;
        }

        public async Task<ServiceResponse<StoreDTO>> UpdateStoreAsync(int id, AddStoreDTO StoreDTO)
        {
            var response = new ServiceResponse<StoreDTO>();
            var exits = await _unitOfWork.StoreRepository.GetByIdAsync(id);
            if (exits == null)
            {
                response.Success = false;
                response.Message = "Store not found";
                return response;
            }
            try
            {
                var store = _mapper.Map(StoreDTO, exits);
                _unitOfWork.StoreRepository.Update(store);
                var isSuccessful = await _unitOfWork.SaveChangeAsync() > 0;
                if (isSuccessful)
                {
                    response.Data = _mapper.Map<StoreDTO>(store);
                    response.Message = "Store updated successfully";
                    response.Success = true;
                }
                else
                {
                    response.Success = false;
                    response.Message = "Store update failed";
                }
            }
            catch (DbException ex)
            {
                response.Success = false;
                response.Message = "Database error occured.";
                response.ErrorMessages = new List<string> { ex.Message };
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error";
                response.ErrorMessages = new List<string> { ex.Message };
            }
            return response;
        }
    }
}
