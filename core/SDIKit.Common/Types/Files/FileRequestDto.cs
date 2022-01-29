namespace SDIKit.Common.Types.Files
{
    public class FileRequestDto : DtoBase
    {
        public string Key { get; set; }
        public string MimeType { get; set; }
        public string FileExtension { get; set; }
        public string FileName { get; set; }
        public long FileSize { get; set; }
        public int? FileHeight { get; set; }
        public int? FileWeight { get; set; }
        public byte[] FileBytes { get; set; }
    }
}