using ApiSharePointGraitec.Models;
using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiSharePointGraitec.Mapper
{
    public static class FieldListaMapper
    {
        public static FieldLista Map(Field fieldShapoint)
        {
            if (fieldShapoint != null)
                return new FieldLista()
                {
                    DisplayName = fieldShapoint.Title,
                    Name = fieldShapoint.InternalName,
                    Type = fieldShapoint.TypedObject.ToString(),
                    Id = fieldShapoint.Id,
                    Hidden = fieldShapoint.Hidden,
                    Required = fieldShapoint.Required,
                    StaticName = fieldShapoint.StaticName
                };
            else
                return null;

        }

        public static List<FieldLista> Map(FieldCollection fieldCollectionSharePoint)
        {
            List<FieldLista> result = new List<FieldLista>();

            foreach (Field field in fieldCollectionSharePoint)
            {
                result.Add(Map(field));
            }

            return result;
        }
    }
}
