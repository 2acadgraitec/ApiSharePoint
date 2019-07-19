using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint.Client;

namespace ApiSharePointGraitec.Models
{
    public class ItemLista
    {
        public int Id { get; set; }
        public string DisplayName { get; set; }
        public string Type { get; set; }
        public string FileServerRelativeUrl { get; set; }
        public string FileName { get; set; }
        public File File { get; set; }
        public AttachmentCollection Attachments {get;set;}
    }
}
