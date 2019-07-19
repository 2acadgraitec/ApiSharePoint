using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiSharePointGraitec.Models
{
    public class Lista
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Folder RootFolder { get; set; }
        public string ServerRelativeUrl { get; set; }
        public BaseType Type { get; set; }
        public List<ItemLista> Items { get; set; }
    }
}
