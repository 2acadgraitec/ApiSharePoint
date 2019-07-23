using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint.Client;

namespace ApiSharePointGraitec.Models
{
    public class FieldLista
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Type { get; set; }
        public bool Required { get; set; }
        public bool Hidden { get; set; }
        public string StaticName { get; set; }

    }
}
