using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IEMS
{
    static class GetData
    {
        public static List<string> SelectAll(Enums.IDType IDType)
        {
            List<string> ListOfManufactuer = new List<string>();
            DataTable dt = new DataTable();
            string SQLCommand = string.Empty;
            switch (IDType)
            {
                case Enums.IDType.CLNT:
                    SQLCommand = "Select ID from client";
                    break;
                case Enums.IDType.INVO:
                    SQLCommand = "Select concat(name,'(',ID,')') manu from invoice";
                    break;
                case Enums.IDType.MANF:
                    SQLCommand = "Select concat(name,'(',ID,')') manu from Manufacturer";
                    break;
                case Enums.IDType.PROD:
                    SQLCommand = "Select concat(name,'(',ID,')') manu from product";
                    break;
                case Enums.IDType.PUOR:
                    SQLCommand = "Select ID manu from OrderReceived";
                    break;
                case Enums.IDType.SUPP:
                    SQLCommand = "Select concat(name,'(',ID,')') manu from Supplier";
                    break;
                case Enums.IDType.USER:
                    SQLCommand = "Select concat(username,'(',ID,')') manu from User";
                    break;
                case Enums.IDType.CONT:
                    SQLCommand = "Select concat(name,'(',ID,')') manu from Contacts";
                    break;
            }
            if (db.SQLQuery(ref dt, SQLCommand))
            {
                foreach (DataRow dr in dt.Rows)
                {
                    ListOfManufactuer.Add(dr[0].ToString());
                }
            }
            return ListOfManufactuer;
        }
        public static DataTable Select(Enums.IDType idType, string ID)
        {
            DataTable dt = new DataTable();
            string SQLCommand = string.Empty;
            switch (idType)
            {
                case Enums.IDType.CLNT:
                    SQLCommand = "select* from client where id = '"+ID+"'";
                    break;
                case Enums.IDType.INVO:
                    SQLCommand = "Select * from invoice where id = '" + ID + "'";
                    break;
                case Enums.IDType.MANF:
                    SQLCommand = "Select * from Manufacturer where id = '" + ID + "'";
                    break;
                case Enums.IDType.PROD:
                    SQLCommand = "Select * from product where id = '" + ID + "'";
                    break;
                case Enums.IDType.PUOR:
                    SQLCommand = "Select * from OrderReceived where id = '" + ID + "'";
                    break;
                case Enums.IDType.SUPP:
                    SQLCommand = "Select * from Supplier where id = '" + ID + "'";
                    break;
                case Enums.IDType.USER:
                    SQLCommand = "Select username,role from User where id = '" + ID + "'";
                    break;
                case Enums.IDType.CONT:
                    SQLCommand = "Select * from Contacts where id = '" + ID + "'";
                    break;
            }
            db.SQLQuery(ref dt, SQLCommand);
            return dt;
        }
       
    }
}
