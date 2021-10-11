using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XMLToSQL
{
    public partial class MainForm : Form
    {
        int igFormWidth = new int();  //窗口寬度
        int igFormHeight = new int(); //窗口高度

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            if (igFormWidth == 0 || igFormHeight == 0) return;
            float fgWidthScaling = (float)this.Width / (float)igFormWidth; //寬度縮放比例
            float fgHeightScaling = (float)this.Height / (float)igFormHeight; //高度縮放比例
            SetControls(fgWidthScaling, fgHeightScaling, this);
        }

        //記錄控件集初始的 位置、大小、字體大小信息
        private void InitConTag(Control cons)
        {
            foreach (Control con in cons.Controls)
            {
                con.Tag = con.Width + "," + con.Height + "," + con.Left + "," + con.Top + "," + con.Font.Size;
                if (con.Controls.Count > 0)
                {
                    InitConTag(con);
                }
            }
        }

        private void SetControls(float widthScaling, float heightScaling, Control cons)
        {
            //遍歷窗體中的控制元件，重新設定控制元件的值
            foreach (Control con in cons.Controls)
            {
                //獲取控制元件的Tag屬性值，並分割後儲存字串陣列
                if (con.Tag != null)
                {
                    string[] mytag = con.Tag.ToString().Split(new char[] { ',' });
                    //根據窗體縮放的比例確定控制元件的值
                    con.Width = Convert.ToInt32(System.Convert.ToSingle(mytag[0]) * widthScaling);//寬度
                    con.Height = Convert.ToInt32(System.Convert.ToSingle(mytag[1]) * heightScaling);//高度
                    con.Left = Convert.ToInt32(System.Convert.ToSingle(mytag[2]) * widthScaling);//左邊距
                    con.Top = Convert.ToInt32(System.Convert.ToSingle(mytag[3]) * heightScaling);//頂邊距
                    //Single currentSize = System.Convert.ToSingle(mytag[4]) * heightScaling;//字型大小
                    //con.Font = new Font(con.Font.Name, currentSize, con.Font.Style, con.Font.Unit);
                    if (con.Controls.Count > 0)
                    {
                        SetControls(widthScaling, heightScaling, con);
                    }
                }
            }
        }
    }
}
