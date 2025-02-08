using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AppCredenciales.Data
{
    public class EmployeeData
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string NSS { get; set; }
        public string CURP { get; set; }
        public string ImageBase64 { get; set; }
        public string JobTitle { get; set; }
        public string Position { get; set; }

        // Constructor por defecto para la serialización
        public EmployeeData() { }

        // Constructor con parámetros
        [JsonConstructor]
        public EmployeeData(int id, string name, string nss, string curp,string position , string imageBase64)
        {
            ID = id;
            Name = name;
            NSS = nss;
            CURP = curp;
            Position = position;
            ImageBase64 = imageBase64;
        }
    }
}
