
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ImageService : IImageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFirebaseStorageService _firebaseStorageService;

        public ImageService(IUnitOfWork unitOfWork, IFirebaseStorageService firebaseStorageService)
        {
            _unitOfWork = unitOfWork;
            _firebaseStorageService = firebaseStorageService;
        }

        public async Task UploadDiamondImages(List<IFormFile> imageFiles, int diamondId)
        {
            if (imageFiles.IsNullOrEmpty())
            {
                throw new Exception("No Image File found");
            }
            var folderPath = $"diamond/{diamondId}";
            var imageUrls = await _firebaseStorageService.UploadImagesAsync(imageFiles, folderPath);
            var images = imageUrls.Select(url => new Image { DiamondId = diamondId, UrlPath = url }).ToList();
            await _unitOfWork.ImageRepository.AddRangeAsync(images);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task UploadProductImages(List<IFormFile> imageFiles, int productId)
        {
            if (imageFiles.IsNullOrEmpty())
            {
                throw new Exception("No Image File found");
            }
            var folderPath = $"product/{productId}";
            var imageUrls = await _firebaseStorageService.UploadImagesAsync(imageFiles, folderPath);
            var images = imageUrls.Select(url => new Image { ProductId = productId, UrlPath = url }).ToList();
            await _unitOfWork.ImageRepository.AddRangeAsync(images);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task DeleteImages(IEnumerable<Image> images)
        {
            var imageUrls = images.Select(p => p.UrlPath);
            await _firebaseStorageService.DeleteImagesAsync(imageUrls.ToList());
            await _unitOfWork.ImageRepository.DeleteRangeAsync(images);
            await _unitOfWork.SaveChangeAsync();
        }
    }
}
