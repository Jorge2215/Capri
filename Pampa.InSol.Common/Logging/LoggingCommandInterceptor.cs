using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;
using System.Globalization;
using System.Text;

namespace Pampa.InSol.Common.Logging
{
    public class LoggingCommandInterceptor : IDbCommandInterceptor
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private static IDictionary<DbType, string> dbTypeToSqlDbTypeString = new Dictionary<DbType, string>
        {
            { DbType.AnsiString, "VarChar" },
            { DbType.Binary, "VarBinary" },
            { DbType.Byte, "TinyInt" },
            { DbType.Boolean, "Bit" },
            { DbType.Currency, "Money" },
            { DbType.Date, "Date" },
            { DbType.DateTime, "DateTime" },
            { DbType.Decimal, "Decimal" },
            { DbType.Double, "Float" },
            { DbType.Guid, "UniqueIdentifier" },
            { DbType.Int16, "SmallInt" },
            { DbType.Int32, "Int" },
            { DbType.Int64, "BigInt" },
            { DbType.Object, "Variant" },
            { DbType.SByte, "<<SByte is not supported>>" },
            { DbType.Single, "Real" },
            { DbType.String, "NVarChar" },
            { DbType.Time, "Time" },
            { DbType.UInt16, "<<UInt16 is not supported>>" },
            { DbType.UInt32, "<<UInt32 is not supported>>" },
            { DbType.UInt64, "<<UInt64 is not supported>>" },
            { DbType.VarNumeric, "<<VarNumeric is not supported>>" },
            { DbType.AnsiStringFixedLength, "Char" },
            { DbType.StringFixedLength, "NChar" },
            { DbType.Xml, "<<UInt64 is not supported>>" },
            { DbType.DateTime2, "<<DateTime2 is not supported>>" },
            { DbType.DateTimeOffset, "DateTimeOffset" },
        };

        public void NonQueryExecuting(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
        }

        public void NonQueryExecuted(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            Log(command);
        }

        public void ReaderExecuting(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
        }

        public void ReaderExecuted(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            Log(command);
        }

        public void ScalarExecuting(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
        }

        public void ScalarExecuted(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            Log(command);
        }

        private static void Log(DbCommand command)
        {
            var loggingCommandInterceptor = ConfigurationManager.AppSettings["LoggingCommandInterceptor"];

            if (string.IsNullOrWhiteSpace(loggingCommandInterceptor) || loggingCommandInterceptor != "1")
            {
                return;
            }

            var textToLog = new StringBuilder();

            foreach (DbParameter parameter in command.Parameters)
            {
                textToLog.AppendFormat("{0}DECLARE @{1} {2}", Environment.NewLine, parameter.ParameterName, GetSqlType(parameter.DbType));
                textToLog.AppendFormat("{0}SET @{1} = {2}", Environment.NewLine, parameter.ParameterName, parameter.Value);
            }

            textToLog.AppendLine(command.CommandText);

            textToLog.Append(Environment.NewLine);

            logger.ConditionalDebug(CultureInfo.CurrentCulture, "DB Command Executed: \r\n{0}", textToLog.ToString());
        }

        private static string GetSqlType(DbType dbType)
        {
            if (!dbTypeToSqlDbTypeString.ContainsKey(dbType))
            {
                return string.Format("<<{0} is not supported>>", dbType.ToString());
            }

            return dbTypeToSqlDbTypeString[dbType];
        }
    }
}
