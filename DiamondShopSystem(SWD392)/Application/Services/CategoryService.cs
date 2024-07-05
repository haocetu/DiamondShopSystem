﻿using Application.Commons;
using Application.Interfaces;
using Application.ViewModels.CategoryDTOs;
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
	public class CategoryService : ICategoryService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<ServiceResponse<IEnumerable<CategoryDTO>>> GetCategoriesAsync()
		{
			var response = new ServiceResponse<IEnumerable<CategoryDTO>>();
			try
			{
				var categories = await _unitOfWork.CategoryRepository.GetAllAsync();
				var categoryDTOs = new List<CategoryDTO>();
				foreach (var cat in categories)
				{
					if (cat.IsDeleted == false)
					{
						categoryDTOs.Add(_mapper.Map<CategoryDTO>(cat));
					}
				}
				if (categoryDTOs.Count > 0)
				{
					response.Data = categoryDTOs;
					response.Success = true;
					response.Message = "Categories retrieved successfully!";
				}
				else
				{
					response.Success = false;
					response.Message = "No categories were found!";
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
				response.Message = "Error.";
				response.ErrorMessages = new List<string> { ex.Message };
			}
			return response;
		}
		public async Task<ServiceResponse<CategoryDTO>> GetCategoryByIdAsync(int id)
		{
			var response = new ServiceResponse<CategoryDTO>();
			var exist = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
			var category = _mapper.Map<CategoryDTO>(exist);
			if (category == null)
			{
				response.Success = false;
				response.Message = "Cannot found category.";
			}
			else if (exist.IsDeleted == true)
			{
				response.Success = false;
				response.Message = "Category was deleted from the system.";
			}
			else
			{
				response.Success = true;
				response.Message = "Category was retrieved successfully!";
				response.Data = category;
			}
			return response;
		}
		public async Task<ServiceResponse<CategoryDTO>> CreateCategoryAsync(CreateCategoryDTO cat)
		{
			var response = new ServiceResponse<CategoryDTO>();
			try
			{
				var category = _mapper.Map<Category>(cat);
				category.IsDeleted = false;
				await _unitOfWork.CategoryRepository.AddAsync(category);

				var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
				if (isSuccess)
				{
					var categoryDTO = _mapper.Map<CategoryDTO>(category);
					response.Data = categoryDTO;
					response.Success = true;
					response.Message = "Category created successfully.";
				}
				else
				{
					response.Success = false;
					response.Message = "Error saving category.";
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
				response.Message = "Error.";
				response.ErrorMessages = new List<string> { ex.Message };
			}
			return response;
		}
	}
}