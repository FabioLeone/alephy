using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;

namespace SIAO.SRV.DAL
{
    public class DbHelper
    {
        internal static object GetParameter(DbCommand cmdUsers, System.Data.DbType dbType, string FieldName, object FieldValue)
        {
            DbParameter parameter = cmdUsers.CreateParameter();
            parameter.ParameterName = FieldName;
            parameter.Value = FieldValue;
            parameter.DbType = dbType;

            return parameter;
        }
    }
}
