using Application.Commons;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
	public class ProductService : IProductService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly IImageService _imageService;

		public ProductService(IUnitOfWork unitOfWork, IMapper mapper, IImageService imageService)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_imageService = imageService;
		}

		public async Task<ServiceResponse<IEnumerable<Product>>> GetProductsAsync()
		{
			var response = new ServiceResponse<IEnumerable<Product>>();
			try
			{
				var products = await _unitOfWork.ProductRepository.GetAllAsync();

				var productsDTO = new List<Product>();

				foreach (var product in products)
				{
					if (product.IsDeleted == false)
					{
						productsDTO.Add(_mapper.Map<Product>(product));
					}
				}

				if (productsDTO.Count != 0)
				{
					response.Success = true;
					response.Message = "Products retrieved successfully!";
					response.Data = productsDTO;
				}
				else
				{
					response.Success = true;
					response.Message = "Not have product yet!";
				}
			}
			catch (Exception ex)
			{
				response.Success = false;
				response.Message = "Error";
				response.ErrorMessages = new List<string> { Convert.ToString(ex.Message) };
			}
			return response;
		}
	}
}