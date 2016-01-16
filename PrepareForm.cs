using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Net;
using System.IO; //正则表达式

namespace spider_1
{
    public partial class PrepareForm : Form
    {
        
        //通过百度找到的一下特殊符号对应的Ascii码

        //1. + URL 中+号表示空格 %2B 
        //2. 空格 URL中的空格可以用+号或者编码 %20 
        //3. / 分隔目录和子目录 %2F 
        //4. ? 分隔实际的 URL 和参数 %3F 
        //5. % 指定特殊字符 %25 
        //6. # 表示书签 %23 
        //7. & URL 中指定的参数间的分隔符 %26 
        //8. = URL 中指定参数的值 %3D 

        public PrepareForm()
        {
            InitializeComponent();
        }
        
        //预处理就是获取动态码了
        private void prepare_Load(object sender, EventArgs e)
        {
            webBrowser1.Navigate("http://jw1.hustwenhua.net");
        }
        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
        }


        //获取到动态码后来获取viewstate,这时候还没有经过处理
        private void button1_Click(object sender, EventArgs e)
        {
            //默认的登录地址

            string urldefault = webBrowser1.Url.ToString();
            globals.urlDefault = urldefault;

            //获得浏览器地址栏的动态码
            string first = urldefault.Substring(urldefault.LastIndexOf('(')+1);
            string last = first.Substring(0, first.LastIndexOf(')'));

            //截取到动态码,保存到globals里的dynamiccode中
            globals.dynamicCode = last;

            //创建一个字符串来保存主页的信息用来截取字符串
            string strHTML = "";
            WebClient myWebClient = new WebClient();
            Stream myStream = myWebClient.OpenRead("http://jw1.hustwenhua.net/" + "(" + globals.dynamicCode + ")" + "/" + "default2.aspx");
            StreamReader sr = new StreamReader(myStream, System.Text.Encoding.GetEncoding("utf-8"));
            strHTML = sr.ReadToEnd();
            myStream.Close();
            string temp;
            //获取viewstate码
            Regex my=new Regex(@"<input[^>]*name=\""__VIEWSTATE\""[^>]*value=\""([^""]*)\""[^>]*>");
            MatchCollection mymatch=my.Matches(strHTML);
              foreach (Match m in mymatch)
                {
                    
                       globals.viewstate=m.ToString();
                   }
              temp = globals.viewstate;

            //获取gnmkd码:


            globals.viewstate = globals.viewstate.Substring(globals.viewstate.LastIndexOf("value=") + 7);
            globals.viewstate = globals.viewstate.Substring(0,globals.viewstate.LastIndexOf("\""));
            globals.viewstatedefault = globals.viewstate;

            //因为服务器不能识别某些特殊字符,需要转换为Ascii码
            globals.viewstate = globals.viewstate.Replace("/", "%2F");
            globals.viewstate = globals.viewstate.Replace("+", "%2B");
            globals.viewstate = globals.viewstate.Replace("=", "%3D");



                globals.postUrl = "http://jw1.hustwenhua.net/" + "(" + globals.dynamicCode + ")" + "/" + "default2.aspx";
            //在信息栏显示信息
            richTextBox1.Text += "\n\n" + "动态码:\n\n" + globals.dynamicCode + "\n\nviewState码:\n\n" + globals.viewstate + "\n\n" + "已完成必须的步骤\n\n"+"请点击完成按钮继续下一步操作";
            globals.picUrl = "http://jw1.hustwenhua.net/" + "(" + globals.dynamicCode + ")" + "/" + "CheckCode.aspx";
            richTextBox1.Text += "\n\n" + globals.viewstate1 + "\n\n"+globals.viewstate2 +"\n\n"+globals.viewstate3;

            richTextBox1.Text += "\n\n处理前的viewstate:" + globals.viewstatedefault;
            richTextBox1.Text += "\n\n处理后的viewstate:" + globals.viewstate;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginForm frm1 = new LoginForm();
            frm1.Show();
        }
    }
}
