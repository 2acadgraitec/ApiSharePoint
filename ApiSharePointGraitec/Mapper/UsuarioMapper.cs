using ApiSharePointGraitec.Models;
using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiSharePointGraitec.Mapper
{
    public static class UsuarioMapper
    {
        public static Usuario Map(User userSharePoint)
        {
            if (userSharePoint != null)
                return new Usuario()
                {   
                    Id = userSharePoint.Id,
                    Email = userSharePoint.Email,
                    LoginName = userSharePoint.LoginName,
                    Title = userSharePoint.Title,
                };
            else
                return null;

        }

        public static List<Usuario> Map(UserCollection userCollection)
        {
            List<Usuario> result = new List<Usuario>();

            foreach (User userSharePoint in userCollection)
            {
                result.Add(Map(userSharePoint));
            }

            return result;
        }
    }
}
