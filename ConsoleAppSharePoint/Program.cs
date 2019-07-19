using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeDevPnP.Core;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Client;
using ApiSharePointGraitec.Models;

namespace ConsoleAppSharePoint
{
    class Program
    {
        static void Main(string[] args)
        {



            //Caso 1
            //**********
            //Trae todas las listas visibles en sharepoint, con todos los items
            EjecutaCaso1();

            //Caso 2
            //**********
            //Trae una lista especifica, en este caso, la lista es ListaDePrueba, con todos los items            
            //De la lista ListaDePrueba, de su item ItemPruebaConAdjunto, se trae el segundo file adjunto
            //EjecutaCaso2();

            //Caso 3
            //**********
            //Trae una lista especifica, en este caso, la lista es Documentos, con todos los items            
            //Descaga el segundo file. Aqui no hay adjuntos
            //EjecutaCaso3();

            //Caso 4
            //**********
            //Descarga un archivo dado el id de la lista, el id del item, dado que el item es un documento y no un item con adjunto.
            //EjecutaCaso4();


            //Caso 5
            //**********
            //Sube un archivo a la lista Documentos que es del tipo DocumentLibrary
            //EjecutaCaso5();

            //Caso 6
            //**********
            //Sube un archivo como attachment a un item existente, de una lista existente del tipo GenericList
            //EjecutaCaso6();

            //Caso 7
            //**********
            //Crea un item en una lista existente del tipo GenericList
            //EjecutaCaso7();

            //Caso 8
            //**********
            //Crea, Edita y elimina una lista
            EjecutaCaso8();

        }

        private static void EjecutaCaso1()
        {
            ApiSharePointGraitec.ApiUoW apiSharePoint = new ApiSharePointGraitec.ApiUoW();
            //Caso 1
            //**********
            //Trae todas las listas visibles en sharepoint, con todos los items
            List<Lista> listListas = apiSharePoint.Listas.getAllIncludeItems();
        }

        private static void EjecutaCaso2()
        {
            ApiSharePointGraitec.ApiUoW apiSharePoint = new ApiSharePointGraitec.ApiUoW();
            //Caso 2
            //**********
            //Trae una lista especifica, en este caso, la lista es ListaDePrueba, con todos los items
            Lista listListaDePrueba = apiSharePoint.Listas.getByTitleIncludeItems("ListaDePrueba");
            //De la lista ListaDePrueba, de su item ItemPruebaConAdjunto, se trae el segundo file adjunto
            apiSharePoint.ItemListas.downloadFile(listListaDePrueba.Items[0].Attachments[1], @"C:\2aCAD");
        }
        private static void EjecutaCaso3()
        {
            ApiSharePointGraitec.ApiUoW apiSharePoint = new ApiSharePointGraitec.ApiUoW();
            //Trae una lista especifica, en este caso, la lista es Documentos, con todos los items
            Lista listDocument = apiSharePoint.Listas.getByTitleIncludeItems("ListaDePrueba");
            //Descaga el segundo file. Aqui no hay adjuntos
            apiSharePoint.ItemListas.downloadFile(listDocument.Items[1].File, @"C:\2aCAD");
        }

        private static void EjecutaCaso4()
        {
            ////Descarga un archivo dado el id de la lista, el id del item, dado que el item es un documento y no un item con adjunto.
            ApiSharePointGraitec.ApiUoW apiSharePoint = new ApiSharePointGraitec.ApiUoW();
            apiSharePoint.ItemListas.downloadFile(new Guid("33cac1a3-1f5f-44fc-9ce4-a4a2ca6b17d6"), 1, @"C:\2aCAD");
        }
        private static void EjecutaCaso5()
        {            
            ApiSharePointGraitec.ApiUoW apiSharePoint = new ApiSharePointGraitec.ApiUoW();
            //Trae una lista especifica, en este caso, la lista es Documentos, con todos los items
            Lista listDocument = apiSharePoint.Listas.getByTitle("Documentos");
            if (listDocument.Type == BaseType.DocumentLibrary)
            {
                //Para que funcione, la lista tiene que ser de tipo DocumentLibrary
                //En la carpeta root de la lista esta añadiendo un nuevo documento que esta en el equipo local. 
                apiSharePoint.ItemListas.uploadFile(listDocument.RootFolder, @"C:\Users\angel\Desktop\Logo-2aCAD-2016.png");
            }
            

        }

        private static void EjecutaCaso6()
        {
            ApiSharePointGraitec.ApiUoW apiSharePoint = new ApiSharePointGraitec.ApiUoW();
            //Trae una lista especifica, en este caso, la lista es ListaDePrueba, con todos los items
            Lista listDocument = apiSharePoint.Listas.getByTitleIncludeItems("ListaDePrueba");
            if (listDocument.Type == BaseType.GenericList)
            {
                //Para que funcione, la lista tiene que ser de tipo GenericList
                //En la carpeta root de la lista esta añadiendo un nuevo documento que esta en el equipo local. 
                apiSharePoint.ItemListas.uploadAttachment(listDocument.Id, listDocument.Items[0].Id, @"C:\Users\angel\Desktop\Logo-2aCAD-2016.png");
            }


        }

        private static void EjecutaCaso7()
        {
            ApiSharePointGraitec.ApiUoW apiSharePoint = new ApiSharePointGraitec.ApiUoW();
            //Trae una lista especifica, en este caso, la lista es ListaDePrueba, con todos los items
            Lista listPrueba = apiSharePoint.Listas.getByTitle("ListaDePrueba");
            if (listPrueba.Type == BaseType.GenericList)
            {
                //Para que funcione, la lista tiene que ser de tipo GenericList
                //Añade un item list con un titulo especifico
                apiSharePoint.ItemListas.set(listPrueba.Id, "Titulo del Item");
                //Añade un item list con un titulo especifico y le adjunta un archivo
                ItemLista itemLista = apiSharePoint.ItemListas.set(listPrueba.Id, "Para modificar", @"C:\Users\angel\Desktop\Logo-2aCAD-2016.png");
                //Modifica el titulo de un item liste existente
                itemLista = apiSharePoint.ItemListas.updateItemLista(listPrueba.Id, itemLista.Id, "Titulo modificado");
                //Elimina un archivo adjunto de un item list dado el nombre del archivo adjunto
                apiSharePoint.ItemListas.deleteAttachment(listPrueba.Id, itemLista.Id, "Logo-2aCAD-2016.png");
                //Elimina un item de una lista
                apiSharePoint.ItemListas.deleteItemLista(listPrueba.Id, itemLista.Id);
                
            }

            Lista listDocument = apiSharePoint.Listas.getByTitleIncludeItems("Documentos");
            apiSharePoint.ItemListas.deleteItemLista(listDocument.Id, listDocument.Items[1].Id);

        }
        private static void EjecutaCaso8()
        {
            ApiSharePointGraitec.ApiUoW apiSharePoint = new ApiSharePointGraitec.ApiUoW();
            Lista lista = apiSharePoint.Listas.set("Lista creadad desde C#", "descripcion temporal", ListTemplateType.DocumentLibrary);
        }

    }
}
