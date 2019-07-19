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
    public class ListRepository
    {
        private readonly ClientContext _context;
        public ListRepository(ClientContext clientContext)
        {
            _context = clientContext;
        }

        public List<Lista> getAll()
        {
            //Solo recupera los campos que se necesitan.
            _context.Load(_context.Web.Lists, eachList=> eachList.Where(list => list.Hidden == false && (list.BaseType == BaseType.GenericList || list.BaseType == BaseType.DocumentLibrary))
                                                                 .Include(list => list.Title, list => list.Id, list => list.RootFolder, list => list.BaseType));
            
            _context.ExecuteQuery();
            return ListaMapper.Map(_context.Web.Lists);

        }

        public List<Lista> getAllIncludeItems()
        {
            List<Lista> result = new List<Lista>();
            //Solo recupera los campos que se necesitan.
            _context.Load(_context.Web.Lists, eachList => eachList.Where( list => list.Hidden ==false && (list.BaseType == BaseType.GenericList || list.BaseType == BaseType.DocumentLibrary))
                                                                  .Include(list => list.Title, list => list.Id, list=> list.RootFolder, list => list.BaseType));
            _context.ExecuteQuery();
            
            foreach (List list in _context.Web.Lists)
            {
                List<ItemLista> items = getAllItemsById(list.Id);
                result.Add(ListaMapper.Map(list, items));
            }

            return result;
        }

        public Lista getByTitle(string title)
        {            
            return ListaMapper.Map(_context.Web.GetListByTitle(title));
        }

        public Lista getByTitleIncludeItems(string title)
        {
            List list = _context.Web.GetListByTitle(title);
            List<ItemLista> items = getAllItemsById(list.Id);
            return ListaMapper.Map(list, items);
        }

        public Lista getById(Guid guid)
        {
            return ListaMapper.Map(_context.Web.GetListById(guid));
        }

        public Lista getGetListByIdIncludeItems(Guid guid)
        {
            List list = _context.Web.GetListById(guid);
            List<ItemLista> items = getAllItemsById(list.Id);
            return ListaMapper.Map(list, items);
        }

        public List<ItemLista> getAllItemsById(Guid guid)
        {
            var list = _context.Web.GetListById(guid);
            var items = list.GetItems(CamlQuery.CreateAllItemsQuery());
            if (list.BaseType == BaseType.GenericList)
                _context.Load(items, eachItem => eachItem.IncludeWithDefaultProperties(item => item.DisplayName, item => item.File, item=> item.AttachmentFiles));
            else
                _context.Load(items, eachItem => eachItem.IncludeWithDefaultProperties(item => item.DisplayName, item => item.File));

            _context.ExecuteQuery();
            return ItemListaMapper.Map(items);
        }

        public Lista set(string title, string description, ListTemplateType type)
        {
            Web web = _context.Web;

            ListCreationInformation listCreationInfo = new ListCreationInformation();
            listCreationInfo.Title = title;
            listCreationInfo.Description = description;
            listCreationInfo.TemplateType = (int)type;
            
            List list = web.Lists.Add(listCreationInfo);
            _context.ExecuteQuery();
            return getById(list.Id);
        }
    }
}
