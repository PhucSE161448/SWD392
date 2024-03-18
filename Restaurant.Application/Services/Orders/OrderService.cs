using AutoMapper;
using Restaurant.Application.Interfaces;
using Restaurant.Application.Interfaces.Orders;
using Restaurant.Application.ViewModels.OrderDTO;
using Restaurant.Application.ViewModels.ProductDTO;
using Restaurant.Application.ViewModels.ProductTemplateDTO;
using Restaurant.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.Services.Orders
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<List<OrderDto>>> GetAllOrderByUser()
        {
            var _response = new ServiceResponse<List<OrderDto>>();
            try
            {
                var order = await _unitOfWork.OrderRepository.GetAllOrderByUser();
                List<OrderDto> list = new List<OrderDto>();
                if(order != null)
                {
                    foreach (var item in order)
                    {
                        list.Add(_mapper.Map<OrderDto>(item));
                    }
                }

                if (list.Any())
                {
                    _response.Success = true;
                    _response.Message = "Order retrieved successfully";
                    _response.Data = list;
                }
                else
                {
                    _response.Success = true;
                    _response.Message = "Order not found";
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
        public async Task<ServiceResponse<OrderDto>> GetOrderById(int id)
        {
            var _response = new ServiceResponse<OrderDto>();
            try
            {
                var order = await _unitOfWork.OrderRepository.GetOrderById(id);

                if (order != null)
                {
                    _response.Success = true;
                    _response.Message = "Order retrieved successfully";
                    _response.Data = _mapper.Map<OrderDto>(order);
                }
                else
                {
                    _response.Success = true;
                    _response.Message = "Order not found";
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
        public async Task<ServiceResponse<OrderDto>> CreateOrder(OrderCreateDTO orderCreate)
        {
                var response = new ServiceResponse<OrderDto>();
                try
                {
                    var (isSuccess, Order) = await _unitOfWork.OrderRepository.CreateOrderAsync(orderCreate);

                    if (isSuccess)
                    {
                        var o = await _unitOfWork.OrderRepository.GetOrderById(Order.Id);
                        var order = _mapper.Map<OrderDto>(o);
                        response.Data = order;
                        response.Success = true;
                        response.Message = "Order created successfully";
                    }
                    else
                    {
                        response.Success = false;
                        response.Message = "Create Order failed";
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

        public async Task<ServiceResponse<OrderDto>> UpdateOrderStatus(int id, string status)
        {
            var response = new ServiceResponse<OrderDto>();
            try
            {
                var (isSuccess, Order) = await _unitOfWork.OrderRepository.UpdateOrderStatusAsync(id,status);

                if (isSuccess)
                {
                    var o = await _unitOfWork.OrderRepository.GetOrderById(Order.Id);
                    var order = _mapper.Map<OrderDto>(o);
                    response.Data = order;
                    response.Success = true;
                    response.Message = "Order updated successfully";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Update Order failed";
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

        public async Task<ServiceResponse<OrderDetailDTO>> GetOrderByUser(int OrderId)
        {
            var response = new ServiceResponse<OrderDetailDTO>();
            try
            {
                var detail = await _unitOfWork.OrderRepository.GetOrderDetail(OrderId);

                if (detail != null)
                {
                    response.Data = detail;
                    response.Success = true;
                    response.Message = "OrderDetail Retrieved successfully";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Retrieved OrderDetail failed";
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
    }
}
