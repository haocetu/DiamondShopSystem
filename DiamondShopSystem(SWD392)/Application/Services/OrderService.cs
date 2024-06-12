using Application.Commons;
using Application.Interfaces;
using Application.ViewModels.AccountDTOs;
using Application.ViewModels.OrderDTOs;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
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
        public async Task<ServiceResponse<bool>> CancelOrderAsync(int id)
        {
            var response = new ServiceResponse<bool>();

            var exist = await _unitOfWork.OrderRepository.GetByIdAsync(id);
            if (exist == null || exist.IsDeleted == true)
            {
                response.Success = false;
                response.Message = "Order is not existed";
                return response;
            }

            try
            {
                _unitOfWork.OrderRepository.SoftRemove(exist);
                exist.IsDeleted = true;
                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
                if (isSuccess)
                {
                    response.Success = true;
                    response.Message = "Cancel order successfully.";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Error while cancelling order";
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

        public async Task<ServiceResponse<OrderDTO>> CreateOrderAsync(CreateOrderDTO createOrderDTO)
        {
            var response = new ServiceResponse<OrderDTO>();

            try
            {
                var order = _mapper.Map<Order>(createOrderDTO);

                order.IsDeleted = false;

                await _unitOfWork.OrderRepository.AddAsync(order);

                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
                if (isSuccess)
                {
                    var orderDTO = _mapper.Map<OrderDTO>(order);
                    response.Data = orderDTO; 
                    response.Success = true;
                    response.Message = "Order created successfully.";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Error saving the order.";
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

        public async Task<ServiceResponse<OrderDTO>> GetOrderByIdAsync(int orderId)
        {
            var response = new ServiceResponse<OrderDTO>();

            var exist = await _unitOfWork.OrderRepository.GetByIdAsync(orderId);
            if (exist == null || exist.IsDeleted == true)
            {
                response.Success = false;
                response.Message = "Order is not existed";
            }
            else
            {
                response.Success = true;
                response.Message = "Order found";
                response.Data = _mapper.Map<OrderDTO>(exist);
            }

            return response;
        }

        public Task<ServiceResponse<IEnumerable<OrderDTO>>> GetOrderByUserIDAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<IEnumerable<OrderDTO>>> GetOrdersAsync()
        {
            var _response = new ServiceResponse<IEnumerable<OrderDTO>>();
            try
            {
                var orders = await _unitOfWork.OrderRepository.GetAllAsync();

                var orderDTOs = new List<OrderDTO>();

                foreach (var order in orders)
                {
                    if (order.IsDeleted == false)
                    {
                        orderDTOs.Add(_mapper.Map<OrderDTO>(order));
                    }
                }

                if (orderDTOs.Count != 0)
                {
                    _response.Success = true;
                    _response.Message = "Orders retrieved successfully";
                    _response.Data = orderDTOs;
                }
                else
                {
                    _response.Success = true;
                    _response.Message = "Not have order";
                }

            }
            catch (Exception ex)
            {
                _response.Success = false;
                _response.Message = "Error";
                _response.ErrorMessages = new List<string> { Convert.ToString(ex.Message) };
            }

            return _response;
        }
    }
}
