using DataContaxtClassLibrary.DataUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelsClassLibrary;
using System.Data.SqlClient;
using DataContaxtClassLibrary.Utility;

namespace DataContaxtClassLibrary
{
    public class APIAuthDAL
    {
        #region
        DataFunctions objDataFunctions = null;
        //DataTable objDataTable = null;
        DataSet objDataSet = null;
        string _commandText = string.Empty;
        //static string CommandText;
        #endregion

        public APIAuthDAL()
        {
            objDataFunctions = new DataFunctions();
        }
        public Messages AuthenticateUser(APIAuthMDL ObjAPIAuthMDL)
        {
            Messages objMessages = new Messages();
            try
            {
                var test = ClsCrypto.Decrypt(ObjAPIAuthMDL.Password);
                List<SqlParameter> parms = new List<SqlParameter>()
                {
                    new SqlParameter("@cUserName",ObjAPIAuthMDL.UserName),
                    new SqlParameter("@cPassword",ClsCrypto.Encrypt(ObjAPIAuthMDL.Password))
                };
                CheckParameters.ConvertNullToDBNull(parms);
                _commandText = "[dbo].[USP_AuthenticateUser]";
                objDataSet = (DataSet)objDataFunctions.getQueryResult(_commandText, DataReturnType.DataSet, parms);

                if (objDataSet.Tables[0].Rows.Count > 0)
                {
                    objMessages.Message_Id = objDataSet.Tables[0].Rows[0].Field<int>("Message_Id");
                    objMessages.Message = objDataSet.Tables[0].Rows[0].Field<string>("Message");
                }
            }
            catch (Exception ex)
            {
                var objBase = System.Reflection.MethodBase.GetCurrentMethod();
                LockErrors.SetError("Login Controller", "Account", "Login", "Login DAL > AuthenticateUser > Catch Block", ex.Message.ToString(), "Exception", "Exception During Login: " + ex.Message.ToString());
                objMessages.Message_Id = 0;
                objMessages.Message = "Authentication Failed.";
            }
            return objMessages;
        }
    }
}
