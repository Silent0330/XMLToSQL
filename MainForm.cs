using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using XMLToSQL.Managers;
using XMLToSQL.SubForms;

namespace XMLToSQL
{
    public partial class MainForm : Form
    {
        private DataManager dataMgr;
        private SQLManager sqlMgr;

        private bool reading;
        Thread readProThread;
        private bool inserting;
        private bool fullRunning;
        Thread fullRunProThread;

        private bool isDataTable;

        private string serverName;
        public string ServerName { get => serverName; set => serverName = value; }

        private string databaseName;
        public string DatabaseName { get => databaseName; set => databaseName = value; }

        private string tableName;
        public string TableName { get => tableName; set => tableName = value; }

        private string sourceDirectory;
        public string SourceDirectory { get => sourceDirectory; }

        private string destinationDirectory;
        public string DestinationDirectory { get => destinationDirectory; set => destinationDirectory = value; }

        private int progressValue;
        public int ProgressValue { get => progressValue; }

        private int progressMaxValue;
        public int ProgressMaxValue { get => progressMaxValue; }

        public static readonly string LogFile = "log.txt";
        public static readonly string FailLogFile = "fail_log.txt";
        public static readonly string ErrorLogFile = "error_log.txt";
        public static readonly string ConfigDirectory = "Config";

        private int maxRowsToHandle;
        private int success_count = 0;
        private int failed_count = 0;
        private int error_count = 0;
        private List<string> readDoneFiles;
        private List<string> readDoneErrorFiles;

        public MainForm()
        {
            InitializeComponent();

            dataMgr = new DataManager(this);
            sqlMgr = new SQLManager(this);
            readDoneFiles = new List<string>();
            readDoneErrorFiles = new List<string>();

            isDataTable = true;
            reading = false;
            readProThread = null;
            inserting = false;
            fullRunning = false;
            fullRunProThread = null;

            igFormWidth = this.Width;
            igFormHeight = this.Height;
            InitConTag(this);

            maxRowsToHandle = 100000;

            progressValue = 0;
            progressMaxValue = 0;


            dataGridView.DataSource = dataMgr.DataTable;
        }

