using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Text.Json;

namespace XMLToSQL.Managers
{
    struct DataColInfo
    {
        public DataColInfo(string name, Type type, int length = 0, bool isByteLength = false, string tagName = null)
        {
            this.ColName = name;
            this.TypeString = type.ToString();
            this.Length = length;
            this.IsByteLength = isByteLength;
            if (tagName == null)
            {
                this.TagName = ColName;
            }
            else
            {
                this.TagName = tagName;
            }
        }
        public string ColName { get; set; }
        public string TypeString { get; set; }
        public int Length { get; set; }
        public bool IsByteLength { get; set; }
        public string TagName { get; set; }
    }
    class XMLDataConfig
    {
        public string TypeName { get; set; }
        public string TableName { get; set; }
        public string ErrorTableName { get; set; }
        public string RootTag { get; set; }
        public int ModifidateLength { get; set; }
        public List<DataColInfo> DataColInfos { get; set; }
        public List<List<string>> DiviTagLists { get; set; }
        public XMLDataConfig(string typeName, string tableName, string errorTableName, string rootTag, int modifidateLength, List<DataColInfo> datacolInfos, List<List<string>> diviTagLists)
        {
            this.TypeName = typeName;
            this.TableName = tableName;
            this.ErrorTableName = errorTableName;
            this.RootTag = rootTag;
            this.ModifidateLength = modifidateLength;
            this.DataColInfos = datacolInfos;
            this.DiviTagLists = diviTagLists;
        }
    }

    class DataManager
    {
        private MainForm mainForm;

        private DataTable dataTable;
        public DataTable DataTable { get => dataTable; }
        private DataTable errorDataTable;
        public DataTable ErrorDataTable { get => errorDataTable; }

        private bool success;
        public bool Success { get => success; }

        private int tableType;
        /// <summary>
        /// Set => 設定datatable的type，若不同則會清除原本datatable
        /// </summary>
        public int TableType
        {
            get => tableType;
            set
            {
                if (value < 0 || value >= typeNum || tableType == value)
                {
                    return;
                }
                tableType = value;
                dataTable.Columns.Clear();
                dataTable.Dispose();
                dataTable = new DataTable();
                errorDataTable.Columns.Clear();
                errorDataTable.Dispose();
                errorDataTable = new DataTable();
            }
        }

        private List<XMLDataConfig> dataConfigs;
        public List<XMLDataConfig> DataConfigs { get => dataConfigs; }
        private int typeNum;

        public DataManager(MainForm mainForm)
        {
            this.mainForm = mainForm;
            dataConfigs = new List<XMLDataConfig>();
            typeNum = 0;
            tableType = 0;

            success = readDataColumnInfo();

            dataTable = Create_Table();
            errorDataTable = Create_Table();
        }

