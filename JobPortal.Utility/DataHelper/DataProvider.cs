using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Microsoft.Data.SqlClient;

namespace JobPortal.Utility
{
    public class DataProvider
    {
        public static SqlParameter GetDateSqlParameter(string parameterName, DateTime? value, bool IsOutput = false)
        {
            var parameter = value != null ? new SqlParameter(parameterName, value) 
                                          : new SqlParameter(parameterName, System.Data.SqlDbType.Date);
            if (IsOutput)
                parameter.Direction = ParameterDirection.Output;
            return parameter;
        }
        public static SqlParameter GetStringSqlParameter(string parameterName, string value, bool IsOutput = false)
        {
            var parameter = value != null ? new SqlParameter(parameterName, value)
                                          : new SqlParameter(parameterName, DBNull.Value);
            if (IsOutput)
                parameter.Direction = ParameterDirection.Output;
            return parameter;
        }
        public static SqlParameter GetStringSqlParam_DefaultNull(string parameterName, string value, bool IsOutput = false)
        {
            var parameter = !(string.IsNullOrEmpty(value)) ?
                                        new SqlParameter(parameterName, value) :
                                        new SqlParameter(parameterName, DBNull.Value);

            if (IsOutput)
                parameter.Direction = ParameterDirection.Output;

            return parameter;
        }


        public static SqlParameter GetDecimalSqlParameter(string parameterName, decimal? value, bool IsOutput = false)
        {
            var parameter = value.HasValue ?
                                        new SqlParameter(parameterName, value) :
                                        new SqlParameter(parameterName, SqlDbType.Decimal);

            if (IsOutput)
                parameter.Direction = ParameterDirection.Output;
            return parameter;
        }

        public static SqlParameter GetIntSqlParameter(string parameterName, int? value, bool IsOutput = false)
        {
            var parameter = value.HasValue ?
                                        new SqlParameter(parameterName, value) :
                                        new SqlParameter(parameterName, SqlDbType.Int);

            if (IsOutput)
                parameter.Direction = ParameterDirection.Output;
            return parameter;
        }

        public static SqlParameter GetIntSqlParam_DefaultNull(string parameterName, int? value, bool IsOutput = false)
        {
            var parameter = value.HasValue && value != 0 ?
                                        new SqlParameter(parameterName, value) :
                                        new SqlParameter(parameterName, DBNull.Value);

            if (IsOutput)
                parameter.Direction = ParameterDirection.Output;
            return parameter;
        }

        public static SqlParameter GetBoolSqlParameter(string parameterName, bool? value, bool IsOutput = false)
        {
            var parameter = value.HasValue ?
                                        new SqlParameter(parameterName, value) :
                                        new SqlParameter(parameterName, DBNull.Value);

            if (IsOutput)
                parameter.Direction = ParameterDirection.Output;

            return parameter;
        }
    }
}
