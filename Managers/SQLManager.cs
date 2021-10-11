using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XMLToSQL.Managers
{
    class SQLManager
    {
        private MainForm mainForm;

        private string server;
        private string id;
        private string password;

        public SQLManager(MainForm mainForm)
        {
            this.mainForm = mainForm;
            this.server = null;
            this.id = null;
            this.password = null;
        }

        /// <summary>
        /// 嘗試連線到SQL以驗證登入，預設sever為localhost
        /// </summary>
        /// <param name="id"></param>
        /// <param name="password"></param>
        /// <param name="server"></param>
        /// <returns></returns>
        public int Login(string id, string password, string server = "(local)", string database = null)
        {
            Console.WriteLine("Login...");
            string conStr;
            if (database == null)
                conStr = "Server = " + server + ";User Id = " + id + ";Password = " + password + ";";
            else
                conStr = "Server = " + server + ";Database = " + database + ";User Id = " + id + ";Password = " + password + ";";
            try
            {
                using (SqlConnection conn = new SqlConnection(conStr))
                {
                    conn.Open();
                    conn.Close();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Number + " msg: " + ex.Message);
                switch(ex.Number)
                {
                    case 53:        //sever error
                        return -2;
                    case 18456:     //id password error
                        return -3;
                    default:
                        MessageBox.Show("Login failed:\nNumber: " + ex.Number + " Msg: " + ex.Message, "Login");
                        return -1;
                }
            }
            this.id = id;
            this.password = password;
            this.server = server;
            Console.WriteLine("Login success");
            return 0;
        }

        /// <summary>
        /// 將datatable insert到database的table內
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="database"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public int InsertData(DataTable dataTable, string database, string tableName)
        {
            if (server == null || id == null || password == null)
                return -1;
            string fail_log_file = $"{mainForm.DestinationDirectory}/Logs/{MainForm.FailLogFile}";
            try
            {
                Console.WriteLine("Inserting data...");
                string conStr = "Server = " + server + ";Database = " + database + ";User Id = " + id + ";Password = " + password + ";";
                using (SqlConnection conn = new SqlConnection(conStr))
                {
                    conn.Open();
                    SqlTransaction trans = conn.BeginTransaction();
                    //SqlBulkCopy批次處理新增 沒有檢驗比對處理
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(conn, SqlBulkCopyOptions.KeepIdentity, trans))
                    {
                        bulkCopy.DestinationTableName = tableName;

                        try
                        {
                            bulkCopy.WriteToServer(dataTable);
                            trans.Commit();
                            Console.WriteLine("Insert done");
                        }
                        catch (Exception ex)
                        {
                            trans.Rollback();
                            MessageBox.Show("Insert failed:\n" + ex.Message, "Insert");
                            string[] logMessages = { "Error: Insert error in Bulkcopy", "Msg: " + ex.Message };
                            MainForm.Log(logMessages, fail_log_file);
                            return -1;
                        }
                    }

                }
            }
            catch(SqlException ex)
            {
                Console.WriteLine("Number: " + ex.Number + " Msg: " + ex.Message);
                switch (ex.Number)
                {
                    case 53:        //sever error
                        return -2;
                    case 18456:     //id password error
                        return -3;
                    case 4060:     //database error
                        return -4;
                    default:
                        MessageBox.Show("Insert failed:\nNumber: " + ex.Number + " Msg: " + ex.Message, "Insert");
                        string[] logMessages = { "Error: Insert error", "Msg: " + ex.Message};
                        MainForm.Log(logMessages, fail_log_file);
                        return -1;
                }
            }
            return 0;
        }
    }
}