        public static void Log(string[] logMessages, string fileName)
        {
            Console.Write("\nLog Entry : ");
            Console.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}");
            Console.WriteLine("  Contents:");
            foreach (var message in logMessages)
            {
                Console.WriteLine($"  {message}");
            }
            Console.WriteLine("-------------------------------");
            string dir = Path.GetDirectoryName(fileName);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            using (StreamWriter w = File.AppendText(fileName))
            {
                w.Write("\nLog Entry : ");
                w.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}");
                w.WriteLine("  Contents:");
                foreach (var message in logMessages)
                {
                    w.WriteLine($"  {message}");
                }
                w.WriteLine("-------------------------------");
            }
        }

        public int Login(string id, string password, string server = "(local)", string database = null)
        {
            int result = sqlMgr.Login(id, password, server, database);
            if (result == 0)
            {
                ServerName = server;
                DatabaseName = database;
                UserLabel.Text = id;
            }
            return result;
        }

        private bool checkFreeState()
        {
            if (reading || readProThread != null)
            {
                MessageBox.Show("Process is reading now.\nWait for the process done", "Insert");
                return false;
            }
            else if (inserting)
            {
                MessageBox.Show("Process is inserting now.\nWait for the process done", "Insert");
                return false;
            }
            else if (fullRunning || fullRunProThread != null)
            {
                MessageBox.Show("Process is running now.\nWait for the process done", "Insert");
                return false;
            }

            return true;
        }

        /// <summary>
        /// return 0 = 讀完, -1 = error, -2 = table已滿, -3 = 不同的type且table不為空, -4 = 有errorTable存在, -5 = 第一個檔案非存在的type
        /// </summary>
        /// <returns></returns>
        private int readXML(IEnumerable<string> files)
        {
            string error_log_file = $"{destinationDirectory}/Logs/{ErrorLogFile}";
            int fun_result;
            string sourceDirectory = srcTextBox.Text;
            string current_file = "unknow";
            try
            {
                if (files.Count() == 0)
                {
                    return 0;
                }

                /*
                 分析xml類型，共5種: MM_1_1, MM_10_1, SD_1_1, SD_2_1, SD_8_1
                 */
                string otherDirectory = destinationDirectory + "/Other";
                if (!Directory.Exists(otherDirectory))
                {
                    Directory.CreateDirectory(otherDirectory);
                }
                #region 第一個xml檔案名稱分析
                string firstFile = Path.GetFileNameWithoutExtension(files.First());
                current_file = firstFile;
                int type = dataMgr.FindType(firstFile);
                if (type == -1)
                {
                    firstFile = Path.GetFileName(firstFile);
                    File.Move(firstFile, otherDirectory + "/" + firstFile);
                    string[] logMessages = { "檔案: " + firstFile, " Error: Not found type" };
                    Log(logMessages, error_log_file);
                    failed_count++;
                    return -5;
                }
                #endregion

                string readDoneDirectory = $"{destinationDirectory}/{dataMgr.DataConfigs[type].TypeName}/Read_Done";
                string failDirectory = $"{destinationDirectory}/{dataMgr.DataConfigs[type].TypeName}/Fail";
                if (!Directory.Exists(readDoneDirectory))
                {
                    Directory.CreateDirectory(readDoneDirectory);
                }
                if (!Directory.Exists(failDirectory))
                {
                    Directory.CreateDirectory(failDirectory);
                }

                if (type != dataMgr.TableType)
                {
                    if (dataMgr.DataTable.Rows.Count > 0)
                    {
                        return -3;
                    }
                    dataMgr.TableType = type;
                }

                foreach (string currentFile in files)
                {
                    current_file = currentFile;
                    string currentFileName = Path.GetFileName(currentFile);
                    #region xml檔案名稱分析，type不同時中斷
                    type = dataMgr.FindType(currentFile);
                    if (type == -1)
                    {
                        File.Move(currentFile, otherDirectory + "/" + currentFileName);
                        failed_count++;
                        return -3;
                    }
                    else if (type != dataMgr.TableType)
                    {
                        return -3;
                    }
                    #endregion

                    DataTable tempTable = dataMgr.Create_Table();
                    fun_result = dataMgr.ReadXML(currentFile, tempTable);
                    if (fun_result == 0)
                    {
                        if (dataMgr.DataTable.Rows.Count > 0 && dataMgr.DataTable.Rows.Count + tempTable.Rows.Count >= maxRowsToHandle)
                        {
                            return -2;
                        }

                        try
                        {
                            dataMgr.DataTable.Merge(tempTable);
                        }
                        catch (Exception ex)
                        {
                            string[] logMessages = { "檔案: " + currentFile, " Error: Table merge fail" };
                            Log(logMessages, error_log_file);
                            File.Move(currentFile, failDirectory + "/" + currentFileName);
                            failed_count++;
                            continue;
                        }
                        readDoneFiles.Add(currentFile);
                        File.Move(currentFile, readDoneDirectory + "/" + currentFileName);
                        success_count++;
                    }
                    else if (fun_result == 1 && !dataMgr.DataConfigs[dataMgr.TableType].ErrorTableName.Equals(""))
                    {
                        try
                        {
                            dataMgr.ErrorDataTable.Merge(tempTable);
                        }
                        catch (Exception ex)
                        {
                            string[] logMessages = { "檔案: " + currentFile, " Error: Error table merge fail" };
                            Log(logMessages, error_log_file);
                            File.Move(currentFile, failDirectory + "/" + currentFileName);
                            failed_count++;
                            continue;
                        }
                        readDoneErrorFiles.Add(currentFile);
                        File.Move(currentFile, readDoneDirectory + "/" + currentFileName);
                        error_count++;
                        return -4;
                    }
                    else
                    {
                        File.Move(currentFile, failDirectory + "/" + currentFileName);
                        failed_count++;
                    }
                }
            }
            catch (Exception ex)
            {
                string[] logMessages = { "檔案: " + current_file, " Error: ReadXML fail" , "Msg: " + ex.Message };
                MainForm.Log(logMessages, error_log_file);
                return -1;
            }
            return 0;
        }

        private int insertData(bool error = false)
        {
            int result;
            if (serverName == null)
            {
                MessageBox.Show("Error:\nNot login yet!", "Insert");
                return -1;
            }
            if (databaseName == null)
            {
                MessageBox.Show("Error:\nNot found database!", "Insert");
                return -1;
            }

            if (!error)
            {
                tableName = dataMgr.DataConfigs[dataMgr.TableType].TableName;
                result = sqlMgr.InsertData(dataMgr.DataTable, DatabaseName, tableName);
            }
            else
            {
                tableName = dataMgr.DataConfigs[dataMgr.TableType].ErrorTableName;
                result = sqlMgr.InsertData(dataMgr.ErrorDataTable, DatabaseName, tableName);
            }
            switch (result)
            {
                case 0:
                    break;
                case -1:
                    return -1;
                case -2:
                    MessageBox.Show("Insert failed:\nNot found sever", "Insert");
                    return -1;
                case -3:
                    MessageBox.Show("Insert failed:\nID or password error", "Insert");
                    return -1;
                case -4:
                    MessageBox.Show("Insert failed:\nNOt found database", "Insert");
                    return -1;
            }
            return 0;
        }

        private void readingProcess()
        {
            string log_file = $"{destinationDirectory}/Logs/{LogFile}";
            DateTime start = DateTime.Now;
            var files = Directory.EnumerateFiles(sourceDirectory, "*.xml");
            success_count = 0;
            failed_count = 0;
            error_count = 0;
            progressMaxValue = files.Count();

            int row_count = 0;
            try
            {
                while (reading)
                {
                    files = Directory.EnumerateFiles(sourceDirectory, "*.xml");
                    if (files.Count() == 0)
                    {
                        break;
                    }
                    if (success_count + failed_count + error_count > ProgressMaxValue)
                    {
                        break;
                    }
                    int result = readXML(files);
                    if (result == -5)
                    {
                        continue;
                    }

                    row_count += dataMgr.DataTable.Rows.Count;
                    row_count += dataMgr.ErrorDataTable.Rows.Count;

                    break;
                }
                string[] logMessages = { "主題: " + dataMgr.DataConfigs[dataMgr.TableType].TypeName, "完成: " + success_count + " files", "失敗: " + failed_count + " files", "Error: " + error_count + " files",
                "資料:" + (row_count) + "筆", "用時:" + (DateTime.Now - start)};
                Log(logMessages, log_file);
                reading = false;
                readProThread = null;
                MessageBox.Show("主題: " + dataMgr.DataConfigs[dataMgr.TableType].TypeName + "\n完成: " + success_count + " files\n失敗: " + failed_count + " files\n" + "Error: " + error_count + " files\n" +
                    "資料:" + (row_count) + "筆\n用時:" + (DateTime.Now - start), "Read");
            }
            catch (Exception ex)
            {
                string[] logMessages = { "主題: " + dataMgr.DataConfigs[dataMgr.TableType].TypeName, "完成: " + success_count + " files", "失敗: " + failed_count + " files", "Error: " + error_count + " files",
                "資料:" + (row_count) + "筆", "用時:" + (DateTime.Now - start)};
                Log(logMessages, log_file);
                reading = false;
                readProThread = null;
                MessageBox.Show("主題: " + dataMgr.DataConfigs[dataMgr.TableType].TypeName + "\n完成: " + success_count + " files\n失敗: " + failed_count + " files\n" + "Error: " + error_count + " files\n" +
                    "資料:" + (row_count) + "筆\n用時:" + (DateTime.Now - start), "Read");
            }
        }

        private void fullRunProcess()
        {
            string log_file = $"{destinationDirectory}/Logs/{LogFile}";
            int fun_result;
            string readDoneirectory, completeDirectory, failDirectory, errorDirectory;
            DateTime start = DateTime.Now;
            var files = Directory.EnumerateFiles(sourceDirectory, "*.xml");
            success_count = 0;
            failed_count = 0;
            error_count = 0;
            progressMaxValue = files.Count();

            int row_count = 0;
            int current_type = 0;
            try
            {
                while (fullRunning)
                {
                    bool insert_error = false;
                    files = Directory.EnumerateFiles(sourceDirectory, "*.xml");
                    if (files.Count() == 0)
                    {
                        break;
                    }
                    if (success_count + failed_count + error_count > ProgressMaxValue)
                    {
                        break;
                    }
                    fun_result = readXML(files);
                    if (fun_result == -5)
                    {
                        continue;
                    }

                    row_count += dataMgr.DataTable.Rows.Count;
                    row_count += dataMgr.ErrorDataTable.Rows.Count;
                    current_type = dataMgr.TableType;
                    if (fun_result == -1)
                    {
                        break;
                    }
                    else if (fun_result == -3)
                    {

                        fullRunning = false;
                    }
                    else if (fun_result == -4)
                    {
                        insert_error = true;
                    }

                    fun_result = insertData();
                    readDoneirectory = $"{destinationDirectory}/{dataMgr.DataConfigs[dataMgr.TableType].TypeName}/Read_Done";
                    completeDirectory = $"{destinationDirectory}/{dataMgr.DataConfigs[dataMgr.TableType].TypeName}/Archive";
                    failDirectory = $"{destinationDirectory}/{dataMgr.DataConfigs[dataMgr.TableType].TypeName}/Fail";
                    if (!Directory.Exists(readDoneirectory))
                    {
                        Directory.CreateDirectory(readDoneirectory);
                    }
                    if (!Directory.Exists(completeDirectory))
                    {
                        Directory.CreateDirectory(completeDirectory);
                    }
                    if (!Directory.Exists(failDirectory))
                    {
                        Directory.CreateDirectory(failDirectory);
                    }
                    if (fun_result == 0)
                    {
                        dataMgr.DataTable.Clear();
                        foreach (string file in readDoneFiles)
                        {
                            string fileName = Path.GetFileName(file);
                            File.Move(readDoneirectory + "/" + fileName, completeDirectory + "/" + fileName);
                        }
                        readDoneFiles.Clear();
                    }
                    else
                    {
                        foreach (string file in readDoneFiles)
                        {
                            string fileName = Path.GetFileName(file);
                            File.Move(readDoneirectory + "/" + fileName, failDirectory + "/" + fileName);
                            success_count--;
                            failed_count++;
                        }
                        readDoneFiles.Clear();
                        break;
                    }
                    if (insert_error)
                    {
                        fun_result = insertData(true);
                        readDoneirectory = $"{destinationDirectory}/{dataMgr.DataConfigs[dataMgr.TableType].TypeName}/Read_Done";
                        failDirectory = $"{destinationDirectory}/{dataMgr.DataConfigs[dataMgr.TableType].TypeName}/Fail";
                        errorDirectory = $"{destinationDirectory}/{dataMgr.DataConfigs[dataMgr.TableType].TypeName}/Error";
                        if (!Directory.Exists(readDoneirectory))
                        {
                            Directory.CreateDirectory(readDoneirectory);
                        }
                        if (!Directory.Exists(failDirectory))
                        {
                            Directory.CreateDirectory(failDirectory);
                        }
                        if (!Directory.Exists(errorDirectory))
                        {
                            Directory.CreateDirectory(errorDirectory);
                        }
                        if (fun_result == 0)
                        {
                            dataMgr.ErrorDataTable.Clear();
                            foreach (string file in readDoneErrorFiles)
                            {
                                string fileName = Path.GetFileName(file);
                                File.Move(readDoneirectory + "/" + fileName, errorDirectory + "/" + fileName);
                            }
                            readDoneErrorFiles.Clear();
                        }
                        else
                        {
                            foreach (string file in readDoneErrorFiles)
                            {
                                string fileName = Path.GetFileName(file);
                                File.Move(readDoneirectory + "/" + fileName, failDirectory + "/" + fileName);
                                error_count--;
                                failed_count++;
                            }
                            readDoneErrorFiles.Clear();
                            break;
                        }
                    }
                }
                if (!Directory.Exists(destinationDirectory + "/Logs"))
                {
                    Directory.CreateDirectory(destinationDirectory + "/Logs");
                }
                string[] logMessages = { "主題: " + dataMgr.DataConfigs[dataMgr.TableType].TypeName, "完成: " + success_count + " files", "失敗: " + failed_count + " files", "Error: " + error_count + " files",
                "資料:" + (row_count) + "筆", "用時:" + (DateTime.Now - start)};
                Log(logMessages, log_file);
                fullRunning = false;
                fullRunProThread = null;
                MessageBox.Show("主題: " + dataMgr.DataConfigs[dataMgr.TableType].TypeName + "\n完成: " + success_count + " files\n失敗: " + failed_count + " files\n" + "Error: " + error_count + " files\n" +
                    "資料:" + (row_count) + "筆\n用時:" + (DateTime.Now - start), "FullRun");
            }
            catch (Exception ex)
            {
                string[] logMessages = { "主題: " + dataMgr.DataConfigs[dataMgr.TableType].TypeName, "完成: " + success_count + " files", "失敗: " + failed_count + " files", "Error: " + error_count + " files",
                "資料:" + (row_count) + "筆", "用時:" + (DateTime.Now - start)};
                Log(logMessages, log_file);
                fullRunning = false;
                fullRunProThread = null;
                MessageBox.Show("主題: " + dataMgr.DataConfigs[dataMgr.TableType].TypeName + "\n完成: " + success_count + " files\n失敗: " + failed_count + " files\n" + "Error: " + error_count + " files\n" +
                    "資料:" + (row_count) + "筆\n用時:" + (DateTime.Now - start), "FullRun");
            }
        }

        private void stopPro()
        {
            if (reading)
            {
                reading = false;
                try
                {
                    if (readProThread != null)
                    {
                        readProThread.Abort();
                        readProThread = null;
                    }
                }
                catch
                {

                }
            }
            if (fullRunning)
            {
                fullRunning = false;
                try
                {
                    if (fullRunProThread != null)
                    {
                        fullRunProThread.Abort();
                        fullRunProThread = null;
                    }
                }
                catch
                {

                }
            }
        }

        private void readXMLBtn_Click(object sender, EventArgs e)
        {
            if (checkFreeState())
            {
                reading = true;
                if (readProThread != null)
                {
                    readProThread.Abort();
                    readProThread = null;
                }
                if (srcTextBox.Text == "")
                {
                    MessageBox.Show("Source folder is empty", "Insert");
                    reading = false;
                    return;
                }
                if (dstTextBox.Text == "")
                {
                    MessageBox.Show("Destination folder is empty", "Insert");
                    reading = false;
                    return;
                }
                string[] src_split = srcTextBox.Text.Split('\\');
                string[] dst_split = dstTextBox.Text.Split('\\');
                if (src_split.Count() == dst_split.Count() + 2)
                {
                    for (int i = 0; i < dst_split.Count(); i++)
                    {
                        if (!src_split[i].Equals(dst_split[i]))
                        {
                            break;
                        }
                        if (i == dst_split.Count() - 1)
                        {
                            MessageBox.Show("Source folder can't be dst\\***\\***", "Insert");
                            reading = false;
                            return;
                        }
                    }
                }
                sourceDirectory = srcTextBox.Text;
                destinationDirectory = dstTextBox.Text;
                readProThread = new Thread(readingProcess);
                readProThread.IsBackground = true;
                readProThread.Start();
            }
        }

        private void insertBtn_Click(object sender, EventArgs e)
        {
            int fun_result;
            string readDoneirectory, completeDirectory, failDirectory, errorDirectory;
            if (checkFreeState())
            {
                if (serverName == null || databaseName == null)
                {
                    MessageBox.Show("Not login yet!", "FullRun");
                    return;
                }
                inserting = true;
                using (InsertCheckForm insertCheckForm = new InsertCheckForm(this))
                {
                    DialogResult dialogResult = insertCheckForm.ShowDialog();
                    if (dialogResult != DialogResult.OK)
                    {
                        inserting = false;
                        return;
                    }
                }
                try
                {
                    fun_result = insertData();
                    readDoneirectory = $"{destinationDirectory}/{dataMgr.DataConfigs[dataMgr.TableType].TypeName}/Read_Done";
                    completeDirectory = $"{destinationDirectory}/{dataMgr.DataConfigs[dataMgr.TableType].TypeName}/Archive";
                    failDirectory = $"{destinationDirectory}/{dataMgr.DataConfigs[dataMgr.TableType].TypeName}/Fail";
                    if (!Directory.Exists(readDoneirectory))
                    {
                        Directory.CreateDirectory(readDoneirectory);
                    }
                    if (!Directory.Exists(completeDirectory))
                    {
                        Directory.CreateDirectory(completeDirectory);
                    }
                    if (!Directory.Exists(failDirectory))
                    {
                        Directory.CreateDirectory(failDirectory);
                    }
                    if (fun_result == 0)
                    {
                        dataMgr.DataTable.Clear();
                        foreach (string file in readDoneFiles)
                        {
                            string fileName = Path.GetFileName(file);
                            File.Move(readDoneirectory + "/" + fileName, completeDirectory + "/" + fileName);
                        }
                        readDoneFiles.Clear();
                        MessageBox.Show("Success", "Insert");
                    }
                    else
                    {
                        foreach (string file in readDoneFiles)
                        {
                            string fileName = Path.GetFileName(file);
                            File.Move(readDoneirectory + "/" + fileName, failDirectory + "/" + fileName);
                            success_count--;
                            failed_count++;
                        }
                        readDoneFiles.Clear();
                        MessageBox.Show("Fail", "Insert");
                    }

                    fun_result = insertData(true);
                    readDoneirectory = $"{destinationDirectory}/{dataMgr.DataConfigs[dataMgr.TableType].TypeName}/Read_Done";
                    failDirectory = $"{destinationDirectory}/{dataMgr.DataConfigs[dataMgr.TableType].TypeName}/Fail";
                    errorDirectory = $"{destinationDirectory}/{dataMgr.DataConfigs[dataMgr.TableType].TypeName}/Error";
                    if (!Directory.Exists(readDoneirectory))
                    {
                        Directory.CreateDirectory(readDoneirectory);
                    }
                    if (!Directory.Exists(failDirectory))
                    {
                        Directory.CreateDirectory(failDirectory);
                    }
                    if (!Directory.Exists(errorDirectory))
                    {
                        Directory.CreateDirectory(errorDirectory);
                    }
                    if (fun_result == 0)
                    {
                        dataMgr.ErrorDataTable.Clear();
                        foreach (string file in readDoneErrorFiles)
                        {
                            string fileName = Path.GetFileName(file);
                            File.Move(readDoneirectory + "/" + fileName, errorDirectory + "/" + fileName);
                        }
                        readDoneErrorFiles.Clear();
                    }
                    else
                    {
                        foreach (string file in readDoneErrorFiles)
                        {
                            string fileName = Path.GetFileName(file);
                            File.Move(readDoneirectory + "/" + fileName, failDirectory + "/" + fileName);
                            error_count--;
                            failed_count++;
                        }
                        readDoneErrorFiles.Clear();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    MessageBox.Show("Fail", "Insert");
                }
                inserting = false;
            }
        }

        private void fullRunBtn_Click(object sender, EventArgs e)
        {
            if (checkFreeState())
            {
                if (serverName == null || databaseName == null)
                {
                    MessageBox.Show("Not login yet!", "FullRun");
                    return;
                }
                fullRunning = true;
                if (fullRunProThread != null)
                {
                    fullRunProThread.Abort();
                    fullRunProThread = null;
                }
                if (srcTextBox.Text == "")
                {
                    MessageBox.Show("Source folder is empty", "Insert");
                    fullRunning = false;
                    return;
                }
                if (dstTextBox.Text == "")
                {
                    MessageBox.Show("Destination folder is empty", "Insert");
                    fullRunning = false;
                    return;
                }
                string[] src_split = srcTextBox.Text.Split('\\');
                string[] dst_split = dstTextBox.Text.Split('\\');
                if (src_split.Count() == dst_split.Count() + 2)
                {
                    for (int i = 0; i < dst_split.Count(); i++)
                    {
                        if (src_split[i] != dst_split[i])
                        {
                            break;
                        }
                        if (i == dst_split.Count() - 1)
                        {
                            MessageBox.Show("Source folder can't be dst\\***\\***", "Insert");
                            fullRunning = false;
                            return;
                        }
                    }
                }
                sourceDirectory = srcTextBox.Text;
                destinationDirectory = dstTextBox.Text;
                using (InsertCheckForm insertCheckForm = new InsertCheckForm(this))
                {
                    DialogResult dialogResult = insertCheckForm.ShowDialog();
                    if (dialogResult != DialogResult.OK)
                    {
                        fullRunning = false;
                        return;
                    }
                }
                fullRunProThread = new Thread(fullRunProcess);
                fullRunProThread.IsBackground = true;
                fullRunProThread.Start();
            }
        }

        private void srcBrowseBtn_Click(object sender, EventArgs e)
        {
            if (checkFreeState())
            {
                folderBrowserDialog.Description = "選擇來源文件的資料夾位置";
                folderBrowserDialog.ShowDialog();
                srcTextBox.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void dstBrowseBtn_Click(object sender, EventArgs e)
        {
            if (checkFreeState())
            {
                folderBrowserDialog.Description = "選擇完成文件的資料夾位置";
                folderBrowserDialog.ShowDialog();
                dstTextBox.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            if (checkFreeState())
            {
                using (LoginForm loginForm = new LoginForm(this))
                {
                    loginForm.ShowDialog();
                }
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (reading)
            {
                dataGridView.DataSource = null;
                stateLabel.Text = "Reading";
            }
            else if (inserting)
            {
                dataGridView.DataSource = null;
                stateLabel.Text = "Inserting";
            }
            else if (fullRunning)
            {
                dataGridView.DataSource = null;
                stateLabel.Text = "FullRunning";
            }
            else
            {
                stateLabel.Text = "Free";
                if (isDataTable)
                    dataGridView.DataSource = dataMgr.DataTable;
                else
                    dataGridView.DataSource = dataMgr.ErrorDataTable;
            }
            progressBar.Maximum = ProgressMaxValue;
            progressValue = success_count + failed_count + error_count;
            if (progressValue > ProgressMaxValue)
            {
                progressValue = ProgressMaxValue;
            }
            progressBar.Value = ProgressValue;
            processLabel.Text = $"{progressValue} / {ProgressMaxValue}";
        }

        private void stopBtn_Click(object sender, EventArgs e)
        {
            stopPro();
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            if (checkFreeState())
            {
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult dialogResult = MessageBox.Show("Warning!\n將清除datatable，確定要繼續?", "Warning", buttons);
                if (dialogResult != DialogResult.Yes)
                {
                    return;
                }
                success_count = 0;
                failed_count = 0;
                error_count = 0;
                progressValue = 0;
                progressMaxValue = 0;
                dataMgr.DataTable.Clear();
                dataMgr.ErrorDataTable.Clear();
                readDoneFiles.Clear();
                readDoneErrorFiles.Clear();
            }
        }

        private void switchTableBtn_Click(object sender, EventArgs e)
        {
            if (checkFreeState())
            {
                isDataTable = !isDataTable;
                if (isDataTable)
                {
                    tableLabel.Text = "Normal";
                }
                else
                {
                    tableLabel.Text = "Error";
                }
            }
        }
    }
}
