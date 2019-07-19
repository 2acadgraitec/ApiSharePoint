using ApiSharePointGraitec.Mapper;
using ApiSharePointGraitec.Models;
using Microsoft.SharePoint.Client;
using System;
using System.Linq;

namespace ApiSharePointGraitec.Repositories
{
    public class ItemListRespository
    {
        private readonly ClientContext _context;
        public ItemListRespository(ClientContext clientContext)
        {
            _context = clientContext;
        }

        public ItemLista getById (Guid guidList, int idItem)
        {
            var list = _context.Web.Lists.GetById(guidList);
            var listItem = list.GetItemById(idItem);
            _context.Load(list);
            _context.Load(listItem, i => i.DisplayName, i => i.FileSystemObjectType, i => i.Id , i => i.File, i => i.AttachmentFiles);
            _context.ExecuteQuery();
            
            return ItemListaMapper.Map(listItem);
        }

        public bool downloadFile(Guid guidList, int idItem, string filePathEnd)
        {
            try
            {
                
                var listItem = getById(guidList, idItem);
                
                if (listItem != null && listItem.FileServerRelativeUrl!= null)
                {
                    ClientResult<System.IO.Stream> data = listItem.File.OpenBinaryStream();
                    _context.Load(listItem.File);
                    _context.ExecuteQuery();
                    using (System.IO.MemoryStream mStream = new System.IO.MemoryStream())
                    {
                        if (data != null)
                        {
                            data.Value.CopyTo(mStream);
                            byte[] imageArray = mStream.ToArray();
                            string b64String = Convert.ToBase64String(imageArray);
                            System.IO.File.WriteAllBytes(System.IO.Path.Combine(filePathEnd, listItem.File.Name), mStream.ToArray());
                        }
                    }
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public bool downloadFile(File fileSharePoint, string filePathEnd)
        {
            try
            {

                ClientResult<System.IO.Stream> data = fileSharePoint.OpenBinaryStream();
                _context.Load(fileSharePoint);
                _context.ExecuteQuery();
                using (System.IO.MemoryStream mStream = new System.IO.MemoryStream())
                {
                    if (data != null)
                    {
                        data.Value.CopyTo(mStream);
                        byte[] imageArray = mStream.ToArray();
                        string b64String = Convert.ToBase64String(imageArray);
                        System.IO.File.WriteAllBytes(System.IO.Path.Combine(filePathEnd, fileSharePoint.Name), mStream.ToArray());
                    }
                }
                return true;
                
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public bool downloadFile(Attachment attachSharePoint, string filePathEnd)
        {
            try
            {
                //Se obtiene la carpeta en donde estan los adjuntos del item. Para ello se quita el nombre del archivo a la ServerRelativeUrl
                var pathFolderServer = attachSharePoint.ServerRelativeUrl.Replace(attachSharePoint.FileName, "");                
                Folder attFolder = _context.Web.GetFolderByServerRelativeUrl(pathFolderServer);
                FileCollection filesFolder = attFolder.Files;
                
                //Si no se hace el where, traería todos los adjuntos que estan en el item, porque se esta consultado la carpeta del mismo
                _context.Load(filesFolder, files => files.Where( file => file.Name == attachSharePoint.FileName)
                                                         .Include(file => file.ServerRelativeUrl, file => file.Name, file => file.ServerRelativeUrl)
                             );

                _context.ExecuteQuery();

                if (filesFolder.Count == 1)
                    return downloadFile(filesFolder[0], filePathEnd);
                else
                    return false;

            }
            catch (Exception ex)
            {
                return false;
            }

        }
        


        public bool uploadFile(Folder folder, string filePathSource)
        {
            try
            {
                _context.Load(folder);
                _context.ExecuteQuery();

                if (!System.IO.File.Exists(filePathSource))
                    throw new System.IO.FileNotFoundException("File not found.", filePathSource);

                // Prepara la informacion del documento a subir
                var newFile = new FileCreationInformation()
                {
                    Content = System.IO.File.ReadAllBytes(filePathSource),
                    Url = System.IO.Path.GetFileName(filePathSource),
                    Overwrite = true
                };
                
                File file = folder.Files.Add(newFile);

                // Commit 
                folder.Update();
                _context.ExecuteQuery();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
            

        }

        public bool uploadAttachment(Guid guidList, int idItem, string filePathSource)
        {
            try
            {
                if (!System.IO.File.Exists(filePathSource))
                    throw new System.IO.FileNotFoundException("File not found.", filePathSource);

                var list = _context.Web.Lists.GetById(guidList);
                var listItem = list.GetItemById(idItem);               
                _context.Load(listItem, i => i.AttachmentFiles);
                _context.ExecuteQuery();

                if (listItem != null)
                {
                    using (System.IO.FileStream fs = new System.IO.FileStream(filePathSource, System.IO.FileMode.Open))
                    {
                        var attInfo = new AttachmentCreationInformation()
                        {
                            FileName = System.IO.Path.GetFileName(filePathSource),
                            ContentStream = fs
                        };

                        var att = listItem.AttachmentFiles.Add(attInfo);
                        _context.Load(att);
                        _context.ExecuteQuery();
                    }

                }
                
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public ItemLista set (Guid guidList, string tituloItem)
        {
            var list = _context.Web.Lists.GetById(guidList);
            
            ListItemCreationInformation itemCreateInfo = new ListItemCreationInformation();
            ListItem listItem = list.AddItem(new ListItemCreationInformation());
            listItem["Title"] = tituloItem;            
            listItem.Update();
            _context.ExecuteQuery();
            return getById(guidList, listItem.Id);
        }

        public ItemLista set(Guid guidList, string tituloItem, string filePathSource)
        {
            ItemLista itemLista = set(guidList, tituloItem);
            uploadAttachment(guidList, itemLista.Id, filePathSource);
            //Vuelve a llamar para que actualice los adjuntos.
            return getById(guidList, itemLista.Id);
        }

        public ItemLista updateItemLista(Guid guidList, int idItem, string tituloItem)
        {
            var list = _context.Web.Lists.GetById(guidList);
            ListItem listItem = list.GetItemById(idItem);
            listItem["Title"] = tituloItem;
            listItem.Update();
            _context.ExecuteQuery();
            return getById(guidList, idItem);
        }

        public void deleteItemLista(Guid guidList, int idItem)
        {
            var list = _context.Web.Lists.GetById(guidList);
            ListItem listItem = list.GetItemById(idItem);            
            listItem.DeleteObject();
            _context.ExecuteQuery();
            
        }

        public bool deleteAttachment(Guid guidList, int idItem, string fileName)
        {
            try
            {
               
                var list = _context.Web.Lists.GetById(guidList);
                var listItem = list.GetItemById(idItem);
                _context.Load(listItem, i => i.AttachmentFiles);
                
                if (listItem != null)
                {
                    listItem.AttachmentFiles.GetByFileName(fileName).DeleteObject();                    
                    listItem.Update();
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

        public bool deleteAttachment(Guid guidList, int idItem, int indexAttachment)
        {
            try
            {

                var list = _context.Web.Lists.GetById(guidList);
                var listItem = list.GetItemById(idItem);
                _context.Load(listItem, i => i.AttachmentFiles);
                
                if (listItem != null)
                {                    
                    listItem.AttachmentFiles[indexAttachment].DeleteObject();
                    listItem.Update();
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
