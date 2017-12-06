#region

using System;
using System.Collections.Generic;
using System.IO;

#endregion

namespace WarmUpWindowsService
{
    public class DocumentItemDto
    {
        public int ItemId { get; set; }

        public string Title { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsDocument { get; set; }

        public string ParentDirectoryUrl { get; set; } //map to FileDto Fileleaf

        public string RelativeUrl { get; set; }

        public string CreatedBy { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public int ParentId { get; set; }

        public bool IsFlexibleFolder { get; set; }

        public string HitHighlightedSummary { get; set; }

        // Can be obtained by using  _api/web/lists(guid'{1}')/items({2})?$expand=File,Folder/ParentFolder/ListItemAllFields
        public IEnumerable<string> UserAccessList { get; set; }

        //File properties
        public int CheckOutType { get; set; }

        public string CheckedOutByUser { get; set; }

        public string FileSizeInBytes { get; set; }

        public string CurrentVersion { get; set; }

        public string CheckInComment { get; set; }

        public string SecondaryFileExtension { get; set; } //JKK - search realted. To be verified if needed

        //Folder Properties
        public int? ItemCount { get; set; }

        public string Type { get; set; }

        public string NameWithoutExtension
        {
            get
            {
                var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(Name);

                if (fileNameWithoutExtension != null)
                    return Name == null ? string.Empty : fileNameWithoutExtension;

                return string.Empty;
            }
        }

        public string Extension
        {
            get
            {
                var extension = Path.GetExtension(Name);

                if (extension != null)
                    return Name == null ? string.Empty : extension;

                return string.Empty;
            }
        }

        public string FileSizeInKb
        {
            get
            {
                return string.IsNullOrEmpty(FileSizeInBytes)
                    ? string.Empty
                    : ((Convert.ToInt64(FileSizeInBytes) / 1024) + " Kb");

            }
        }
    }
}