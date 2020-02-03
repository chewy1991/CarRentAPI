using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentApi.Model
{
    public class Customer:IDBTable
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Adress { get; set; }
        public string EMailAdress { get; set; }
        public string Telephonenumber { get; set; }
    }
}
