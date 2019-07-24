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
    public class GroupRepository
    {
        private readonly ClientContext _context;
        public GroupRepository(ClientContext clientContext)
        {
            _context = clientContext;
        }

        public List<Grupo> getAll()
        {
            try
            {
                GroupCollection collGroup = _context.Web.SiteGroups;
                _context.Load(collGroup);
                _context.ExecuteQuery();
                return GrupoMapper.Map(collGroup);

            }
            catch(Exception ex)
            {
                return new List<Grupo>();
            }            
        }

        public Grupo getById(int id)
        {
            try
            {
                GroupCollection collGroup = _context.Web.SiteGroups;
                Group oGroup = collGroup.GetById(id);               
                _context.Load(oGroup);
                _context.ExecuteQuery();
                return GrupoMapper.Map(oGroup);

            }
            catch (Exception ex)
            {
                return new Grupo();
            }
        }

        public List<Usuario> getAllUsersById(int id)
        {            
            UsuarioRepository usuarioRepository = new UsuarioRepository(_context);
            return usuarioRepository.getAllOfGroup(id);
        }

        public void setUser(int idGrupo, string emailUser)
        {
            try
            {
                GroupCollection collGroup = _context.Web.SiteGroups;
                Group group = collGroup.GetById(idGrupo);
                
                UsuarioRepository usuarioRepository = new UsuarioRepository(_context);
                
                if (usuarioRepository.exist(emailUser))
                {
                    var userNew = _context.Web.SiteUsers.GetByEmail(emailUser);
                    group.Users.AddUser(userNew);                    
                    _context.ExecuteQuery();
                }
            }
            catch(Exception ex)
            {

            }            
        }

        public bool setUser(int idGrupo, Usuario usuario)
        {
            try
            {
                GroupCollection collGroup = _context.Web.SiteGroups;
                Group group = collGroup.GetById(idGrupo);

                UsuarioRepository usuarioRepository = new UsuarioRepository(_context);

                if (usuarioRepository.exist(usuario.Email))
                {
                    var userNew = _context.Web.SiteUsers.GetByEmail(usuario.Email);
                    group.Users.AddUser(userNew);
                    _context.ExecuteQuery();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }

        }


    }
}
