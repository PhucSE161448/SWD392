﻿using Restaurant.Application.Services;
using Restaurant.Application.ViewModels.NewsDTO;
using Restaurant.Application.ViewModels.NewsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.Interfaces.News
{
    public interface INewsService
    {
        Task<ServiceResponse<IEnumerable<NewsDTO>>> GetAllNewsAsync(string description);
        Task<ServiceResponse<NewsDTO>> CreateNewsAsync(AddNewsDTO AddNewsDTO);
        Task<ServiceResponse<NewsDTO>> UpdateNewsAsync(int id, AddNewsDTO NewsDTO);
        Task<ServiceResponse<bool>> DeleteNewsAsync(int id);
        Task<ServiceResponse<IEnumerable<NewsDTO>>> SearchNewsByNameAsync(string name);
        Task<ServiceResponse<IEnumerable<NewsDTO>>> GetSortedNewsAsync();
    }
}