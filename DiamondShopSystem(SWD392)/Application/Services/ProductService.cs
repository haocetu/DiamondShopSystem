using Application.Commons;
using Application.Interfaces;
using Application.ViewModels.ProductDTOs;
using AutoMapper;
using Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data.Common;
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

		public async Task<ServiceResponse<Product>> GetProductByIdAsync(int id)
		{
			var response = new ServiceResponse<Product>();
			var exist = await _unitOfWork.ProductRepository.GetByIdAsync(id);
			if (exist == null)
			{
				response.Success = false;
				response.Message = "Product is not existed!";
			}
			else if (exist.IsDeleted == true)
			{
				response.Success = false;
				response.Message = "Product have been deleted from the system.";

			}
			else
			{
				response.Success = true;
				response.Message = "Product found.";
				response.Data = exist;
			}
			return response;
		}

		public async Task<ServiceResponse<IEnumerable<Product>>> SearchProductByNameAsync(string name)
		{
			var response = new ServiceResponse<IEnumerable<Product>>();
			try
			{
				var products = await _unitOfWork.ProductRepository.SearchProduct(name);
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
					response.Success = false;
					response.Message = "Not have product yet!";
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

		public async Task<ServiceResponse<Product>> CreateProductAsync(CreateProductDTO createProduct)
		{
			var response = new ServiceResponse<Product>();
			try
			{
				var product = _mapper.Map<Product>(createProduct);
				product.IsDeleted = false;
				await _unitOfWork.ProductRepository.AddAsync(product);


				var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
				if (!createProduct.ProductImages.IsNullOrEmpty())
				{
					await _imageService.UploadProductImages(createProduct.ProductImages, product.Id);
				}
				if (isSuccess)
				{
					var productDTO = _mapper.Map<Product>(product);
					response.Data = productDTO;
					response.Success = true;
					response.Message = "Product created successfully!";
				}
				else
				{
					response.Success = false;
					response.Message = "Error saving the product!";
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

		public async Task<ServiceResponse<bool>> DeleteProductAsync(int id)
		{
			var response = new ServiceResponse<bool>();

			var exist = await _unitOfWork.ProductRepository.GetByIdAsync(id);
			if (exist == null)
			{
				response.Success = false;
				response.Message = "Product is not existed!";
				return response;
			}
			else if (exist.IsDeleted == true)
			{
				response.Success = false;
				response.Message = "Product have been deleted from the system!";
				return response;
			}
			try
			{
				_unitOfWork.ProductRepository.SoftRemove(exist);
				exist.IsDeleted = true;
				var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
				if (isSuccess)
				{
					response.Success = true;
					response.Message = "Product deleted successfully!";
				}
				else
				{
					response.Success = false;
					response.Message = "Error deleting product!";
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