        private void genExDataConfig()
        {
            int i;
            string[] typeName = { "MM_1_1", "MM_10_1", "SD_1_1", "SD_2_1", "SD_8_1" };
            string[] tableName = { "dbo.MM_1_1_Target_temp1", "dbo.MM_10_1_Target_temp1",
            "dbo.SD_1_1_Target_temp1", "dbo.SD_2_1_Target_temp1", "dbo.SD_8_1_Target_temp1" };
            string[] errorTableName = { "dbo.MM_1_1_error", "dbo.MM_10_1_error",
            "dbo.SD_1_1_error", "dbo.SD_2_1_error", "dbo.SD_8_1_error" };
            #region data column infos
            List<DataColInfo> MM_1_1_DataColInfos = new List<DataColInfo> {
            new DataColInfo("PROD", typeof(string), 20, false),
            new DataColInfo("IND_TYPE", typeof(string), 1, true),
            new DataColInfo("PROD_TYPE", typeof(string), 4, true),
            new DataColInfo("PLANT", typeof(string), 10, false),
            new DataColInfo("SALES_ORG", typeof(string), 10, true),
            new DataColInfo("DISTR_CHAN", typeof(string), 10, true),
            new DataColInfo("PROD_CDESC", typeof(string), 256, false),
            new DataColInfo("PROD_UNIT", typeof(string), 3, true),
            new DataColInfo("UMATKL", typeof(string), 9, true),
            new DataColInfo("UBISMT", typeof(string), 18, true),
            new DataColInfo("DIV", typeof(string), 10, true),
            new DataColInfo("PROD_BRAND", typeof(string), 10, true),
            new DataColInfo("PROD_LINE", typeof(string), 10, true),
            new DataColInfo("PROD_SUBLINE", typeof(string), 10, true),
            new DataColInfo("BEAUTY_CATG_LVL1", typeof(string), 2, true),
            new DataColInfo("BEAUTY_CATG_LVL2", typeof(string), 4, true),
            new DataColInfo("BEAUTY_CATG_LVL3", typeof(string), 6, true),
            new DataColInfo("MTPOS_MARA", typeof(string), 4, true),
            new DataColInfo("UBRGEW", typeof(decimal), 13, false),
            new DataColInfo("UNTGEW", typeof(decimal), 13, false),
            new DataColInfo("UGEWEI", typeof(string), 3, true),
            new DataColInfo("UVOLUM", typeof(decimal), 13, false),
            new DataColInfo("UVOLEH", typeof(string), 3, true),
            new DataColInfo("EAN11", typeof(string), 18, false),
            new DataColInfo("NUMTP", typeof(string), 2, true),
            new DataColInfo("LANG", typeof(string), 5, true),
            new DataColInfo("PROD_EDESC", typeof(string), 256, false),
            new DataColInfo("UMREN", typeof(decimal), 5, false),
            new DataColInfo("PAK_UNIT", typeof(string), 3, true),
            new DataColInfo("PAK_QTY", typeof(int)),
            new DataColInfo("LAENG", typeof(decimal), 13, false),
            new DataColInfo("BREIT", typeof(decimal), 13, false),
            new DataColInfo("HOEHE", typeof(decimal), 13, false),
            new DataColInfo("MEABM", typeof(string), 3, true),
            new DataColInfo("EAN11_2", typeof(string), 18, false),
            new DataColInfo("NUMTP_2", typeof(string), 2, true),
            new DataColInfo("PRCTR", typeof(string), 10, false),
            new DataColInfo("VMSTA", typeof(string), 2, true),
            new DataColInfo("VMSTD", typeof(string), 8, true),
            new DataColInfo("TAXKM", typeof(string), 1, true),
            new DataColInfo("SKTOF", typeof(string), 2, true),
            new DataColInfo("LFMNG", typeof(decimal), 13, false),
            new DataColInfo("COST_CENTER", typeof(string), 10, true),
            new DataColInfo("GMCTMC_TYPE", typeof(string), 2, true),
            new DataColInfo("KTGRM", typeof(string), 2, false),
            new DataColInfo("MTPOS", typeof(string), 4, true),
            new DataColInfo("HCCS_TYPE", typeof(string), 2, true),
            new DataColInfo("MVGR1", typeof(string), 3, false),
            new DataColInfo("MVGR2", typeof(string), 3, false),
            new DataColInfo("SHIPPING_GRADE", typeof(string), 3, true),
            new DataColInfo("MVGR4", typeof(string), 3, true),
            new DataColInfo("MVGR5", typeof(string), 3, true),
            new DataColInfo("MTVFP", typeof(string), 2, true),
            new DataColInfo("TRAGR", typeof(string), 4, true),
            new DataColInfo("LADGR", typeof(string), 4, true),
            new DataColInfo("PURCH_GRP", typeof(string), 3, true),
            new DataColInfo("MMSTA", typeof(string), 2, true),
            new DataColInfo("MMSTD", typeof(string), 8, true),
            new DataColInfo("KAUTB", typeof(string), 1, true),
            new DataColInfo("EKWSL", typeof(string), 4, true),
            new DataColInfo("UEBTK", typeof(string), 1, true),
            new DataColInfo("XCHPF", typeof(string), 1, true),
            new DataColInfo("STAWN", typeof(string), 17, true),
            new DataColInfo("PRODUCER_COUNTRY", typeof(string), 5, true),
            new DataColInfo("SHIPPING_COUNTRY", typeof(string), 5, true),
            new DataColInfo("EVAL_TYPE", typeof(string), 4, true),
            new DataColInfo("SALE_START_DT", typeof(string), 10, true),
            new DataColInfo("SALE_TERMINATE_DT", typeof(string), 10, true),
            new DataColInfo("ATWRT_Class", typeof(string), 70, true),
            new DataColInfo("ATWRT_Alcohol_volume_PER", typeof(string), 70, true),
            new DataColInfo("ATWRT_Volume_Content", typeof(string), 10, false),
            new DataColInfo("ATWRT_Volume_Content_Unit", typeof(string), 10, false),
            new DataColInfo("SALES_TEPE_1", typeof(string), 5, true),
            new DataColInfo("SALES_TEPE_2", typeof(string), 5, true),
            new DataColInfo("SALES_TEPE_3", typeof(string), 10, true),
            new DataColInfo("PROD_END_DT", typeof(DateTime)),
            new DataColInfo("SAVE_MNS", typeof(int)),
            new DataColInfo("ModifiedDate", typeof(DateTime)),
            new DataColInfo("ModifiedDate1", typeof(decimal), 18, false)
        };

            List<DataColInfo> MM_10_1_DataColInfos = new List<DataColInfo> {
            new DataColInfo("PROD", typeof(string), 20, true),
            new DataColInfo("PLANT", typeof(string), 10, true),
            new DataColInfo("STORAGE_LOC", typeof(string), 10, true),
            new DataColInfo("BATCH_NO", typeof(string), 10, true),
            new DataColInfo("VFDAT", typeof(string), 10, true),
            new DataColInfo("LIFNR", typeof(string), 10, true),
            new DataColInfo("CUST", typeof(string), 10, true),
            new DataColInfo("PROD_UNIT", typeof(string), 5, true),
            new DataColInfo("PROD_QTY", typeof(decimal), 12, false),
            new DataColInfo("STATUS", typeof(string), 16, true),
            new DataColInfo("SPEICAL_IND", typeof(string), 5, true),
            new DataColInfo("MENGE", typeof(int)),
            new DataColInfo("INSME", typeof(int)),
            new DataColInfo("EINME", typeof(int)),
            new DataColInfo("SPEME", typeof(int)),
            new DataColInfo("ModifiedDate", typeof(DateTime)),
            new DataColInfo("ModifiedDate1", typeof(decimal), 18, false)
        };

            List<DataColInfo> SD_1_1_DataColInfos = new List<DataColInfo> {
            new DataColInfo("CUST", typeof(string), 10, true),
            new DataColInfo("SALES_ORG", typeof(string), 10, true),
            new DataColInfo("CHAN_DISTR", typeof(string), 10, true),
            new DataColInfo("DIV", typeof(string), 10, true),
            new DataColInfo("KTOKD", typeof(string), 4, true),
            new DataColInfo("CUST_ABBR", typeof(string), 128, false),
            new DataColInfo("MKNAME", typeof(string), 50, false),
            new DataColInfo("CUST_CNAME", typeof(string), 256, false),
            new DataColInfo("COUNTRY", typeof(string), 5, true),
            new DataColInfo("POST_CD", typeof(string), 10, true),
            new DataColInfo("CITY", typeof(string), 50, false),
            new DataColInfo("DELIVERY_ADDR", typeof(string), 512, false),
            new DataColInfo("INVOICE_ADDR",  typeof(string), 512, false),
            new DataColInfo("TEL", typeof(string), 30, true),
            new DataColInfo("TEL_EXT", typeof(string), 10, true),
            new DataColInfo("FAX", typeof(string), 30, true),
            new DataColInfo("FAX_EXT", typeof(string), 10, true),
            new DataColInfo("VBUND", typeof(string), 6, true),
            new DataColInfo("KONZS", typeof(string), 10, true),
            new DataColInfo("UNIFY_CD", typeof(string), 20, false),
            new DataColInfo("CUST_TYPE", typeof(string), 6, true),
            new DataColInfo("ZZTRADE_ORG",   typeof(string), 2, true),
            new DataColInfo("KATR6", typeof(string), 3, true),
            new DataColInfo("KNRZE", typeof(string), 10, true),
            new DataColInfo("STORE_LVL4", typeof(string), 10, true),
            new DataColInfo("SALES_OFFICE",  typeof(string), 10, true),
            new DataColInfo("SALES_GRP", typeof(string), 10, true),
            new DataColInfo("KATR9", typeof(string), 2, true),
            new DataColInfo("KATR10", typeof(string), 3, true),
            new DataColInfo("KVGR3", typeof(string), 3, true),
            new DataColInfo("KUNN2", typeof(string), 30, true),
            new DataColInfo("CUST_OLD", typeof(string), 20, true),
            new DataColInfo("CUSTNO", typeof(string), 20, false),
            new DataColInfo("CHAN_LVL2", typeof(string), 4, true),
            new DataColInfo("CHAN_STORE", typeof(string), 3, true),
            new DataColInfo("BIZ_AREA", typeof(string), 4, true),
            new DataColInfo("SHIP_SERIES", typeof(string), 3, true),
            new DataColInfo("ZEDIGUI", typeof(string), 10, true),
            new DataColInfo("STRATEGY", typeof(string), 10, true),
            new DataColInfo("OPEN_DT", typeof(string), 10, true),
            new DataColInfo("CLOSE_DT", typeof(string), 10, true),
            new DataColInfo("ISS_STORE", typeof(string), 10, true),
            new DataColInfo("GRADE_STORE", typeof(string), 10, true),
            new DataColInfo("GRADE", typeof(string), 10, true),
            new DataColInfo("Z_TW_BONUS_LEVEL", typeof(string), 1, true),
            new DataColInfo("STOSERTYP", typeof(string), 1, true),
            new DataColInfo("CUSTYPE",   typeof(string), 3, true),
            new DataColInfo("Z_TW_STORE_ADDRESS", typeof(string), 140, true),
            new DataColInfo("CON_PERSON", typeof(string), 20, false),
            new DataColInfo("HKUNNR", typeof(string), 10, true),
            new DataColInfo("DATAB", typeof(string), 10, true),
            new DataColInfo("DATBI", typeof(string), 10, true),
            new DataColInfo("MSGFN", typeof(string), 3, true),
            new DataColInfo("ModifiedDate", typeof(DateTime)),
            new DataColInfo("ModifiedDate1", typeof(decimal), 18, false)
        };

            List<DataColInfo> SD_2_1_DataColInfos = new List<DataColInfo> {
            new DataColInfo("SALES_ORG", typeof(string), 10, true),
            new DataColInfo("DIV", typeof(string), 10, true),
            new DataColInfo("PROD", typeof(string), 20, true),
            new DataColInfo("EFF_START_DT", typeof(string), 10, true),
            new DataColInfo("EFF_END_DT", typeof(string), 10, true),
            new DataColInfo("STD_PRICE", typeof(decimal), 15, false),
            new DataColInfo("CURRENCY", typeof(string), 50, true),
            new DataColInfo("STD_QTY", typeof(int)),
            new DataColInfo("PROD_UNIT", typeof(string), 3, true),
            new DataColInfo("DEL_IND", typeof(string), 1, true),
            new DataColInfo("KSCHL", typeof(string), 4, true),
            new DataColInfo("ModifiedDate", typeof(DateTime)),
            new DataColInfo("ModifiedDate1", typeof(decimal), 18, false)
        };

            List<DataColInfo> SD_8_1_DataColInfos = new List<DataColInfo> {
            new DataColInfo("BILLING_ID", typeof(string), 40, true),
            new DataColInfo("BILLING_CNT", typeof(string), 10, true),
            new DataColInfo("BILLING_DT", typeof(string), 10, true),
            new DataColInfo("BILLING_TYPE", typeof(string), 40, true),
            new DataColInfo("COMPANY", typeof(string), 40, true),
            new DataColInfo("SALES_ORG", typeof(string), 40, true),
            new DataColInfo("DISTR_CHAN", typeof(string), 40, true),
            new DataColInfo("DIV", typeof(string), 40, true),
            new DataColInfo("STORE_LVL4", typeof(string), 10, true),
            new DataColInfo("CUST", typeof(string), 20, true),
            new DataColInfo("KUNRG", typeof(string), 20, true),
            new DataColInfo("CURRENCY", typeof(string), 10, true),
            new DataColInfo("EXCHANGE_RATE", typeof(decimal), 10, false),
            new DataColInfo("ZLSCH", typeof(string), 1, true),
            new DataColInfo("ZUONR", typeof(string), 40, true),
            new DataColInfo("XBLNR", typeof(string), 40, true),
            new DataColInfo("REVERSE_BILLING_ID", typeof(string), 10, true),
            new DataColInfo("CREATE_DT", typeof(string), 10, true),
            new DataColInfo("CREATE_TM", typeof(string), 6, true),
            new DataColInfo("BILLING_SEQ", typeof(string), 10, true),
            new DataColInfo("PSTYV", typeof(string), 4, true),
            new DataColInfo("RETURN_IND", typeof(string), 1, true),
            new DataColInfo("SALES_OFFICE", typeof(string), 10, true),
            new DataColInfo("SALES_GRP", typeof(string), 10, true),
            new DataColInfo("PROD", typeof(string), 40, true),
            new DataColInfo("PLANT", typeof(string), 10, true),
            new DataColInfo("STORAGE_LOC", typeof(string), 4, true),
            new DataColInfo("PROD_QTY", typeof(decimal), 18, false),
            new DataColInfo("PROD_UNIT", typeof(string), 3, true),
            new DataColInfo("PROD_WOTAX_AMT", typeof(decimal), 20, false),
            new DataColInfo("PROD_TAX_AMT", typeof(decimal), 20, false),
            new DataColInfo("PROD_COST", typeof(decimal), 20, false),
            new DataColInfo("SO_REASON", typeof(string), 3, true),
            new DataColInfo("SO_TYPE", typeof(string), 1, true),
            new DataColInfo("SO_ID", typeof(string), 10, true),
            new DataColInfo("SO_SEQ", typeof(string), 10, true),
            new DataColInfo("BNAME", typeof(string), 40, true),
            new DataColInfo("BSTKD", typeof(string), 40, true),
            new DataColInfo("VGBEL", typeof(string), 10, true),
            new DataColInfo("VGPOS", typeof(string), 10, true),
            new DataColInfo("BOLNR", typeof(string), 40, true),
            new DataColInfo("ModifiedDate", typeof(DateTime)),
            new DataColInfo("ModifiedDate1", typeof(decimal), 18, false)
        };
            #endregion
            string[] rootTag = { "Record", "Record", "RECORDSET", "E1KOMG", "HEADER" };
            List<List<string>>[] divTagLists = new List<List<string>>[5];
            divTagLists[0] = new List<List<string>> { new List<string> { "E1MARCM", "E1MVKEM", "E1MARMM", "E1MBEWM" }, new List<string> { "E1MEANM" } };
            divTagLists[1] = new List<List<string>>();
            divTagLists[2] = new List<List<string>>();
            divTagLists[3] = new List<List<string>>();
            divTagLists[4] = new List<List<string>> { new List<string> { "ITEM" } };


            List<DataColInfo>[] dataColInfos = new List<DataColInfo>[5];

            dataColInfos[0] = MM_1_1_DataColInfos;
            dataColInfos[1] = MM_10_1_DataColInfos;
            dataColInfos[2] = SD_1_1_DataColInfos;
            dataColInfos[3] = SD_2_1_DataColInfos;
            dataColInfos[4] = SD_8_1_DataColInfos;
            Directory.CreateDirectory(MainForm.ConfigDirectory);
            for (i = 0; i < 5; i++)
            {
                XMLDataConfig config = new XMLDataConfig(typeName[i], tableName[i], errorTableName[i], rootTag[i], 19, dataColInfos[i], divTagLists[i]);
                string json_str = JsonSerializer.Serialize(config);
                File.WriteAllText($"{MainForm.ConfigDirectory}/Config_{typeName[i]}.json", json_str);
                dataConfigs.Add(config);
            }
            typeNum = 5;
        }

