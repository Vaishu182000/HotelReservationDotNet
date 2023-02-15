using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;

namespace HotelBooking.S3;

public class UploadToS3 : IUploadToS3
{
    public async Task<string> Upload(IFormFile image, string filname)
    {
        var awsAccessKey = "";
        var awsSecretKey = "";
        var awsSessionToken =
            "";
        var client = new AmazonS3Client(awsAccessKey, awsSecretKey, awsSessionToken);
        using (var ms = new MemoryStream())
        {
            await image.CopyToAsync(ms);
            ms.Seek(0, SeekOrigin.Begin);
                
            var putRequest = new PutObjectRequest
            {
                BucketName = "dotnet-training-hotelreservation",
                Key = filname,
                InputStream = ms,
                ContentType = "application/jpeg"
            };
            PutObjectResponse response = client.PutObjectAsync(putRequest).Result;
        }

        return (string)"https://dotnet-training-hotelreservation.s3.amazonaws.com/" + filname;
    }
}