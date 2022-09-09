using DataContaxtClassLibrary.DataUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContaxtClassLibrary
{
    public class LockErrors
    {
        #region
        static DataFunctions objDataFunctions = null;
        DataSet objDataSet = null;
        static string _commandText = string.Empty;
        #endregion

        public LockErrors()
        {
            objDataFunctions = new DataFunctions();
        }
        public static void SetError(string name, string controller, string action, string Source, string message, string type, string Remarks)
        {
            try
            {
                _commandText = "[dbo].[USP_LogApplicationError]";
                List<SqlParameter> paramList = new List<SqlParameter>();

                SqlParameter objSqlParameter = new SqlParameter("@cSource", SqlDbType.VarChar);
                objSqlParameter.Value = Source;
                paramList.Add(objSqlParameter);

                objSqlParameter = new SqlParameter("@cAssemblyName", SqlDbType.VarChar);
                objSqlParameter.Value = name;
                paramList.Add(objSqlParameter);

                objSqlParameter = new SqlParameter("@cClassName", SqlDbType.VarChar);
                objSqlParameter.Value = controller;
                paramList.Add(objSqlParameter);

                objSqlParameter = new SqlParameter("@cMethodName", SqlDbType.VarChar);
                objSqlParameter.Value = action;
                paramList.Add(objSqlParameter);

                objSqlParameter = new SqlParameter("@cErrorMessage", SqlDbType.VarChar);
                objSqlParameter.Value = message;
                paramList.Add(objSqlParameter);

                objSqlParameter = new SqlParameter("@cErrorType", SqlDbType.VarChar);
                objSqlParameter.Value = type;
                paramList.Add(objSqlParameter);

                objSqlParameter = new SqlParameter("@cRemarks", SqlDbType.VarChar);
                objSqlParameter.Value = Remarks;
                paramList.Add(objSqlParameter);

                objDataFunctions.executeCommand(_commandText, paramList);
            }
            catch { }
        }

    }
}
