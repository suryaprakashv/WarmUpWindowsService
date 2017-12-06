#region

using System.Collections.Generic;

#endregion

namespace WarmUpWindowsService
{
    public class DocumentItemListDto
    {
        public int? ParentFolderId { get; set; }

        public int ItemCount { get; set; }

        public string Name { get; set; }

        public string ParentFolderTitle { get; set; }

        public ICollection<DocumentItemDto> DocumentItems { get; set; }

        public string RelativeUrl { get; set; }

        public bool IsFlexibleFolder { get; set; }
    }
}