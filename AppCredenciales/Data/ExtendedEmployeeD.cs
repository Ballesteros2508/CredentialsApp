using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AppCredenciales.Data
{
    internal class ExtendedEmployeeD : EmployeeData
    {
        public string Department { get; set; }
        public string Date { get; set; }
        public string RFC { get; set; }

        // Constructor sin parámetros para la deserialización automática
        public ExtendedEmployeeD() { }

        // Constructor con parámetros
        [JsonConstructor]
        public ExtendedEmployeeD(int id, string name, string nss, string curp, string department, string date, string rfc, string position, string imageBase64)
            : base(id, name, nss, curp, position ,imageBase64)
        {
            Department = department;
            Date = date;
            RFC = rfc;
        }
    }
}