        private bool readDataColumnInfo()
        {
            int i;
            tableType = 0;
            typeNum = 0;
            dataConfigs.Clear();
            if (!Directory.Exists(MainForm.ConfigDirectory))
            {
                Directory.CreateDirectory(MainForm.ConfigDirectory);
            }
            var files = Directory.EnumerateFiles(MainForm.ConfigDirectory, "Config_*.json");
            if (files.Count() == 0)
            {
                genExDataConfig();
                return true;
            }

            i = 0;
            foreach (string file in files)
            {
                string json_str = File.ReadAllText(file);
                XMLDataConfig config;
                try
                {
                    config = JsonSerializer.Deserialize<XMLDataConfig>(json_str);
                    foreach (DataColInfo info in config.DataColInfos)
                    {
                        Type.GetType(info.TypeString);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    continue;
                }
                dataConfigs.Add(config);
                i++;
            }
            if (i == 0)
            {
                Console.WriteLine("type num = " + i);
                return false;
            }
            typeNum = i;
            return true;
        }

        public int FindType(string fileName)
        {
            int i;
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(fileName);
            for (i = 0; i < typeNum; i++)
            {
                if (fileNameWithoutExt.Substring(0, dataConfigs[i].TypeName.Length).Equals(dataConfigs[i].TypeName))
                {
                    break;
                }
            }
            if (i == typeNum)
            {
                return -1;
            }
            return i;
        }

        public DataTable Create_Table()
        {
            if (!success)
            {
                return null;
            }
            //建立資料格式
            DataTable dt = new DataTable();
            foreach (DataColInfo info in dataConfigs[tableType].DataColInfos)
            {
                try
                {
                    dt.Columns.Add(info.ColName, Type.GetType(info.TypeString));
                }
                catch
                {
                    return null;
                }
            }
            return dt;
        }

        /// <summary>
        /// 從xml file讀取資料到datatable, 請先用CreateTable()創建data table
        /// 失敗會回傳null
        /// </summary>
        /// <param name="xmlFileName"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        public int ReadXML(string xmlFileName, DataTable table)
        {
            int result = 0, fun_result;
            string fail_log_file = $"{mainForm.DestinationDirectory}/Logs/{dataConfigs[tableType].TypeName}_{MainForm.FailLogFile}";
            if (!success)
            {
                string[] logMessages = { "檔案: " + xmlFileName, "Error: data config error" };
                MainForm.Log(logMessages, fail_log_file);
                return -1;
            }
            Console.WriteLine("Reading xml...");
            //建立資料格式
            if (table == null)
            {
                string[] logMessages = { "檔案: " + xmlFileName, "Error: ReadXML() table is null" };
                MainForm.Log(logMessages, fail_log_file);
                return -1;
            }

            #region xml檔案名稱分析
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(xmlFileName);
            if (!fileNameWithoutExt.Substring(0, dataConfigs[tableType].TypeName.Length).Equals(dataConfigs[tableType].TypeName))
            {
                string[] logMessages = { "檔案: " + xmlFileName, "Error: Type out of range" };
                MainForm.Log(logMessages, fail_log_file);
                return -1;
            }
            #endregion

            XMLDataConfig config = dataConfigs[tableType];

            decimal modifidate = 0;
            if (config.ModifidateLength > 0)
            {
                string modifidateString = fileNameWithoutExt.Substring(fileNameWithoutExt.Length - config.ModifidateLength, config.ModifidateLength);
                modifidateString = modifidateString.Replace("-", "");
                //DateTime modifidate;
                try
                {
                    modifidate = decimal.Parse(modifidateString);
                    //modifidate = DateTime.ParseExact(modifidateString, "yyyyMMdd-HHmmss-fff", null);
                }
                catch
                {
                    string[] logMessages = { "檔案: " + xmlFileName, "error type: " + "modifidate error" };
                    MainForm.Log(logMessages, fail_log_file);
                    return -1;
                }
            }
            try
            {
                XDocument xml = XDocument.Load(xmlFileName);
                var records = XDocument.Load(xmlFileName).Root.Elements(config.RootTag);
                foreach (var record in records)
                {
                    List<DataRow> insertDataRows = new List<DataRow>();
                    var newRow = table.NewRow();
                    if (config.ModifidateLength > 0)
                    {
                        newRow["ModifiedDate1"] = modifidate;
                    }
                    insertDataRows.Add(newRow);

                    List<string> blackList = new List<string>();
                    foreach (var list in config.DiviTagLists)
                    {
                        blackList.AddRange(list);
                    }
                    fun_result = DivideByTagsToRows(xmlFileName, table, insertDataRows, record, config.DiviTagLists);
                    if (fun_result == -1)
                    {
                        return -1;
                    }
                    else if (fun_result == 1)
                    {
                        result = fun_result;
                    }
                    fun_result = XMLTagsToRows(xmlFileName, table, insertDataRows, record, blackList);
                    if (fun_result == -1)
                    {
                        return -1;
                    }
                    else if (fun_result == 1)
                    {
                        result = fun_result;
                    }

                    foreach (var insertDataRow in insertDataRows)
                    {
                        table.Rows.InsertAt(insertDataRow, table.Rows.Count);
                    }

                }
            }
            catch (Exception ex)
            {
                string[] logMessages = { "檔案: " + xmlFileName, "error: " + ex.Message };
                MainForm.Log(logMessages, fail_log_file);
                return -1;
            }
            Console.WriteLine("Read done");
            return result;
        }

        /// <summary>
        /// duplicate rows by duplicated tags
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="dataRows"></param>
        /// <param name="rootElemnent"></param>
        /// <param name="type"></param>
        /// <param name="tagList"></param>
        private int DivideByTagsToRows(string fileName, DataTable dataTable, List<DataRow> dataRows, XElement rootElemnent, List<List<string>> diviTagLists, int layer = 0)
        {
            int result = 0, fun_result;
            if (diviTagLists == null || layer >= diviTagLists.Count)
            {
                return 0;
            }
            foreach (string tag in diviTagLists[layer])
            {
                var elements = rootElemnent.Elements(tag);
                int count = dataRows.Count;
                List<string> blackList = new List<string>();
                foreach (var list in diviTagLists)
                {
                    blackList.AddRange(list);
                }
                for (int i = 0; i < count; i++)
                {
                    var sample_row = dataTable.NewRow();
                    sample_row.ItemArray = dataRows.ElementAt(i).ItemArray.Clone() as object[];
                    bool first = false;
                    for (int j = 0; j < elements.Count(); j++)
                    {
                        if (!elements.ElementAt(j).HasElements)
                        {
                            continue;
                        }
                        if (!first)
                        {
                            if (layer < diviTagLists.Count - 1)
                            {
                                List<DataRow> sec_data_rows = new List<DataRow>();
                                var sec_new_row = dataTable.NewRow();
                                sec_new_row.ItemArray = sample_row.ItemArray.Clone() as object[];
                                sec_data_rows.Add(sec_new_row);
                                fun_result = DivideByTagsToRows(fileName, dataTable, sec_data_rows, elements.ElementAt(j), diviTagLists, layer + 1);
                                if (fun_result == -1)
                                {
                                    return -1;
                                }
                                else if (fun_result == 1)
                                {
                                    result = 1;
                                }
                                fun_result = XMLTagsToRows(fileName, dataTable, sec_data_rows, elements.ElementAt(j), blackList);
                                if (fun_result == -1)
                                {
                                    return -1;
                                }
                                else if (fun_result == 1)
                                {
                                    result = 1;
                                }
                                for (int k = 0; k < sec_data_rows.Count; k++)
                                {
                                    if (k == 0)
                                    {
                                        dataRows.ElementAt(i).ItemArray = sec_data_rows.ElementAt(k).ItemArray.Clone() as object[];
                                    }
                                    else
                                    {
                                        dataRows.Add(sec_data_rows.ElementAt(k));
                                    }
                                }
                            }
                            else
                            {
                                fun_result = XMLTagsToRow(fileName, dataTable, dataRows.ElementAt(i), elements.ElementAt(j));
                                if (fun_result == -1)
                                {
                                    return -1;
                                }
                                else if (fun_result == 1)
                                {
                                    result = 1;
                                }
                            }
                            first = true;
                        }
                        else
                        {
                            if (layer < diviTagLists.Count - 1)
                            {
                                List<DataRow> sec_data_rows = new List<DataRow>();
                                var sec_new_row = dataTable.NewRow();
                                sec_new_row.ItemArray = sample_row.ItemArray.Clone() as object[];
                                sec_data_rows.Add(sec_new_row);
                                fun_result = DivideByTagsToRows(fileName, dataTable, sec_data_rows, elements.ElementAt(j), diviTagLists, layer + 1);
                                if (fun_result == -1)
                                {
                                    return -1;
                                }
                                else if (fun_result == 1)
                                {
                                    result = 1;
                                }
                                fun_result = XMLTagsToRows(fileName, dataTable, sec_data_rows, elements.ElementAt(j), blackList);
                                if (fun_result == -1)
                                {
                                    return -1;
                                }
                                else if (fun_result == 1)
                                {
                                    result = 1;
                                }
                                for (int k = 0; k < sec_data_rows.Count; k++)
                                {
                                    dataRows.Add(sec_data_rows.ElementAt(k));
                                }
                            }
                            else
                            {
                                var new_row = dataTable.NewRow();
                                new_row.ItemArray = sample_row.ItemArray.Clone() as object[];
                                fun_result = XMLTagsToRow(fileName, dataTable, new_row, elements.ElementAt(j));
                                if (fun_result == -1)
                                {
                                    return -1;
                                }
                                else if (fun_result == 1)
                                {
                                    result = 1;
                                }
                                dataRows.Add(new_row);
                            }
                        }
                    }
                }
            }
            return result;
        }

        private int XMLTagsToRow(string fileName, DataTable dataTable, DataRow dataRow, XElement rootElemnent, List<string> blackList = null)
        {
            int result = 0, fun_result;
            XMLDataConfig configs = dataConfigs[tableType];
            string fail_log_file = $"{mainForm.DestinationDirectory}/Logs/{configs.TypeName}_{MainForm.FailLogFile}";
            if (!rootElemnent.HasElements)
                return 0;
            var elements = rootElemnent.Elements();
            foreach (var element in elements)
            {
                if (blackList != null && blackList.Contains(element.Name.ToString()))
                {
                    continue;
                }
                if (element.IsEmpty || element.Value == "")
                {
                    continue;
                }
                if (!element.HasElements)
                {
                    try
                    {

                        #region Check legal
                        int ind = dataTable.Columns.IndexOf(element.Name.ToString());
                        if (ind < 0)
                        {
                            Console.WriteLine("There is no column named " + element.Name);
                            continue;
                        }
                        int limit_length = 0, element_length = element.Value.Length;
                        DataColInfo info = configs.DataColInfos.ElementAt(ind);
                        if (info.TypeString == typeof(decimal).ToString())
                        {
                            limit_length = info.Length;
                            string[] int_float = element.Value.Split('.');
                            if (int_float.Length == 2)
                            {
                                element_length = element_length - 1;
                            }
                            else if (int_float.Length != 1)
                            {
                                string[] logMessages = { "檔案: " + fileName, "Error tag: " + element.Name, "Event: decimal type tag error value : " + element.Value };
                                MainForm.Log(logMessages, fail_log_file);
                                return -1;
                            }
                        }
                        else if (info.TypeString == typeof(string).ToString())
                        {
                            if (info.IsByteLength)
                            {
                                element_length = Encoding.Default.GetByteCount(element.Value);
                            }
                            limit_length = configs.DataColInfos.ElementAt(ind).Length;
                        }

                        if (limit_length > 0 && element_length > limit_length)
                        {
                            string[] logMessages = { "檔案: " + fileName, "Error tag: " + element.Name, "Event: Value length over " + element.Value + " length " + element_length + " > " + limit_length };
                            MainForm.Log(logMessages, fail_log_file);
                            result = 1;
                        }
                        #endregion

                        if (dataRow[element.Name.ToString()].ToString() == "")
                        {
                            dataRow[element.Name.ToString()] = element.Value;
                        }
                        else
                        {
                            string[] logMessages = { "檔案: " + fileName, "Error tag: " + element.Name, "Event: duplicate " + element.Value };
                            MainForm.Log(logMessages, fail_log_file);
                            return -1;
                        }
                    }
                    catch
                    {
                        string[] logMessages = { "檔案: " + fileName, "Error tag: " + element.Name, "Event: type error " + element.Value + " isn't " + dataTable.Columns[element.Name.ToString()].DataType };
                        MainForm.Log(logMessages, fail_log_file);
                        return -1;
                    }

                }
                else
                {
                    fun_result = XMLTagsToRow(fileName, dataTable, dataRow, element, blackList);
                    if (fun_result == -1)
                    {
                        return -1;
                    }
                    else if (fun_result == 1)
                    {
                        result = fun_result;
                    }
                }
            }
            return result;
        }

        private int XMLTagsToRows(string fileName, DataTable dataTable, List<DataRow> dataRows, XElement rootElemnent, List<string> blackList = null)
        {
            int result = 0, fun_result;
            XMLDataConfig configs = dataConfigs[tableType];
            string fail_log_file = $"{mainForm.DestinationDirectory}/Logs/{configs.TypeName}_{MainForm.FailLogFile}";
            if (!rootElemnent.HasElements || dataRows.Count == 0)
                return 0;

            var elements = rootElemnent.Elements();
            foreach (var element in elements)
            {
                if (blackList != null && blackList.Contains(element.Name.ToString()))
                {
                    continue;
                }
                if (element.IsEmpty || element.Value == "")
                {
                    continue;
                }
                if (!element.HasElements)
                {
                    try
                    {
                        #region Check legal
                        int ind = dataTable.Columns.IndexOf(element.Name.ToString());
                        if (ind < 0)
                        {
                            Console.WriteLine("There is no column named " + element.Name);
                            continue;
                        }
                        int limit_length = 0, element_length = element.Value.Length;
                        DataColInfo info = dataConfigs[tableType].DataColInfos.ElementAt(ind);
                        if (info.TypeString == typeof(decimal).ToString())
                        {
                            limit_length = info.Length;
                            string[] int_float = element.Value.Split('.');
                            if (int_float.Length == 2)
                            {
                                element_length = element_length - 1;
                            }
                            else if (int_float.Length != 1)
                            {
                                string[] logMessages = { "檔案: " + fileName, "Error tag: " + element.Name, "Event: decimal type tag error value : " + element.Value };
                                MainForm.Log(logMessages, fail_log_file);
                                return -1;
                            }
                        }
                        else if (info.TypeString == typeof(string).ToString())
                        {
                            if (info.IsByteLength)
                            {
                                element_length = Encoding.Default.GetByteCount(element.Value);
                            }
                            limit_length = info.Length;
                        }

                        if (limit_length > 0 && element_length > limit_length)
                        {
                            string[] logMessages = { "檔案: " + fileName, "Error tag: " + element.Name, "Event: Value length over " + element.Value + " length " + element_length + " > " + limit_length };
                            MainForm.Log(logMessages, fail_log_file);
                            result = 1;
                        }
                        #endregion

                        foreach (var dataRow in dataRows)
                        {
                            if (dataRow[element.Name.ToString()].ToString() == "")
                            {

                                dataRow[element.Name.ToString()] = element.Value;
                            }
                            else
                            {
                                string[] logMessages = { "檔案: " + fileName, "Error tag: " + element.Name, "Event: duplicate " + element.Value };
                                MainForm.Log(logMessages, fail_log_file);
                                return -1;
                            }
                        }
                    }
                    catch
                    {
                        string[] logMessages = { "檔案: " + fileName, "Error tag: " + element.Name, "Event: type error " + element.Value + " isn't " + dataTable.Columns[element.Name.ToString()].DataType };
                        MainForm.Log(logMessages, fail_log_file);
                        return -1;
                    }


                }
                else
                {
                    fun_result = XMLTagsToRows(fileName, dataTable, dataRows, element, blackList);
                    if (fun_result == -1)
                    {
                        return -1;
                    }
                    else if (fun_result == 1)
                    {
                        result = fun_result;
                    }

                }
            }
            return result;
        }

    }
}