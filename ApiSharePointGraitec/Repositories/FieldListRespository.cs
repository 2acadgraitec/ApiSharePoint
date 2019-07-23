using ApiSharePointGraitec.Mapper;
using ApiSharePointGraitec.Models;
using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace ApiSharePointGraitec.Repositories
{
    public class FieldListRespository
    {
        private readonly ClientContext _context;

        public enum FieldType
        {            
            Text,
            Note,
            Boolean,
            DateTime,
            Number,
            Choice,
            MultiChoice,
            URL,
            Lookup,
            User,
            UserMulti,
            TaxonomyFieldType,
            Calculated
        }
        
        public enum FormatDateTime
        {
            DateOnly,
            DateTime,
            ISO8601, //Ejemplo: 2019-07-10T00:00:00Z 00:00
            ISO8601Basic //Ejemplo: 20190730T070000Z 00:00
        }
        public enum FormatChoice
        {
            RadioButtons,
            Dropdown
        }
        public FieldListRespository(ClientContext clientContext)
        {
            _context = clientContext;
        }
          
        public bool setFieldText (Guid guidList, string displayName, string name,  bool hidden, bool required)
        {

            //Se puede sacar informacion de esta web: https://karinebosch.wordpress.com/my-articles/creating-fields-using-csom/
            //Informacion de las propiedades del campo https://docs.microsoft.com/es-es/sharepoint/dev/schema/field-element-field
            try
            {
                var list = _context.Web.Lists.GetById(guidList);
                if (list != null)
                {
                    string shemaXml = "<Field id='" + Guid.NewGuid().ToString() + "'" +
                                                                 " Name='" + name + "'" +
                                                                 " StaticName='" + name + "'" +
                                                                 " DisplayName='" + displayName + "'" +
                                                                 " Type='Text'" +
                                                                 " Hidden='" + hidden.ToString().ToUpper() + "'" +
                                                                 " Required='" + required.ToString().ToUpper() + "'" +
                                                                 " />";

                    var field = list.Fields.AddFieldAsXml(shemaXml, true, AddFieldOptions.DefaultValue);

                    list.Update();
                    _context.ExecuteQuery();
                    return true;
                }
                else
                {
                    return false;
                }
                
            }
            catch(Exception ex)
            {
                return false;
            }            
        }
        public bool updateFieldText(Guid guidList, Guid id, string displayName, bool? hidden, bool? required)
        {
            try
            {
                var list = _context.Web.Lists.GetById(guidList);
                var field = list.GetFieldById(id);

                var doc = new XmlDocument();
                doc.LoadXml(field.SchemaXml);
                if (displayName != null) doc.FirstChild.Attributes["DisplayName"].Value = displayName;
                if (hidden != null) doc.FirstChild.Attributes["Hidden"].Value = hidden.Value.ToString().ToUpper();
                if (required != null) doc.FirstChild.Attributes["Required"].Value = required.Value.ToString().ToUpper();

                field.SchemaXml = doc.OuterXml;
                field.Update();
                _context.ExecuteQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public bool setFieldNote(Guid guidList, string displayName, string name, bool hidden, bool required, int numLineas, bool richText, bool htmlText)
        {

            //Se puede sacar informacion de esta web: https://karinebosch.wordpress.com/my-articles/creating-fields-using-csom/
            //Informacion de las propiedades del campo https://docs.microsoft.com/es-es/sharepoint/dev/schema/field-element-field
            try
            {
                var list = _context.Web.Lists.GetById(guidList);
                if (list != null)
                {
                    var field = list.Fields.AddFieldAsXml("<Field id='"+Guid.NewGuid().ToString()+"'" +
                                                                 " Name='" + name + "'" +
                                                                 " StaticName='" + name + "'" +
                                                                 " DisplayName='" + displayName + "'" +
                                                                 " Type='Note'" +
                                                                 " RichText = '" + richText.ToString().ToUpper() + "'" +
                                                                  ((htmlText) ? " RichTextMode ='FullHtml'" : "") +
                                                                  ((htmlText) ? " IsolateStyles ='TRUE'" : "") +
                                                                 " NumLines = '" + numLineas.ToString() + "'" +
                                                                 " Hidden='" + hidden.ToString().ToUpper() + "'" +
                                                                 " Required='" + required.ToString().ToUpper() + "'" +
                                                                 " />"
                                                          , true, AddFieldOptions.DefaultValue);

                    list.Update();
                    _context.ExecuteQuery();
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool updateFieldNote(Guid guidList, Guid id, string displayName,  bool? hidden, bool? required, int? numLineas, bool? richText, bool? htmlText)
        {
            try
            {
                var list = _context.Web.Lists.GetById(guidList);
                var field = list.GetFieldById(id);

                var doc = new XmlDocument();
                doc.LoadXml(field.SchemaXml);

                if (displayName != null) doc.FirstChild.Attributes["DisplayName"].Value = displayName;
                if (hidden != null) doc.FirstChild.Attributes["Hidden"].Value = hidden.Value.ToString().ToUpper();
                if (required != null) doc.FirstChild.Attributes["Required"].Value = required.Value.ToString().ToUpper();
                if (richText != null) doc.FirstChild.Attributes["RichText"].Value = richText.Value.ToString().ToUpper();
                if (htmlText != null) {
                    if (htmlText.Value)
                    {
                        doc.FirstChild.Attributes["RichTextMode"].Value = "FullHtml";
                        doc.FirstChild.Attributes["IsolateStyles"].Value = "TRUE";
                    }
                    else
                    {
                        if (doc.FirstChild.Attributes["RichTextMode"]!=null)
                            doc.FirstChild.Attributes.Remove(doc.FirstChild.Attributes["RichTextMode"]);
                        if (doc.FirstChild.Attributes["IsolateStyles"] != null)
                            doc.FirstChild.Attributes.Remove(doc.FirstChild.Attributes["IsolateStyles"]);
                    }                    
                }
                if (numLineas != null) doc.FirstChild.Attributes["NumLines"].Value = numLineas.ToString();

                field.SchemaXml = doc.OuterXml;
                field.Update();
                _context.ExecuteQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public bool setFieldBoolean(Guid guidList, string displayName, string name, bool hidden, bool defaultValue)
        {

            //Se puede sacar informacion de esta web: https://karinebosch.wordpress.com/my-articles/creating-fields-using-csom/
            //Informacion de las propiedades del campo https://docs.microsoft.com/es-es/sharepoint/dev/schema/field-element-field
            try
            {
                var list = _context.Web.Lists.GetById(guidList);
                if (list != null)
                {
                    var field = list.Fields.AddFieldAsXml("<Field id='"+Guid.NewGuid().ToString()+"'" +
                                                                 " Name='" + name + "'" +
                                                                 " StaticName='" + name + "'" +
                                                                 " DisplayName='" + displayName + "'" +
                                                                 " Type='Boolean'" +                                                                 
                                                                 " Hidden='" + hidden.ToString().ToUpper() + "'" +                                                                 
                                                                 ">" +
                                                                 "<Default>"+((defaultValue)? "1":"0") + "</Default>" +
                                                                 "</Field>"

                                                          , true, AddFieldOptions.DefaultValue);

                    list.Update();
                    _context.ExecuteQuery();
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool updateFieldBoolean(Guid guidList, Guid id, string displayName, bool? hidden, bool? defaultValue)
        {
            try
            {
                var list = _context.Web.Lists.GetById(guidList);
                var field = list.GetFieldById(id);

                var doc = new XmlDocument();
                doc.LoadXml(field.SchemaXml);

                if (displayName != null) doc.FirstChild.Attributes["DisplayName"].Value = displayName;
                if (hidden != null) doc.FirstChild.Attributes["Hidden"].Value = hidden.Value.ToString().ToUpper();
                if (defaultValue != null) doc.FirstChild.ChildNodes[0].InnerXml = ((defaultValue.Value) ? "1" : "0");

                field.SchemaXml = doc.OuterXml;
                field.Update();
                _context.ExecuteQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public bool setFieldDateTime(Guid guidList, string displayName, string name, bool hidden, bool required, FormatDateTime format)
        {

            //Se puede sacar informacion de esta web: https://karinebosch.wordpress.com/my-articles/creating-fields-using-csom/
            //Informacion de las propiedades del campo https://docs.microsoft.com/es-es/sharepoint/dev/schema/field-element-field
            try
            {
                var list = _context.Web.Lists.GetById(guidList);
                if (list != null)
                {
                    var field = list.Fields.AddFieldAsXml("<Field id='"+Guid.NewGuid().ToString()+"'" +
                                                                 " Name='" + name + "'" +
                                                                 " StaticName='" + name + "'" +
                                                                 " DisplayName='" + displayName + "'" +
                                                                 " Type='DateTime'" +
                                                                 " Format='" + format + "'" +
                                                                 " Hidden='" + hidden.ToString().ToUpper() + "'" +
                                                                 " Required='" + required.ToString().ToUpper() + "'" +
                                                                 "/>"
                                                          , true, AddFieldOptions.DefaultValue);

                    list.Update();
                    _context.ExecuteQuery();
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool updateFieldDateTime(Guid guidList, Guid id, string displayName, bool? hidden, bool? required, FormatDateTime? format)
        {
            try
            {
                var list = _context.Web.Lists.GetById(guidList);
                var field = list.GetFieldById(id);

                var doc = new XmlDocument();
                doc.LoadXml(field.SchemaXml);

                if (displayName != null) doc.FirstChild.Attributes["DisplayName"].Value = displayName;
                if (hidden != null) doc.FirstChild.Attributes["Hidden"].Value = hidden.Value.ToString().ToUpper();
                if (required != null) doc.FirstChild.Attributes["Required"].Value = required.Value.ToString().ToUpper();
                if (format != null) doc.FirstChild.Attributes["Format"].Value = format.Value.ToString();

                
                field.SchemaXml = doc.OuterXml;
                field.Update();
                _context.ExecuteQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public bool setFieldNumber(Guid guidList, string displayName, string name, bool hidden, bool required, int? numDecimals, int? min, int? max, bool percentage)
        {

            //Se puede sacar informacion de esta web: https://karinebosch.wordpress.com/my-articles/creating-fields-using-csom/
            //Informacion de las propiedades del campo https://docs.microsoft.com/es-es/sharepoint/dev/schema/field-element-field
            try
            {
                var list = _context.Web.Lists.GetById(guidList);
                if (list != null)
                {
                    var field = list.Fields.AddFieldAsXml("<Field id='"+Guid.NewGuid().ToString()+"'" +
                                                                 " Name='" + name + "'" +
                                                                 " StaticName='" + name + "'" +
                                                                 " DisplayName='" + displayName + "'" +
                                                                 " Type='Number'" +
                                                                 ((numDecimals!=null) ? " Decimals ='" + numDecimals.ToString() + "'" : "") +
                                                                 ((min != null) ? " Min ='" + min.ToString() + "'" : "") +
                                                                 ((max != null) ? " Max ='" + max.ToString() + "'" : "") +
                                                                 ((percentage) ? " Percentage ='TRUE'" : "") +
                                                                 " Hidden='" + hidden.ToString().ToUpper() .ToUpper()  + "'" +
                                                                 " Required='" + required.ToString().ToUpper() + "'" +
                                                                 "/>"
                                                          , true, AddFieldOptions.DefaultValue);

                    list.Update();
                    _context.ExecuteQuery();
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool updateFieldNumber(Guid guidList, Guid id, string displayName, bool? hidden, bool? required, int? numDecimals, int? min, int? max, bool? percentage)
        {
            try
            {
                var list = _context.Web.Lists.GetById(guidList);
                var field = list.GetFieldById(id);

                var doc = new XmlDocument();
                doc.LoadXml(field.SchemaXml);

                if (displayName != null) doc.FirstChild.Attributes["DisplayName"].Value = displayName;
                if (hidden != null) doc.FirstChild.Attributes["Hidden"].Value = hidden.Value.ToString().ToUpper();
                if (required != null) doc.FirstChild.Attributes["Required"].Value = required.Value.ToString().ToUpper();
                if (numDecimals != null)
                {
                    if (doc.FirstChild.Attributes["Decimals"] == null)
                    {
                        XmlAttribute att = doc.CreateAttribute("Decimals");
                        doc.FirstChild.Attributes.Append(att);
                    }
                    doc.FirstChild.Attributes["Decimals"].Value = numDecimals.Value.ToString();
                }

                if (min != null)
                {
                    if (doc.FirstChild.Attributes["Min"] == null)
                    {
                        XmlAttribute att = doc.CreateAttribute("Min");
                        doc.FirstChild.Attributes.Append(att);
                    }
                    doc.FirstChild.Attributes["Min"].Value = min.Value.ToString();
                }

                if (max != null)
                {
                    if (doc.FirstChild.Attributes["Max"] == null)
                    {
                        XmlAttribute att = doc.CreateAttribute("Max");
                        doc.FirstChild.Attributes.Append(att);
                    }
                    doc.FirstChild.Attributes["Max"].Value = min.Value.ToString();
                }

                if (percentage != null)
                {
                    if (doc.FirstChild.Attributes["Percentage"] == null)
                    {
                        XmlAttribute att = doc.CreateAttribute("Percentage");
                        doc.FirstChild.Attributes.Append(att);                       
                    }
                    doc.FirstChild.Attributes["Percentage"].Value = percentage.Value.ToString().ToUpper();

                }

                field.SchemaXml = doc.OuterXml;
                field.Update();
                _context.ExecuteQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public bool setFieldChoice(Guid guidList, string displayName, string name, bool hidden, bool required, FormatChoice format, string[] optionsChoice, string defaultValue, bool fillInChoice)
        {

            //Se puede sacar informacion de esta web: https://karinebosch.wordpress.com/my-articles/creating-fields-using-csom/
            //Informacion de las propiedades del campo https://docs.microsoft.com/es-es/sharepoint/dev/schema/field-element-field
            try
            {
                var list = _context.Web.Lists.GetById(guidList);
                if (list != null)
                {
                    //Para permitir a los usuarios rellenar una opción diferente a la que ya está disponible, debe añadir el atributo FillInChoice. 
                    //Si ese atributo se omite en el XML, los usuarios sólo podrán elegir entre las opciones disponibles.
                    string schemaXMl = "<Field id='"+Guid.NewGuid().ToString()+"'" +
                                                                 " Name='" + name + "'" +
                                                                 " StaticName='" + name + "'" +
                                                                 " DisplayName='" + displayName + "'" +
                                                                 " Type='Choice'" +
                                                                 " Hidden='" + hidden.ToString().ToUpper()  + "'" +
                                                                 " Format='" + format + "'" +
                                                                 ((fillInChoice) ? " FillInChoice='TRUE'" : "") +
                                                                 " Required='" + required.ToString().ToUpper() + "'" +
                                                                  ">";
                    if (defaultValue!=null)
                        schemaXMl += "<Default>" + defaultValue + "</Default>";


                    schemaXMl += "<CHOICES>";
                    foreach (string option in optionsChoice)
                    {
                        schemaXMl += "<CHOICE>"+ option + "</CHOICE>";
                    }
                    schemaXMl += "</CHOICES>";
                    schemaXMl += "</Field>";
                    var field = list.Fields.AddFieldAsXml(schemaXMl, true, AddFieldOptions.DefaultValue);

                    list.Update();
                    _context.ExecuteQuery();
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool updateFieldChoice(Guid guidList, Guid id, string displayName, bool? hidden, bool? required, FormatChoice? format, string[] optionsChoice, string defaultValue, bool? fillInChoice)
        {
            try
            {
                var list = _context.Web.Lists.GetById(guidList);
                var field = list.GetFieldById(id);

                var doc = new XmlDocument();
                doc.LoadXml(field.SchemaXml);

                if (displayName != null) doc.FirstChild.Attributes["DisplayName"].Value = displayName;
                if (hidden != null) doc.FirstChild.Attributes["Hidden"].Value = hidden.Value.ToString().ToUpper();
                if (required != null) doc.FirstChild.Attributes["Required"].Value = required.Value.ToString().ToUpper();
                if (format != null) doc.FirstChild.Attributes["Format"].Value = format.Value.ToString();

                if (fillInChoice != null)
                {
                    if (doc.FirstChild.Attributes["FillInChoice"] == null)
                    {
                        XmlAttribute att = doc.CreateAttribute("FillInChoice");
                        doc.FirstChild.Attributes.Append(att);
                    }
                    doc.FirstChild.Attributes["FillInChoice"].Value = fillInChoice.Value.ToString().ToUpper();

                }

                if (defaultValue != null) doc.FirstChild.ChildNodes[0].InnerXml = defaultValue;

                if (optionsChoice != null)
                {
                    doc.FirstChild.RemoveChild(doc.GetElementsByTagName("CHOICES")[0]);
                    XmlDocumentFragment xmlDocFrag = doc.CreateDocumentFragment();
                    string newNode = "<CHOICES>";
                    foreach (string option in optionsChoice)
                    {
                        newNode += "<CHOICE>" + option + "</CHOICE>";
                    }
                    newNode += "</CHOICES>";
                    xmlDocFrag.InnerXml = newNode;
                    doc.FirstChild.AppendChild(xmlDocFrag);
                }

                field.SchemaXml = doc.OuterXml;
                field.Update();
                _context.ExecuteQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public bool setFieldMultiChoice(Guid guidList, string displayName, string name, bool hidden, bool required, string[] optionsChoice, string defaultValue)
        {

            //Se puede sacar informacion de esta web: https://karinebosch.wordpress.com/my-articles/creating-fields-using-csom/
            //Informacion de las propiedades del campo https://docs.microsoft.com/es-es/sharepoint/dev/schema/field-element-field
            try
            {
                var list = _context.Web.Lists.GetById(guidList);
                if (list != null)
                {
                    string schemaXMl = "<Field id='"+Guid.NewGuid().ToString()+"'" +
                                                                 " Name='" + name + "'" +
                                                                 " StaticName='" + name + "'" +
                                                                 " DisplayName='" + displayName + "'" +
                                                                 " Type='MultiChoice'" +
                                                                 " Hidden='" + hidden.ToString().ToUpper()  + "'" +
                                                                 " Format='Dropdown'" +
                                                                 " Required='" + required.ToString().ToUpper() + "'" +
                                                                  ">";
                    if (defaultValue != null)
                        schemaXMl += "<Default>" + defaultValue + "</Default>";

                   
                    schemaXMl += "<CHOICES>";
                    foreach (string option in optionsChoice)
                    {
                        schemaXMl += "<CHOICE>" + option + "</CHOICE>";
                    }
                    schemaXMl += "</CHOICES>";
                    schemaXMl += "</Field>";
                    var field = list.Fields.AddFieldAsXml(schemaXMl, true, AddFieldOptions.DefaultValue);

                    list.Update();
                    _context.ExecuteQuery();
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool updateFieldMultiChoice(Guid guidList, Guid id, string displayName, bool? hidden, bool? required, string[] optionsChoice, string defaultValue)
        {
            try
            {
                var list = _context.Web.Lists.GetById(guidList);
                var field = list.GetFieldById(id);

                var doc = new XmlDocument();
                doc.LoadXml(field.SchemaXml);

                if (displayName != null) doc.FirstChild.Attributes["DisplayName"].Value = displayName;
                if (hidden != null) doc.FirstChild.Attributes["Hidden"].Value = hidden.Value.ToString().ToUpper();
                if (required != null) doc.FirstChild.Attributes["Required"].Value = required.Value.ToString().ToUpper();
                
                if (defaultValue != null) doc.FirstChild.ChildNodes[0].InnerXml = defaultValue;

                if (optionsChoice != null)
                {
                    doc.FirstChild.RemoveChild(doc.GetElementsByTagName("CHOICES")[0]);
                    XmlDocumentFragment xmlDocFrag = doc.CreateDocumentFragment();
                    string newNode = "<CHOICES>";
                    foreach (string option in optionsChoice)
                    {
                        newNode += "<CHOICE>" + option + "</CHOICE>";
                    }
                    newNode += "</CHOICES>";
                    xmlDocFrag.InnerXml = newNode;
                    doc.FirstChild.AppendChild(xmlDocFrag);
                }

                field.SchemaXml = doc.OuterXml;
                field.Update();
                _context.ExecuteQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public bool setFieldPicture(Guid guidList, string displayName, string name, bool required)
        {

            //Se puede sacar informacion de esta web: https://karinebosch.wordpress.com/my-articles/creating-fields-using-csom/
            //Informacion de las propiedades del campo https://docs.microsoft.com/es-es/sharepoint/dev/schema/field-element-field
            try
            {
                var list = _context.Web.Lists.GetById(guidList);
                if (list != null)
                {
                    var field = list.Fields.AddFieldAsXml("<Field id='"+Guid.NewGuid().ToString()+"'" +
                                                                 " Name='" + name + "'" +
                                                                 " StaticName='" + name + "'" +
                                                                 " DisplayName='" + displayName + "'" +
                                                                 " Type='URL'" +
                                                                 " Format='Image'" +
                                                                 " Required='" + required.ToString().ToUpper() + "'" +
                                                                 "/>"
                                                          , true, AddFieldOptions.DefaultValue);

                    list.Update();
                    _context.ExecuteQuery();
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool updateFieldPicture(Guid guidList, Guid id, string displayName, bool? required)
        {
            try
            {
                var list = _context.Web.Lists.GetById(guidList);
                var field = list.GetFieldById(id);

                var doc = new XmlDocument();
                doc.LoadXml(field.SchemaXml);

                if (displayName != null) doc.FirstChild.Attributes["DisplayName"].Value = displayName;                
                if (required != null) doc.FirstChild.Attributes["Required"].Value = required.Value.ToString().ToUpper();

                field.SchemaXml = doc.OuterXml;
                field.Update();
                _context.ExecuteQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public bool setFieldUrl(Guid guidList, string displayName, string name, bool required)
        {

            //Se puede sacar informacion de esta web: https://karinebosch.wordpress.com/my-articles/creating-fields-using-csom/
            //Informacion de las propiedades del campo https://docs.microsoft.com/es-es/sharepoint/dev/schema/field-element-field
            try
            {
                var list = _context.Web.Lists.GetById(guidList);
                if (list != null)
                {
                    var field = list.Fields.AddFieldAsXml("<Field id='"+Guid.NewGuid().ToString()+"'" +
                                                                 " Name='" + name + "'" +
                                                                 " StaticName='" + name + "'" +
                                                                 " DisplayName='" + displayName + "'" +
                                                                 " Type='URL'" +
                                                                 " Format='Hyperlink'" +
                                                                 " Required='" + required.ToString().ToUpper() + "'" +
                                                                 "/>"
                                                          , true, AddFieldOptions.DefaultValue);

                    list.Update();
                    _context.ExecuteQuery();
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool updateFieldUrl(Guid guidList, Guid id, string displayName, bool? required)
        {
            try
            {
                var list = _context.Web.Lists.GetById(guidList);
                var field = list.GetFieldById(id);

                var doc = new XmlDocument();
                doc.LoadXml(field.SchemaXml);

                if (displayName != null) doc.FirstChild.Attributes["DisplayName"].Value = displayName;
                if (required != null) doc.FirstChild.Attributes["Required"].Value = required.Value.ToString().ToUpper();

                field.SchemaXml = doc.OuterXml;
                field.Update();
                _context.ExecuteQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public bool setFieldUser(Guid guidList, string displayName, string name, bool hidden, bool required)
        {

            //Se puede sacar informacion de esta web: https://karinebosch.wordpress.com/my-articles/creating-fields-using-csom/
            //Informacion de las propiedades del campo https://docs.microsoft.com/es-es/sharepoint/dev/schema/field-element-field
            try
            {
                var list = _context.Web.Lists.GetById(guidList);
                if (list != null)
                {
                    var field = list.Fields.AddFieldAsXml("<Field id='" + Guid.NewGuid().ToString() + "'" +
                                                                 " Name='" + name + "'" +
                                                                 " StaticName='" + name + "'" +
                                                                 " DisplayName='" + displayName + "'" +
                                                                 " Type='User'" +                                                                 
                                                                 " Hidden='" + hidden.ToString().ToUpper()  + "'" +
                                                                 " Required='" + required.ToString().ToUpper() + "'" +
                                                                 "/>"
                                                          , true, AddFieldOptions.DefaultValue);

                    list.Update();
                    _context.ExecuteQuery();
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool updateFieldUser(Guid guidList, Guid id, string displayName, bool? hidden, bool? required)
        {
            try
            {
                var list = _context.Web.Lists.GetById(guidList);
                var field = list.GetFieldById(id);

                var doc = new XmlDocument();
                doc.LoadXml(field.SchemaXml);

                if (displayName != null) doc.FirstChild.Attributes["DisplayName"].Value = displayName;
                if (required != null) doc.FirstChild.Attributes["Required"].Value = required.Value.ToString().ToUpper();
                if (hidden != null) doc.FirstChild.Attributes["Hidden"].Value = hidden.Value.ToString().ToUpper();

                field.SchemaXml = doc.OuterXml;
                field.Update();
                _context.ExecuteQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public bool setField(Guid guidList, string schemaXMl)
        {

            //Se puede sacar informacion de esta web: https://karinebosch.wordpress.com/my-articles/creating-fields-using-csom/
            //Informacion de las propiedades del campo https://docs.microsoft.com/es-es/sharepoint/dev/schema/field-element-field
            try
            {
                var list = _context.Web.Lists.GetById(guidList);
                if (list != null)
                {
                    var field = list.Fields.AddFieldAsXml(schemaXMl, true, AddFieldOptions.DefaultValue);
                    
                    list.Update();
                    _context.ExecuteQuery();
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool updateField(Guid guidList, Guid id, string schemaXMl)
        {
            try
            {
                var list = _context.Web.Lists.GetById(guidList);
                var field = list.GetFieldById(id);

                field.SchemaXml = schemaXMl;
                field.Update();
                _context.ExecuteQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public List<FieldLista> getAll(Guid guidList)
        {            
            try
            {
                var list = _context.Web.Lists.GetById(guidList);
                if (list != null)
                {
                    var fields = list.Fields;

                    _context.Load(fields);
                    _context.ExecuteQuery();

                    return FieldListaMapper.Map(fields);
                }
                else
                {
                    return new List<FieldLista>(); ;
                }

            }
            catch (Exception ex)
            {
                return new List<FieldLista>(); ;
            }
        }
        public FieldLista getById(Guid guidList, Guid id)
        {
            try
            {
                var list = _context.Web.Lists.GetById(guidList);
                var field = list.GetFieldById(id);                              
                return FieldListaMapper.Map(field);

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool deleteField(Guid guidList, Guid id)
        {
            try
            {
                var list = _context.Web.Lists.GetById(guidList);
                var field = list.GetFieldById(id);

                field.DeleteObject();
                list.Update();
                _context.ExecuteQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }


    }
}
