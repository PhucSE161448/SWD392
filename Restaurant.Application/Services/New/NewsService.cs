using AutoMapper;
using Restaurant.Application.Interfaces;
using Restaurant.Application.ViewModels.NewsDTO;
using Restaurant.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant.Application.Interfaces.News;

namespace Restaurant.Application.Services.Newss
{
    public class NewsService : INewsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public NewsService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<NewsDTO>> CreateNewsAsync(AddNewsDTO CreatedNewsDTO, string image)
        {
            var response = new ServiceResponse<NewsDTO>();
            try
            {
                var n = await _unitOfWork.NewsRepository.GetAllAsync();
                
                if(n.Any(x => x.Title == CreatedNewsDTO.Title))
                {
                    response.Success = false;
                    response.Message = "News already exists";
                    return response;
                }
                else
                {
                    var News = _mapper.Map<News>(CreatedNewsDTO);
                    News.Image = image;
                    await _unitOfWork.NewsRepository.AddAsync(News);
                    var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
                    if (isSuccess)
                    {
                        var NewsDTO = _mapper.Map<NewsDTO>(News);
                        response.Data = NewsDTO;
                        response.Success = true;
                        response.Message = "News created successfully";
                    }
                    else
                    {
                        response.Success = false;
                        response.Message = "Create News failed";
                    }
                }
                

            }
            catch (DbException ex)
            {
                response.Success = false;
                response.Message = "Database error occurred.";
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

        public async Task<ServiceResponse<bool>> DeleteNewsAsync(int id)
        {
            var response = new ServiceResponse<bool>();
            var exist = await _unitOfWork.NewsRepository.GetByIdAsync(id);
            if (exist == null)
            {
                response.Success = false;
                response.Message = "News not found";
                return response;
            }
            try
            {
                _unitOfWork.NewsRepository.SoftRemove(exist);
                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
                //Get the ImageLocalPath
                Uri uri = new Uri(exist.Image);
                string lastSegment = uri.Segments.LastOrDefault();
                string filePath = @"wwwroot\ProductImage\" + lastSegment;
                var directoryLocation = Path.Combine(Directory.GetCurrentDirectory(), filePath);
                FileInfo file = new FileInfo(directoryLocation);
                if (file.Exists)
                {
                    file.Delete();
                }

                if (isSuccess)
                {
                    response.Success = true;
                    response.Message = "News deleted successfully";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Delete News failed";
                }
            }
            catch (DbException ex)
            {
                response.Success = false;
                response.Message = "Database error occurred.";
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

        public async Task<ServiceResponse<IEnumerable<NewsDTO>>> GetAllNewsAsync(int? id, string title, string size = null)
        {
            var _response = new ServiceResponse<IEnumerable<NewsDTO>>();
            try
            {
                List<News> Newss;
                if(title != null)
                {
                     Newss = await _unitOfWork.NewsRepository.GetAllAsync(x => x.Title == title);
                }else if (id != 0 && id != null)
                {
                    Newss = await _unitOfWork.NewsRepository.GetAllAsync(x => x.Id == id);
                }else if (size != null)
                {
                    Newss = await _unitOfWork.NewsRepository.GetListNewBySize(size);
                }
                else
                { 
                    Newss = await _unitOfWork.NewsRepository.GetAllAsync();
                }
                
                var NewsDTOs = new List<NewsDTO>();
                foreach (var pro in Newss)
                {
                    if ((bool)!pro.IsDeleted)
                    {
                        NewsDTOs.Add(_mapper.Map<NewsDTO>(pro));
                    }
                }
                if (NewsDTOs.Count != 0)
                {
                    _response.Success = true;
                    _response.Message = "News retrieved successfully";
                    _response.Data = NewsDTOs;
                }
                else
                {
                    _response.Success = true;
                    _response.Message = "News not found";
                }
            }
            catch (DbException ex)
            {
                _response.Success = false;
                _response.Message = "Database error occurred.";
                _response.ErrorMessages = new List<string> { ex.Message };
            }
            catch (Exception ex)
            {
                _response.Success = false;
                _response.Data = null;
                _response.Message = "Error";
                _response.ErrorMessages = new List<string> { Convert.ToString(ex.Message) };
            }
            return _response;
        }
        public async Task<ServiceResponse<NewsDTO>> GetNewsAsync(int id)
        {
            var _response = new ServiceResponse<NewsDTO>();
            try
            {
                 var Newss = await _unitOfWork.NewsRepository.GetAsync(x => x.Id == id);
                if (Newss != null)
                {
                    _response.Success = true;
                    _response.Message = "News retrieved successfully";
                    _response.Data = _mapper.Map<NewsDTO>(Newss);
                }
                else
                {
                    _response.Success = true;
                    _response.Message = "News not found";
                }
            }
            catch (DbException ex)
            {
                _response.Success = false;
                _response.Message = "Database error occurred.";
                _response.ErrorMessages = new List<string> { ex.Message };
            }
            catch (Exception ex)
            {
                _response.Success = false;
                _response.Data = null;
                _response.Message = "Error";
                _response.ErrorMessages = new List<string> { Convert.ToString(ex.Message) };
            }
            return _response;
        }
        public async Task<ServiceResponse<IEnumerable<NewsDTO>>> GetSortedNewsAsync()
        {
            var response = new ServiceResponse<IEnumerable<NewsDTO>>();
            try
            {
                var Newss = await _unitOfWork.NewsRepository.GetAllAsync();
                var NewsDTOs = new List<NewsDTO>();
                foreach (var pro in Newss)
                {
                    if ((bool)!pro.IsDeleted)
                    {
                        NewsDTOs.Add(_mapper.Map<NewsDTO>(pro));
                    }
                }
                if (NewsDTOs.Count != 0)
                {
                    response.Success = true;
                    response.Message = "News retrieved successfully";
                    response.Data = NewsDTOs;
                }
                else
                {
                    response.Success = true;
                    response.Message = "News not found";
                }
            }
            catch (DbException ex)
            {
                response.Success = false;
                response.Message = "Database error occurred.";
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

        public async Task<ServiceResponse<IEnumerable<NewsDTO>>> SearchNewsByNameAsync(string name)
        {
            var response = new ServiceResponse<IEnumerable<NewsDTO>>();
            try
            {
                var Newss = await _unitOfWork.NewsRepository.GetAllAsync();
                var NewsDTOs = new List<NewsDTO>();
                foreach (var pro in Newss)
                {
                    if ((bool)!pro.IsDeleted)
                    {
                        NewsDTOs.Add(_mapper.Map<NewsDTO>(pro));
                    }
                }
                if (NewsDTOs.Count != 0)
                {
                    response.Success = true;
                    response.Message = "News retrieved successfully";
                    response.Data = NewsDTOs;
                }
                else
                {
                    response.Success = true;
                    response.Message = "News not found";
                }
            }
            catch (DbException ex)
            {
                response.Success = false;
                response.Message = "Database error occurred.";
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

        public async Task<ServiceResponse<NewsDTO>> UpdateNewsAsync(int id, AddNewsDTO NewsDTO, string image)
        {
            var response = new ServiceResponse<NewsDTO>();
            var exist = await _unitOfWork.NewsRepository.GetByIdAsync(id);
            if (exist == null)
            {
                response.Success = false;
                response.Message = "News not found";
                return response;
            }
            try
            {
                var News = _mapper.Map(NewsDTO, exist);
                if (string.IsNullOrEmpty(image))
                {
                    News.Image = exist.Image;
                }
                else
                {
                    News.Image = image;
                }
                _unitOfWork.NewsRepository.Update(News);
                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
                if (isSuccess)
                {
                    response.Success = true;
                    response.Message = "News updated successfully";
                    response.Data = _mapper.Map<NewsDTO>(News);
                }
                else
                {
                    response.Success = false;
                    response.Message = "Update News failed";
                }
            }
            catch (DbException ex)
            {
                response.Success = false;
                response.Message = "Database error occurred.";
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
        public async Task<ServiceResponse<bool>> UpdateIsDelete(int id, bool? isDeleted)
        {
            var response = new ServiceResponse<bool>();

            var exist = await _unitOfWork.NewsRepository.GetByIdAsync(id);
            if (exist == null)
            {
                response.Success = false;
                response.Message = "Account is not existed";
                return response;
            }

            try
            {
                if (isDeleted.HasValue)
                {
                    exist.IsDeleted = isDeleted;
                }
                _unitOfWork.NewsRepository.Update(exist);

                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
                if (isSuccess)
                {
                    response.Success = true;
                    response.Message = "Account update successfully.";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Error update the account.";
                }
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
