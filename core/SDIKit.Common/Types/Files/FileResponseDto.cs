using System.Collections.Generic;

namespace SDIKit.Common.Types.Files
{
    public class FileResponseDto
    {
        public string Name { get; set; }
        public string Blob { get; set; }
        public List<FileDto> Response { get; set; }
    }
}