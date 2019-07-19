using ApiSharePointGraitec.Models;
using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiSharePointGraitec.Mapper
{
    public static class ItemListaMapper
    {
        public static ItemLista Map(ListItem itemListSharePoint)
        {
            if (itemListSharePoint != null)
                return new ItemLista()
                {
                    DisplayName = itemListSharePoint.DisplayName,
                    Type = itemListSharePoint.FileSystemObjectType.ToString(),
                    Id = itemListSharePoint.Id,
                    Attachments = itemListSharePoint.AttachmentFiles,
                    File = itemListSharePoint.File,
                    FileName = (itemListSharePoint.FileSystemObjectType.ToString().ToLower() == "file" && itemListSharePoint.File.IsPropertyAvailable("Name")) ? itemListSharePoint.File.Name : null,
                    FileServerRelativeUrl = (itemListSharePoint.FileSystemObjectType.ToString().ToLower() == "file" && itemListSharePoint.File.IsPropertyAvailable("ServerRelativeUrl")) ? itemListSharePoint.File.ServerRelativeUrl : null                    
                };
            else
                return null;

        }

        public static List<ItemLista> Map(ListItemCollection listItemCollection)
        {
            List<ItemLista> result = new List<ItemLista>();

            foreach (ListItem itemListSharePoint in listItemCollection)
            {
                result.Add(Map(itemListSharePoint));
            }

            return result;
        }
    }
}
