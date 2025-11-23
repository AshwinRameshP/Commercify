namespace Commercify.Core.Features.Products.Import;

public class UploadedFile
{
    public string FileName { get; set; }
    public string ContentType { get; set; }
    public byte[] FileData { get; set; }
    
   public UploadedFile(string fileName, string contentType, byte[] fileData)
    {
        FileName = fileName;
        ContentType = contentType;
        FileData = fileData;
    }
}
