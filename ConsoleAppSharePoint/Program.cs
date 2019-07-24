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
            //EjecutaCaso1();

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
            //EjecutaCaso8();

            //Caso 9
            //**********
            //Crea una lista añadiendo todos los tipos de campos, editando y eliminando.
            //EjecutaCaso9();

            //Caso 10
            //**********
            //Crea un Grupo en el sitio
            EjecutaCaso10();

            //Caso 11
            //Busqueda de usuario
            //EjecutaCaso11();
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
                itemLista = apiSharePoint.ItemListas.update(listPrueba.Id, itemLista.Id, "Titulo modificado");
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

            //La lista creada se puede ver en "Contenido del sitio" dentro del sitio "Sitio de Prueba"
            Lista lista = apiSharePoint.Listas.getByTitle("Lista creadad desde C# v3");
            //Mira si ya existe, porque si no salta una excepton
            if (lista == null)
            {
                //Crea una lista del tipo Document Library con el nombre "Lista creadad desde C#" y con la descripcion "descripcion temporal"    
                lista = apiSharePoint.Listas.set("Lista creadad desde C# v3", "descripcion temporal", ListTemplateType.DocumentLibrary);
                //Actualiza el titulo, la descripcion y si es visible. Si se pasa un parametro como null, este campo no lo actualiza.
                apiSharePoint.Listas.update(lista.Id, "Lista modificada v2", "Descripcion modificada", true);
                apiSharePoint.Listas.update(lista.Id, null, null, false);
                //Obitiene la lita tras la actualizacion para comprobar los cambios
                lista = apiSharePoint.Listas.getById(lista.Id);

            }
            else
            {
                apiSharePoint.Listas.delete("Lista creadad desde C# v3");
            }


        }
        private static void EjecutaCaso9()
        {
            ApiSharePointGraitec.ApiUoW apiSharePoint = new ApiSharePointGraitec.ApiUoW();
            //Metodo que crea una lista con campos personalizados
            Lista lista = apiSharePoint.Listas.getByTitle("Lista Custom Campos");
            //Mira si ya existe, porque si no salta una excepton
            if (lista == null)
            {
                lista = apiSharePoint.Listas.set("Lista Custom Campos", "", ListTemplateType.DocumentLibrary);
            }
            //Campos Text:            
            //apiSharePoint.FieldListas.setFieldText(lista.Id, "Campo Text Requerido", "campotextReq", false, false);
            //apiSharePoint.FieldListas.setFieldText(lista.Id, "Campo Text", "campotext", false, false);
            ////text oculto a la hora de introducir los datos, pero sale en el listado de documentos o items
            //apiSharePoint.FieldListas.setFieldText(lista.Id, "Campo Text oculto", "campotextoculto", true, false);

            //Campos FieldNote:
            //apiSharePoint.FieldListas.setFieldNote(lista.Id, "NotaBasicaCon6Lineas", "NotaBasicaCon6Lineas", false, false,6,false, false);
            //apiSharePoint.FieldListas.setFieldNote(lista.Id, "NotaBasicaCon6LineasOculto", "NotaBasicaCon6LineasOculto", true, false, 6, false, false);
            //apiSharePoint.FieldListas.setFieldNote(lista.Id, "NotaBasicaCon6LineasRequerido", "NotaBasicaCon6LineasRequerido", false, true, 6, false, false);
            //apiSharePoint.FieldListas.setFieldNote(lista.Id, "NotaBasicaCon6LineasRichText", "NotaBasicaCon6LineasRichText", false, false, 6, true, false);
            //apiSharePoint.FieldListas.setFieldNote(lista.Id, "NotaBasicaCon6LineasRichTextHtml", "NotaBasicaCon6LineasRichTextHtml", false, false, 6, true, true);

            //Campos FieldBoolean
            //apiSharePoint.FieldListas.setFieldBoolean(lista.Id, "Boolean si", "Boolean", false, true);
            //apiSharePoint.FieldListas.setFieldBoolean(lista.Id, "Boolean NO", "Boolean", false, false);
            //apiSharePoint.FieldListas.setFieldBoolean(lista.Id, "Boolean No oculto", "Boolean",true, false);

            //Campos FieldDateTime
            //apiSharePoint.FieldListas.setFieldDateTime(lista.Id, "DateTimeSoloDate", "DateTimeSoloDate", false, true, ApiSharePointGraitec.Repositories.FieldListRespository.FormatDateTime.DateOnly);
            //apiSharePoint.FieldListas.setFieldDateTime(lista.Id, "DateTimeSoloDateTime", "DateTimeSoloDateTime", false, false, ApiSharePointGraitec.Repositories.FieldListRespository.FormatDateTime.DateTime);
            //apiSharePoint.FieldListas.setFieldDateTime(lista.Id, "DateTimeISO8601", "DateTimeISO8601", false, false, ApiSharePointGraitec.Repositories.FieldListRespository.FormatDateTime.ISO8601);
            //apiSharePoint.FieldListas.setFieldDateTime(lista.Id, "DateTimeISO8601Basic", "DateTimeISO8601Basic", false, false, ApiSharePointGraitec.Repositories.FieldListRespository.FormatDateTime.ISO8601Basic);

            //Campos FieldNumber
            //apiSharePoint.FieldListas.setFieldNumber(lista.Id, "Number", "Number", false, true, null, null, null, false);
            //apiSharePoint.FieldListas.setFieldNumber(lista.Id, "decimal", "decimal", false, false, 2, null, null, false);
            //apiSharePoint.FieldListas.setFieldNumber(lista.Id, "MinMax", "decimal", false, false, null, 2, 6, false);
            //apiSharePoint.FieldListas.setFieldNumber(lista.Id, "MinMaxPorcentaje", "decimal", false, false, null, 2, 6, true);

            //Campos FieldChoice
            apiSharePoint.FieldListas.setFieldChoice(lista.Id, "ChoiceRadioButtons", "ChoiceRadioButtons", false, true, ApiSharePointGraitec.Repositories.FieldListRespository.FormatChoice.RadioButtons,
                                                        new string[] { "option 1", "option 2"}, "option 2", false);
            //apiSharePoint.FieldListas.setFieldChoice(lista.Id, "ChoiceDropdown", "ChoiceDropdown", false, true, ApiSharePointGraitec.Repositories.FieldListRespository.FormatChoice.Dropdown,
            //                                            new string[] { "option 1", "option 2" }, "option 2", false);
            //apiSharePoint.FieldListas.setFieldChoice(lista.Id, "ChoiceDropdownfillInChoice", "ChoiceDropdownfillInChoice", false, true, ApiSharePointGraitec.Repositories.FieldListRespository.FormatChoice.Dropdown,
            //                                            new string[] { "option 1", "option 2" }, "option 2", true);

            //campos FieldMultiChoice
            //apiSharePoint.FieldListas.setFieldMultiChoice(lista.Id, "FieldMultiChoice", "FieldMultiChoice", false, true, new string[] { "option 1", "option 2" }, "option 2");

            //Campo FieldPicture
            //apiSharePoint.FieldListas.setFieldPicture(lista.Id, "FieldPicture", "FieldPicture",  true);
            //apiSharePoint.FieldListas.setFieldUrl(lista.Id, "URL", "URL", true);

            //campo FieldUser            
            //apiSharePoint.FieldListas.setFieldUser(lista.Id, "User", "User", false, true);

            //get all field de una lista
            var listField = apiSharePoint.FieldListas.getAll(lista.Id);
            var listFieldVisibles = listField.Where(x =>  x.DisplayName == "ChoiceRadioButtons").ToList();
            var field = apiSharePoint.FieldListas.getById(lista.Id , listFieldVisibles[0].Id);
            apiSharePoint.FieldListas.updateFieldChoice(lista.Id, field.Id, "ChoiceRadioButtons modificado", false, false, ApiSharePointGraitec.Repositories.FieldListRespository.FormatChoice.Dropdown,
                                new string[] { "option 1 modi", "option 2 moid" }, "option 1 modi", true);

            apiSharePoint.FieldListas.deleteField(lista.Id, field.Id);
        }

        public static void EjecutaCaso10()
        {
            ApiSharePointGraitec.ApiUoW apiSharePoint = new ApiSharePointGraitec.ApiUoW();
            //Trae todos los grupos de un sitio
            List<Grupo> list = apiSharePoint.Grupos.getAll();
            //Trae la informacion de un determinado grupo, dado un id
            var grupo = apiSharePoint.Grupos.getById(list[0].Id);
            //Trae todos los usuarios pertenecientes a un grupo, dado el id del grupo.
            var usuarios = apiSharePoint.Grupos.getAllUsersById(grupo.Id);
            apiSharePoint.Grupos.setUser(grupo.Id, "Usurio2@graitecprueba.onmicrosoft.com");

        }

        public static void EjecutaCaso11()
        {
            ApiSharePointGraitec.ApiUoW apiSharePoint = new ApiSharePointGraitec.ApiUoW();
            var usuario = apiSharePoint.Usuarios.getByLoginName(@"i:0#.f|membership|usurio2@graitecprueba.onmicrosoft.com");
            var usuario2 = apiSharePoint.Usuarios.getByEmail("prueba@graitecprueba.onmicrosoft.com");
        }
    }
}
