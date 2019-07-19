using ApiSharePointGraitec.Models;
using Microsoft.SharePoint.Client;
using System.Collections.Generic;

namespace ApiSharePointGraitec.Mapper
{
    public static class ListaMapper
    {
        

        public static Lista Map(List listSharePoint)
        {
            if (listSharePoint != null)
                return new Lista()
                {
                    Title = listSharePoint.Title.ToString(),
                    Id = listSharePoint.Id,
                    Type = listSharePoint.BaseType,
                    RootFolder = listSharePoint.RootFolder,
                    ServerRelativeUrl = listSharePoint.RootFolder.ServerRelativeUrl,
                    Tag = (listSharePoint.IsPropertyAvailable("Tag")) ? listSharePoint.Tag : null
                };
            else
                return null;
        }

        public static Lista Map(List listSharePoint, List<ItemLista> items)
        {
            if (listSharePoint != null)
                return new Lista()
                {
                    Title = listSharePoint.Title.ToString(),
                    Id = listSharePoint.Id,
                    Type = listSharePoint.BaseType,
                    RootFolder = listSharePoint.RootFolder,
                    ServerRelativeUrl = listSharePoint.RootFolder.ServerRelativeUrl,
                    Items = items,
                    Tag = (listSharePoint.IsPropertyAvailable("Tag")) ? listSharePoint.Tag : null
                };
            else
                return null;
        }

        public static List<Lista> Map(ListCollection listCollection)
        {
            List<Lista> result = new List<Lista>();

            foreach (List list in listCollection)
            {

                    result.Add(Map(list));
            }

            return result;
        }
        
    }
}
