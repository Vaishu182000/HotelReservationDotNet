using Microsoft.AspNetCore.Http;

namespace HotelBooking.S3;

public interface IUploadToS3
{
    public Task<string> Upload(IFormFile image, string filname);
}