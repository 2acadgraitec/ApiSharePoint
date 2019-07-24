using ApiSharePointGraitec.Models;
using ApiSharePointGraitec.Repositories;
using Microsoft.SharePoint.Client;
using OfficeDevPnP.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq.Expressions;

namespace ApiSharePointGraitec
{
    //Para que funcione esta aplicacion es necesario llevar una serie de pasos en el sharepoint
    //1- Crear una cuenta de desarrollo en https://developer.microsoft.com/office/dev-program. Se crea con la cuenta angel.fleta@graitec.com. Se siguen los pasos indicados en https://docs.microsoft.com/en-us/office/developer-program/office-365-developer-program 
    //2- Ahora se crea una suscripcion office 365 de prueba. Para ello se accede a https://developer.microsoft.com/en-us/office/profile. Este enlace llega en un correo.
    // En este caso se ha creado una suscripcion con anfleta@graitecprueba.onmicrosoft.com y contraseña: LocalAdmin09
    //3- Con la suscripcion de prueba de office 365 creada, se crea un sharepoint. En este caso como https://graitecprueba.sharepoint.com
    //Los pasos que se siguen se basan en esta pagina: https://docs.microsoft.com/es-es/sharepoint/dev/solution-guidance/security-apponly-azureacs
    //4- Registra una aplicación nueva en el registro de aplicaciones. https://graitecprueba.sharepoint.com/_layouts/15/appregnew.aspx
    //Id de cliente: 94771451-7b58-4a20-95c8-c3c6bef84c92
    //Clave secreta de cliente: FSwDFnisGje3j5SSIUCwaPNOjWllFyMWt6OJ4ABwiy4=
    //5- Se le de da permisos a la aplicacion https://graitecprueba-admin.sharepoint.com/_layouts/15/appinv.aspx
    //6- En el codigo añadir el nuget: https://www.nuget.org/packages/SharePointPnPCoreOnlinesites


    //El codigo se basa en CSOM. La forma normal de trabajar con CSOM  es:
    //ClientContext: Obtener un contexto de sitio para una URL de sitio determinada.
    //Cargue al contexto con el objeto de sitio que necesita usar como un Sitio, Web o Lista.
    //Llamar al ExecuteQuery.
    //Acceder a las propiedades del objeto.
    public class ApiUoW
    {        
        private readonly ClientContext _context;
        public ListRepository Listas { get; private set; }
        public ItemListRespository ItemListas { get; private set; }
        public FieldListRespository FieldListas { get; private set; }
        public GroupRepository Grupos { get; private set; }
        public UsuarioRepository Usuarios { get; private set; }

        public ApiUoW()
        {
            
            string appAppId = Tools.Security.Decrypt(Properties.Settings.Default.AppId);
            string appSecret = Tools.Security.Decrypt(Properties.Settings.Default.AppSecret);
            string siteUrl = Properties.Settings.Default.SiteUrl;

            _context = new AuthenticationManager().GetAppOnlyAuthenticatedContext(siteUrl, appAppId, appSecret);
            Listas = new ListRepository(_context);
            ItemListas = new ItemListRespository(_context);
            FieldListas = new FieldListRespository(_context);
            Grupos = new GroupRepository(_context);
            Usuarios = new UsuarioRepository(_context);
        }
       
        
        
    }
}
