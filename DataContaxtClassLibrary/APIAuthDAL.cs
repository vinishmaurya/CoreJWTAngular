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
        public Messages AuthenticateUser(APIAuthMDL ObjAPIAuthMDL,out string Out_UserName)
        {
            Out_UserName = "";
            Messages objMessages = new Messages();
            try
            {
                var test = ClsCrypto.Decrypt(string.IsNullOrEmpty(ObjAPIAuthMDL.Password) ? "" : ObjAPIAuthMDL.Password);
                List<SqlParameter> parms = new List<SqlParameter>()
                {
                    new SqlParameter("@cUserName",ObjAPIAuthMDL.UserName),
                    new SqlParameter("@cPassword",ClsCrypto.Encrypt(string.IsNullOrEmpty(ObjAPIAuthMDL.Password) ? "" : ObjAPIAuthMDL.Password)),
                    new SqlParameter("@cGrantType",ObjAPIAuthMDL.GrantType),
                    new SqlParameter("@cRefreshToken",ObjAPIAuthMDL.RefreshToken),
                    new SqlParameter("@iRefreshTokenExpiryTimeInDays",ObjAPIAuthMDL.RefreshTokenExpiryTimeInDays)
                };
                CheckParameters.ConvertNullToDBNull(parms);
                _commandText = "[dbo].[USP_SvcAuthenticateAPIUser]";
                objDataSet = (DataSet)objDataFunctions.getQueryResult(_commandText, DataReturnType.DataSet, parms);

                if (objDataSet.Tables[0].Rows.Count > 0)
                {
                    objMessages.Message_Id = objDataSet.Tables[0].Rows[0].Field<int>("Message_Id");
                    objMessages.Message = objDataSet.Tables[0].Rows[0].Field<string>("Message");
                    if (objMessages.Message_Id == 1)
                    {
                        Out_UserName = objDataSet.Tables[1].Rows[0].Field<string>("UserName");
                    }
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

        public Messages SaveAuthenticatedUserDate(TokenInfo obj, string UserName)
        {
            Messages objMessages = new Messages();
            try
            {
                List<SqlParameter> parms = new List<SqlParameter>()
                {
                    new SqlParameter("@cAccessToken",obj.AccessToken),
                    new SqlParameter("@cExpiryDatetime",obj.ExpiryDatetime),
                    new SqlParameter("@cTokenGeneratedDatetime",obj.TokenGeneratedDatetime),
                    new SqlParameter("@cRefreshToken",obj.RefreshTokenInfo.RefreshToken),
                    new SqlParameter("@cRefreshTokenExpiryDatetime",obj.RefreshTokenInfo.ExpiryDatetime),
                    new SqlParameter("@cUserName",UserName)
                };
                CheckParameters.ConvertNullToDBNull(parms);
                _commandText = "[dbo].[USP_SvcSaveAuthenticatedUserDate]";
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
