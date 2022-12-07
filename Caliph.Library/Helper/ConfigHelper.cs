using System;
using System.Configuration;

namespace Caliph.Library.Helper
{
    public class ConfigHelper
    {
        #region DB Connection
        /// <summary>
        /// DONT CHANGED THIS
        /// </summary>
        private static string DB_SecretKey = "zD1#z8Nsm$wRb";

        /// <summary>
        /// MSSQL connection string
        /// Data Source=51.79.218.65\SQLEXPRESS, 1433;Initial Catalog=CaliphDB;Persist Security Info=True;User ID=sa;Password=Kj2#Yah#uB;
        /// </summary>        
        public static string MSSQL_ConStr { get; set; } = HashHelper.DecryptPwd(ConfigurationManager.ConnectionStrings["MSSQL_ConStr"].ToString(), DB_SecretKey);
        #endregion

        #region api authentication
        public static readonly string secret = ConfigurationManager.AppSettings["authentication:Secret"];
        public static readonly string issuer = ConfigurationManager.AppSettings["authentication:Issuer"];
        public static readonly string audience = ConfigurationManager.AppSettings["authentication:Audience"];
        #endregion

        #region token
        public static readonly string token_username = ConfigurationManager.AppSettings["token_username"];
        public static readonly string token_pw = ConfigurationManager.AppSettings["token_pw"];
        #endregion
        public static int TokenExpiryInSecond { get; set; } = Convert.ToInt16(ConfigurationManager.AppSettings["TokenExpiryInSecond"]);

        public static readonly long DefaultPageSize = 2147483647;
        public static readonly long DefaultPageNo = 1;
    }
}
