using System;
using System.Data.SqlClient;
using System.Configuration;
using Newtonsoft.Json;
using System.IO;

namespace DataContaxtClassLibrary.DataUtility
{
    public class ConnectionStrings
    {
        public string DefaultConnection { get; set; }
    }
    public class AppSettings
    {
        public ConnectionStrings ConnectionStrings { get; set; }
    }

    public class Connection
    {
        /// <summary>
        /// return connection string.
        /// </summary>
        public string connectionString
        {
            get
            {

                //string ConString = ConfigurationManager.ConnectionStrings["connectionString"].ToString();
                var text = File.ReadAllText("appsettings.json");
                //get data settings from the JSON file  
                AppSettings settings = JsonConvert.DeserializeObject<AppSettings>(text);

                if (string.IsNullOrWhiteSpace(settings.ConnectionStrings.DefaultConnection))
                {
                    throw new ApplicationException("connection string not found in web.config");
                }

                return settings.ConnectionStrings.DefaultConnection;
            }
        }
        /// <summary>
        /// return new sql connection.
        /// </summary>        
        public SqlConnection getConnection
        {
            get
            {
                return new SqlConnection(connectionString);
            }
        }
    }
}
