using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Restaurant.Application.Interfaces;
using Restaurant.Application.IRepositories.Orders;
using Restaurant.Application.Utils;
using Restaurant.Application.ViewModels.OrderDTO;
using Restaurant.Application.ViewModels.ProductDTO;
using Restaurant.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Infrastructure.Repositories.Orders
{
    public class OrderRepository : IOrderRepository
    {
        private readonly MixFoodContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IClaimsService _claimsService;
        private string _currentUser;
        public OrderRepository(MixFoodContext context, IClaimsService claimsService, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
            _claimsService = claimsService;
            _currentUser = claimsService.GetCurrentUserId;
        }
        public async Task<List<Order>> GetAllOrderByUser()
        {
            var account = _dbContext.Accounts.Where(x => x.Username == _currentUser).FirstOrDefault();
            var order = _dbContext.Orders.Where(x => x.AccountId == account.Id).ToList();
            if(order.Any())
            {
                return order;
            }
            return null;
        }
        public async Task<(bool success, Order order)> CreateOrderAsync(OrderCreateDTO o)
        {
            try
            {
                var account = _dbContext.Accounts.Where(x => x.Username == _currentUser).FirstOrDefault();
                var order = _mapper.Map<Order>(o);
                order.CreatedDate = DateTime.Now;
                order.StoreId = 1;
                order.PaymentMethodId = 1;
                order.TotalPrice = o.TotalPrice;
                order.AccountId = account.Id;
                order.Status = StatusOrder.StatusInProcess;
                await _dbContext.Orders.AddAsync(order);
                await _dbContext.SaveChangesAsync();
                foreach (int id in o.ProductId)
                {
                    var orderProduct = new OrderProduct
                    {
                        ProductId = id,
                        OrderId = order.Id,
                    };
                    await _dbContext.OrderProducts.AddAsync(orderProduct);
                }
                foreach (int productId in o.ProductId)
                {
                    var product = await _dbContext.Products.FindAsync(productId);
                    if (product != null)
                    {
                        product.IsDeleted = true;
                    }
                }
                await _dbContext.SaveChangesAsync();

                return (true, order);
            }
            catch (Exception ex)
            {
                return (false, null);
            }

        }

        public async Task<Order> GetOrderById(int id)
        {
            try
            {
                var order = await _dbContext.Orders
                            .Include(o => o.PaymentMethod)
                            .Include(o => o.Account)
                            .FirstOrDefaultAsync(o => o.Id == id);
                
               
                return order;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<OrderDetailDTO> GetOrderDetail(int id)
        {
            try
            {
                var account = _dbContext.Accounts.Where(x => x.Username == _currentUser).FirstOrDefault();
                var order = await GetOrderById(id);

                var orderProducts = await GetProductsByOrderId(order.Id);

                var orderDetail = new OrderDetailDTO
                {
                    OrderId = order.Id ,
                    details = orderProducts
                };
                return orderDetail;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<List<ProductsDTO>> GetProductsByOrderId(int orderId)
        {
            var orderProducts = await _dbContext.OrderProducts
                .Where(op => op.OrderId == orderId)
                .Include(op => op.Product)
                .Select(op => op.Product)
                .ToListAsync();

            var productDTOs = orderProducts.Select(product => new ProductsDTO
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                ProductTemplateId = product.ProductTemplateId,
                Quantity = product.Quantity,
                Ingredients = product.IngredientProducts.Select(ip => ip.Ingredient.Name).ToList()
            }).ToList();

            return productDTOs;
        }


        public async Task<(bool success, Order order)> UpdateOrderStatusAsync(int id, string status, int paymentId)
        {
            try
            {
                var order = await GetOrderById(id);
                string statusMessage = status.ToLower();
                if (statusMessage.Equals("finished"))
                {
                    order.Status = StatusOrder.StatusFinish;
                }
                else if (statusMessage.Equals("cancel"))
                {
                    order.Status = StatusOrder.StatusCancelled;
                }
                order.PaymentMethodId = paymentId;
                await _dbContext.SaveChangesAsync();
                return (true, order);
            }
            catch (Exception ex)
            {
                return (false, null);
            }
        }
    }
}
