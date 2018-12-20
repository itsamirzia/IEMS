using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IEMS
{
    class GenerateID
    {
        public static string Get(Enums.IDType idType)
        {

            var chars = "1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, 5)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            
            GenerateID dig = new GenerateID();
            if (dig.Available(result, idType))
                return Get(idType);
            else
                return idType.ToString() + result;

        }
        public bool Available(string id, Enums.IDType IDType)
        {
            string SQLCommand = string.Empty;
            DataTable dt = new DataTable();
            switch (IDType)
            {
                case Enums.IDType.CLNT:
                    SQLCommand = "select * from client where id = '"+IDType.ToString()+id+"'";
                    break;
                case Enums.IDType.INVO:
                    SQLCommand = "select * from invoice where id = '" + IDType.ToString() + id + "'";
                    break;
                case Enums.IDType.MANF:
                    SQLCommand = "select * from manufacturer where id = '" + IDType.ToString() + id + "'";
                    break;
                case Enums.IDType.PROD:
                    SQLCommand = "select * from product where id = '" + IDType.ToString() + id + "'";
                    break;
                case Enums.IDType.PUOR:
                    SQLCommand = "select * from purchaseorder where id = '" + IDType.ToString() + id + "'";
                    break;
                case Enums.IDType.SUPP:
                    SQLCommand = "select * from supplier where id = '" + IDType.ToString() + id + "'";
                    break;
                case Enums.IDType.USER:
                    SQLCommand = "select * from user where id = '" + IDType.ToString() + id + "'";
                    break;
                case Enums.IDType.CONT:
                    SQLCommand = "select * from contact where id = '" + IDType.ToString() + id + "'";
                    break;
            }
            if (db.SQLQuery(ref dt, SQLCommand))
            {
                if (dt.Rows.Count>0)
                    return true;
                else
                    return false;
            }
            else
            {
                return false;
            }
        }
    }
}
