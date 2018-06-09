using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 所有成语的列表
        /// </summary>
        public static List<string> allWordList;

        /// <summary>
        /// 当前图片识别后的列表
        /// </summary>
        public static List<string> imgWorldList;

        public static string currentImgPath;
        private void btnChooseImg_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = Application.StartupPath;
            DialogResult dr = openFileDialog1.ShowDialog(this);
            textImage.Text = openFileDialog1.FileName;
            if (dr == DialogResult.OK)
            {
                
                Thread worker = new Thread(delegate()
                {
                    ChooseImg();
                }
                );

                worker.IsBackground = true;
                worker.Start();
            }
        }

        private void ChooseImg()
        {
            try
            {
                ControlShow(pictureBox1, true);
                if (openFileDialog1.FileName != String.Empty)
                {
                    currentImgPath = openFileDialog1.FileName;
                }
                else
                {
                   currentImgPath= Application.StartupPath + "\\屏幕截图\\" + "currentCutImg.jpg";
                   //textImage.Text = currentImgPath;
                   UpdateTextBox(textImage, currentImgPath);
                }

                UpdatePictureBox(picSource, currentImgPath);
                imgWorldList = new List<string>();
                var results = Baidu.Aip.Demo.OcrDemo.GeneralBasic(textImage.Text);
                results = results.Replace("|", "");
                results = results.Replace("‖", "");
                for (int i = 0; i < results.Length; i++)
                {
                    if (results[i] >= 0x4e00 && results[i] <= 0x9fbb)
                    {
                        //是汉字
                        imgWorldList.Add(results[i].ToString());
                    }
                }

                UpdateTextBox(textResults, results);
                UpdateLable(label2, string.Format("已识别图片汉字列表，共计：{0}", imgWorldList.Count));
                search();
                ControlShow(pictureBox1, false);
            }
            catch (Exception ex)
            {
                UpdateTextBox(textResults, ex.Message + "\n\r" + ex.StackTrace);
            }
        }

        /// <summary>
        /// 检索出图片识别出字的所有成语集合
        /// 并且显示出来
        /// </summary>
        private void search()
        {
           RemoveAllListBoxItem(listBox1);
           Dictionary<string, double> matchWords = new Dictionary<string, double>();
            foreach (var imgItem in imgWorldList)
            {
                foreach (var allItem in allWordList)
                {
                    if (allItem.Contains(imgItem))
                    {
                        var ppl = calcPresent(allItem);
                        var text = ppl + " " + allItem;
                        if (ppl > 0.5 && !matchWords.ContainsKey(text))
                            matchWords.Add(text, ppl);
                    }
                }
            }
            var dicSort = from objDic in matchWords orderby objDic.Value descending select objDic;
            foreach (KeyValuePair<string, double> kvp in dicSort)
            {
                AddListBoxItem(listBox1, kvp.Key);
            }
           
        }
        /// <summary>
        /// 计算识别率
        /// </summary>
        private double calcPresent(string word)
        {
            double result = 0;
            string usedWords = "";//已经用过的字，不允许重复
            for (int i = 0; i < word.Length; i++)
            {
                foreach (var item in imgWorldList)
                {
                    if (item == word[i].ToString())
                    {
                        if (!usedWords.Contains(item))
                        {
                            result = result + 0.25;
                            usedWords += item;
                        }

                    }
                }

            }
            return result;
        }

        /// <summary>
        /// 更新Label文本
        /// </summary>
        /// <param name="lable"></param>
        /// <param name="text"></param>
        private void UpdateLable(Label lable, string text)
        {
            Action<string> actionDelegate = (x) =>
            {
                lable.Text = text;
            };
            lable.Invoke(actionDelegate, text);
        }

        private void UpdateTextBox(TextBox textbox, string text)
        {
            Action<string> actionDelegate = (x) =>
            {
                textbox.Text = text;
            };
            textbox.Invoke(actionDelegate, text);
        }

        private void UpdatePictureBox(PictureBox pictureBox, string path)
        {
            Action<string> actionDelegate = (x) =>
            {
                //pictureBox.Image = Image.FromFile(path);
                Stream s = File.Open(path, FileMode.Open);
                pictureBox.Image = Image.FromStream(s);
                s.Close();
            };
            pictureBox.Invoke(actionDelegate, path);
        }

        private void ControlShow(Control ctrl, bool show)
        {
            Action<bool> actionDelegate = (x) =>
            {
                ctrl.Visible = show;
            };
            ctrl.Invoke(actionDelegate, show);
        }

        private void AddListBoxItem(ListBox listBox, string item)
        {
            Action<string> actionDelegate = (x) =>
            {
                listBox.Items.Add(item);
            };
            listBox.Invoke(actionDelegate, item);
        }

        private void RemoveAllListBoxItem(ListBox listBox)
        {
            Action<string> actionDelegate = (x) =>
            {
                listBox.Items.Clear();
            };
            listBox.Invoke(actionDelegate,"");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// 加载txt到字典
        /// </summary>
        private void loadDict()
        {
            var path = Application.StartupPath + "\\成语列表.txt";
            allWordList = new List<string>();
            using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
            {
                string line = string.Empty;
                while ((line = sr.ReadLine()) != null)
                {
                    allWordList.Add(line);
                }
            }
        }

        /// <summary>
        /// 根据下载的TXT文档过滤后创建成语txt
        /// </summary>
        private void createWordTxt()
        {
            using (StreamReader sr = new StreamReader(Application.StartupPath + "\\成语大全（31648个成语解释）.Txt", System.Text.Encoding.Default))
            {
                string line = string.Empty;
                while ((line = sr.ReadLine()) != null)
                {
                    //如果读出一行不是空字符串
                    if (line.Trim() != "")
                    {
                        var arr = Regex.Split(line, "拼音");
                        //0功能>1URL>2请求方法>3请求body
                        var word = arr[0].Trim();
                        if (word.Length == 4)
                            CSHelper.saveTextFile(word, "成语列表", "txt", true);
                    }
                }
            }
        }

        [Flags]
        public enum KeyModifiers
        {
            None = 0,
            Alt = 1,
            Ctrl = 2,
            Shift = 4,
            WindowsKey = 8
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            uint ctrlHotKey = (uint)(KeyModifiers.Alt | KeyModifiers.Ctrl);
            // 注册热键为Alt+Ctrl+C, "100"为唯一标识热键
            HotKey.RegisterHotKey(Handle, 100, ctrlHotKey, Keys.C);

            Thread thread = new Thread(delegate()
            {
                ControlShow(pictureBox1, true);
                UpdateLable(label1, "正在加载成语列表...");
                loadDict();
                UpdateLable(label1, string.Format("当前成语列表有{0}个。", allWordList.Count));
                ControlShow(pictureBox1, false);
            });
            thread.IsBackground = true;
            thread.Start();
        }

        Cutter cutter = null;
        private void btnCutScreenImg_Click(object sender, EventArgs e)
        {
            // 新建一个和屏幕大小相同的图片
            Bitmap CatchBmp = new Bitmap(Screen.AllScreens[0].Bounds.Width, Screen.AllScreens[0].Bounds.Height);

            // 创建一个画板，让我们可以在画板上画图
            // 这个画板也就是和屏幕大小一样大的图片
            // 我们可以通过Graphics这个类在这个空白图片上画图
            Graphics g = Graphics.FromImage(CatchBmp);

            // 把屏幕图片拷贝到我们创建的空白图片 CatchBmp中
            g.CopyFromScreen(new Point(0, 0), new Point(0, 0), new Size(Screen.AllScreens[0].Bounds.Width, Screen.AllScreens[0].Bounds.Height));

            // 创建截图窗体
            cutter = new Cutter();

            // 指示窗体的背景图片为屏幕图片
            cutter.BackgroundImage = CatchBmp;
            // 显示窗体
            //cutter.Show();
            // 如果Cutter窗体结束，则从剪切板获得截取的图片，并显示在聊天窗体的发送框中
            if (cutter.ShowDialog() == DialogResult.OK)
            {
                if (!Directory.Exists( Application.StartupPath+"\\屏幕截图"))  //判断目录是否存在,不存在就创建
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(Application.StartupPath+"\\屏幕截图");
                    directoryInfo.Create();
                }

                IDataObject iData = Clipboard.GetDataObject();
                DataFormats.Format format = DataFormats.GetFormat(DataFormats.Bitmap);
                if (iData.GetDataPresent(DataFormats.Bitmap))
                {
                 var  photo = (Image)iData.GetData(typeof(Bitmap));

                   // richTextBox1.Paste(format);
                   currentImgPath=Application.StartupPath + "\\屏幕截图\\" + "currentCutImg.jpg";
                   //photo.Save(currentImgPath); //保存为文件  ,注意格式是否正确.

                   Bitmap B = new Bitmap(photo.Width, photo.Height); //新建一个理想大小的图像文件  
                   Graphics g1 = Graphics.FromImage(B);//实例一个画板的对象,就用上面的图像的画板  
                   g1.DrawImage(photo, 0, 0);//把目标图像画在这个图像文件的画板上  

                   B.Save(currentImgPath, System.Drawing.Imaging.ImageFormat.Jpeg);//通过这个图像来保存  

                   photo.Dispose();//关闭对象
                   B.Dispose();
                   CatchBmp.Dispose();
                   g1.Dispose();
                    // 清楚剪贴板的图片
                    Clipboard.Clear();
                    Thread t = new Thread(delegate()
                    {
                        ChooseImg();
                    });
                    t.IsBackground = true;
                    t.Start();
                    this.WindowState = FormWindowState.Normal;
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 卸载热键
            HotKey.UnregisterHotKey(Handle, 100);
        }
        // 热键按下执行的方法
        private void GlobalKeyProcess()
        {
            this.WindowState = FormWindowState.Minimized;
            // 窗口最小化也需要一定时间
            Thread.Sleep(200);
            btnCutScreenImg.PerformClick();
        }

        /// <summary>
        /// 重写WndProc()方法，通过监视系统消息，来调用过程
        /// 监视Windows消息
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            //如果m.Msg的值为0x0312那么表示用户按下了热键
            const int WM_HOTKEY = 0x0312;
            switch (m.Msg)
            {
                case WM_HOTKEY:
                    if (m.WParam.ToString() == "100")
                    {
                        GlobalKeyProcess();
                    }

                    break;
            }

            // 将系统消息传递自父类的WndProc
            base.WndProc(ref m);
        }
    }
}
