using ApiSharePointGraitec.Models;
using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiSharePointGraitec.Mapper
{
    public static class GrupoMapper
    {
        public static Grupo Map(Group grupoSharePoint)
        {
            if (grupoSharePoint != null)
                return new Grupo()
                {
                    
                    Id = grupoSharePoint.Id,
                    Title = grupoSharePoint.Title,
                    Description = grupoSharePoint.Description
                };
            else
                return null;

        }

        public static Grupo Map(Group groupSharePoint, List<Usuario> users)
        {
            if (groupSharePoint != null)
                return new Grupo()
                {
                    Title = groupSharePoint.Title.ToString(),
                    Id = groupSharePoint.Id,
                    Description = groupSharePoint.Description,
                    Users = users
                };
            else
                return null;
        }

        public static List<Grupo> Map(GroupCollection groupCollection)
        {
            List<Grupo> result = new List<Grupo>();

            foreach (Group group in groupCollection)
            {
                result.Add(Map(group));
            }

            return result;
        }
    }
}
