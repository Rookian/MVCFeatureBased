namespace MVCFeatureBased.Web.Infrastructure.MediatR
{
    public class FileResult : IRequestResult
    {
        public byte[] Data { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
    }
}