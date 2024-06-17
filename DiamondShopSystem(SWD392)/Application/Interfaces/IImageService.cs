using Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IImageService
    {
        Task UploadDiamondImages(List<IFormFile> imageFiles, int diamondId);
        Task UploadProductImages(List<IFormFile> imageFiles, int productId);

        Task DeleteImages(IEnumerable<Image> images);
    }
}
