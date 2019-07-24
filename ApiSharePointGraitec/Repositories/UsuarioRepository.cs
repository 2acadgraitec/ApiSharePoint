using ApiSharePointGraitec.Mapper;
using ApiSharePointGraitec.Models;
using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiSharePointGraitec.Repositories
{
    public class UsuarioRepository
    {
        private readonly ClientContext _context;
        public UsuarioRepository(ClientContext clientContext)
        {
            _context = clientContext;
        }

        public bool exist (string emailUser)
        {
            var usersResult = _context.LoadQuery(_context.Web.SiteUsers.Where(u => u.Email == emailUser));
            _context.ExecuteQuery();
            return usersResult.Any();
        }

        /// <summary>
        /// devolver a todos los usuarios de un sitio. Esto incluye a los usuarios a los que se les conceden permisos directamente, a los usuarios 
        /// a través de un grupo que han visitado el sitio y a los usuarios a los que se ha hecho referencia en un campo de persona, como por ejemplo, 
        /// al que se les ha asignado una tarea. Llamar a AllUsers[nombre] arrojará una excepción si el usuario no está allí.
        /// </summary>
        /// <returns></returns>
        public List<Usuario> getAllOfSite()
        {            
            return UsuarioMapper.Map(_context.Web.SiteUsers);
        }

        public List<Usuario> getAllOfGroup(int id)
        {
            GroupCollection collGroup = _context.Web.SiteGroups;
            Group oGroup = collGroup.GetById(id);
            _context.Load(oGroup, g => g.Users);
            _context.ExecuteQuery();

            return UsuarioMapper.Map(oGroup.Users);
        }

        public Usuario getByLoginName(string loginName)
        {
            var user = _context.Web.SiteUsers.GetByLoginName(loginName);
            _context.Load(user);
            _context.ExecuteQuery();
            return UsuarioMapper.Map(user);
        }

        public Usuario getByEmail(string email)
        {
            var user = _context.Web.SiteUsers.GetByEmail(email);
            _context.Load(user);
            _context.ExecuteQuery();
            return UsuarioMapper.Map(user);
        }

        public Usuario getById(int id)
        {
            var user = _context.Web.SiteUsers.GetById(id);
            _context.Load(user);
            _context.ExecuteQuery();
            return UsuarioMapper.Map(user);
        }
    }
}
