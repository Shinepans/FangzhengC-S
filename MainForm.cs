using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace spider_1
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }


        //做一些预处理的工作
        private void MainForm_Load(object sender, EventArgs e)
        {
            globals.KS = GetWeb("http://jw1.hustwenhua.net/("+globals.dynamicCode+")/xskscx.aspx?xh=" + globals.userid + "&xm=" + globals.username + "&gnmkdm=N121602");
            if(globals.KS!=null)
            {
                MessageBox.Show("最近有考试哦!,请查看", "考试安排");
                KS ks1 = new KS();
                ks1.Show();
            }
           
            webBrowser1.Navigate("http://jw1.hustwenhua.net/(" + globals.dynamicCode + ")/xs_main.aspx?xh=" + globals.userid);
            //获取学生的姓名
            string temp=globals.indexHtml;
            string first = temp.Substring(temp.LastIndexOf("\"xhxm\">") +7 );
            string last = first.Substring(0, first.LastIndexOf("同学"));
            label1.Text += last;
            globals.username=last;


        }

        private string KBCX(string Url)
        {

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Method = "GET";

            //先前存放的Cookies
            //request.CookieContainer = globals.responseCookies;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader myreader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            string responseText = myreader.ReadToEnd();
            return responseText;

        }
        private void button1_Click(object sender, EventArgs e)
        {
            /*globals.urlCXKB = "http://jw1.hustwenhua.net/" + "(" + globals.dynamicCode + ")" + "/xskbcx.aspx?xh=" + globals.userid + "&xm=" + globals.username + "&gnmkdm=N121602";
            webBrowser1.DocumentText = GetWeb(globals.urlCXKB);*/
          /*  string postData = "__EVENTTARGET=&__EVENTARGUMENT=&__VIEWSTATE=dDwtMzg4NjMzNTA5O3Q8cDxsPFNvcnRFeHByZXM7c2ZkY2JrO2RnMztkeWJ5c2NqO1NvcnREaXJlO3hoO3N0cl90YWJfYmpnO2NqY3hfbHNiO3p4Y2pjeHhzOz47bDxrY21jO1xlO2JqZzsxO2FzYzsxMjAxMDUwMzExMDQ7emZfY3hjanRqXzEyMDEwNTAzMTEwNDs7MTs%2BPjtsPGk8MT47PjtsPHQ8O2w8aTw0PjtpPDEwPjtpPDE5PjtpPDI0PjtpPDMyPjtpPDM0PjtpPDM2PjtpPDM4PjtpPDQwPjtpPDQyPjtpPDQ0PjtpPDQ2PjtpPDQ4PjtpPDUyPjtpPDU0PjtpPDU2Pjs%2BO2w8dDx0PHA8cDxsPERhdGFUZXh0RmllbGQ7RGF0YVZhbHVlRmllbGQ7PjtsPFhOO1hOOz4%2BOz47dDxpPDE2PjtAPFxlOzIwMTUtMjAxNjsyMDE0LTIwMTU7MjAxMy0yMDIxOzIwMTMtMjAyMDsyMDEzLTIwMTk7MjAxMy0yMDE4OzIwMTMtMjAxNzsyMDEzLTIwMTY7MjAxMy0yMDE1OzIwMTMtMjAxNDsyMDEyLTIwMTM7MjAxMS0yMDEyOzIwMTAtMjAxMTsyMDA5LTIwMTA7MjAwOC0yMDA5Oz47QDxcZTsyMDE1LTIwMTY7MjAxNC0yMDE1OzIwMTMtMjAyMTsyMDEzLTIwMjA7MjAxMy0yMDE5OzIwMTMtMjAxODsyMDEzLTIwMTc7MjAxMy0yMDE2OzIwMTMtMjAxNTsyMDEzLTIwMTQ7MjAxMi0yMDEzOzIwMTEtMjAxMjsyMDEwLTIwMTE7MjAwOS0yMDEwOzIwMDgtMjAwOTs%2BPjs%2BOzs%2BO3Q8dDxwPHA8bDxEYXRhVGV4dEZpZWxkO0RhdGFWYWx1ZUZpZWxkOz47bDxrY3h6bWM7a2N4emRtOz4%2BOz47dDxpPDIwPjtAPOWFrOWFseWfuuehgOivvjvkuJPkuJrmoLjlv4Por7476YCa6K%2BG5b%2BF5L%2Bu6K%2B%2BO%2BS4k%2BS4muaKgOiDveivvjvpm4bkuK3lrp7ot7XmlZnlraY75q%2BV5Lia6K6%2B6K6hKOiuuuaWhyk76Leo5LiT5Lia6YCJ5L%2Bu6K%2B%2BO%2BWFrOWFsemAieS%2Fruivvjvlhbbku5bor77nqIs75Y%2Bw5rm%2B5Lqk5rWB5a2m5LmgO%2Be9kee7nOmAieS%2FruivvjvmgJ3mlL%2Flrp7ot7Xor7475LiT5Lia5Z%2B656GA6K%2B%2BO%2BS4k%2BS4muivvjvkuJPkuJrpgInkv67or7475a6e6Le16K%2B%2BO%2BS4k%2BS4muaWueWQkeivvjvlrp7ot7Xnjq%2FoioI75LiT5Lia5o%2BQ6auY6K%2B%2BO1xlOz47QDwxOzEwOzExOzEzOzE0OzE3OzE5OzI7MjI7MjM7MjQ7MjU7Mzs0OzU7Njs3Ozg7OTtcZTs%2BPjs%2BOzs%2BO3Q8cDxwPGw8VmlzaWJsZTs%2BO2w8bzxmPjs%2BPjs%2BOzs%2BO3Q8cDxwPGw8VmlzaWJsZTs%2BO2w8bzxmPjs%2BPjs%2BOzs%2BO3Q8cDxwPGw8VmlzaWJsZTs%2BO2w8bzxmPjs%2BPjs%2BOzs%2BO3Q8cDxwPGw8VGV4dDs%2BO2w8XGU7Pj47Pjs7Pjt0PHA8cDxsPFRleHQ7VmlzaWJsZTs%2BO2w85a2m5Y%2B377yaMTIwMTA1MDMxMTA0O288dD47Pj47Pjs7Pjt0PHA8cDxsPFRleHQ7VmlzaWJsZTs%2BO2w85aeT5ZCN77ya5r2Y5bCaO288dD47Pj47Pjs7Pjt0PHA8cDxsPFRleHQ7VmlzaWJsZTs%2BO2w85a2m6Zmi77ya5L%2Bh5oGv56eR5a2m5LiO5oqA5pyv5a2m6YOoO288dD47Pj47Pjs7Pjt0PHA8cDxsPFRleHQ7VmlzaWJsZTs%2BO2w85LiT5Lia77yaO288dD47Pj47Pjs7Pjt0PHA8cDxsPFRleHQ7VmlzaWJsZTs%2BO2w86L2v5Lu25bel56iLO288dD47Pj47Pjs7Pjt0PHA8cDxsPFRleHQ7PjtsPOS4k%2BS4muaWueWQkTo7Pj47Pjs7Pjt0PHA8cDxsPFRleHQ7VmlzaWJsZTs%2BO2w86KGM5pS%2F54%2Bt77ya6L2v5Lu2MTIzO288dD47Pj47Pjs7Pjt0PEAwPHA8cDxsPFZpc2libGU7PjtsPG88Zj47Pj47Pjs7Ozs7Ozs7Ozs%2BOzs%2BO3Q8O2w8aTwxPjtpPDM%2BO2k8NT47aTw3PjtpPDk%2BO2k8MTE%2BO2k8MTM%2BO2k8MTU%2BO2k8MTc%2BO2k8MTg%2BO2k8MTk%2BO2k8MjE%2BO2k8MjM%2BO2k8MjU%2BO2k8Mjc%2BO2k8Mjk%2BO2k8Mzc%2BO2k8NDM%2BO2k8NDU%2BO2k8NDY%2BOz47bDx0PHA8cDxsPFZpc2libGU7PjtsPG88Zj47Pj47Pjs7Pjt0PEAwPHA8cDxsPFZpc2libGU7PjtsPG88Zj47Pj47cDxsPHN0eWxlOz47bDxESVNQTEFZOm5vbmU7Pj4%2BOzs7Ozs7Ozs7Oz47Oz47dDw7bDxpPDEzPjs%2BO2w8dDxAMDw7Ozs7Ozs7Ozs7Pjs7Pjs%2BPjt0PHA8cDxsPFRleHQ7VmlzaWJsZTs%2BO2w86Iez5LuK5pyq6YCa6L%2BH6K%2B%2B56iL5oiQ57up77yaO288dD47Pj47Pjs7Pjt0PEAwPHA8cDxsPFBhZ2VDb3VudDtfIUl0ZW1Db3VudDtfIURhdGFTb3VyY2VJdGVtQ291bnQ7RGF0YUtleXM7PjtsPGk8MT47aTwxPjtpPDE%2BO2w8Pjs%2BPjtwPGw8c3R5bGU7PjtsPERJU1BMQVk6YmxvY2s7Pj4%2BOzs7Ozs7Ozs7Oz47bDxpPDA%2BOz47bDx0PDtsPGk8MT47PjtsPHQ8O2w8aTwwPjtpPDE%2BO2k8Mj47aTwzPjtpPDQ%2BO2k8NT47PjtsPHQ8cDxwPGw8VGV4dDs%2BO2w8ajA4NTAzMTs%2BPjs%2BOzs%2BO3Q8cDxwPGw8VGV4dDs%2BO2w8572R57uc6K6k6K%2BB77yIQ0NOQS9IM0Mv6L2v6ICD77yJ5Y%2BK572R57uc56ue6LWb5Z%2B56K6tOz4%2BOz47Oz47dDxwPHA8bDxUZXh0Oz47bDzlhazlhbHpgInkv67or747Pj47Pjs7Pjt0PHA8cDxsPFRleHQ7PjtsPDIuMDs%2BPjs%2BOzs%2BO3Q8cDxwPGw8VGV4dDs%2BO2w8MDs%2BPjs%2BOzs%2BO3Q8cDxwPGw8VGV4dDs%2BO2w85YWs6YCJ6K%2B%2BOz4%2BOz47Oz47Pj47Pj47Pj47dDxAMDxwPHA8bDxWaXNpYmxlOz47bDxvPGY%2BOz4%2BO3A8bDxzdHlsZTs%2BO2w8RElTUExBWTpub25lOz4%2BPjs7Ozs7Ozs7Ozs%2BOzs%2BO3Q8QDA8cDxwPGw8VmlzaWJsZTs%2BO2w8bzxmPjs%2BPjtwPGw8c3R5bGU7PjtsPERJU1BMQVk6bm9uZTs%2BPj47Ozs7Ozs7Ozs7Pjs7Pjt0PEAwPDs7Ozs7Ozs7Ozs%2BOzs%2BO3Q8QDA8cDxwPGw8VmlzaWJsZTs%2BO2w8bzxmPjs%2BPjtwPGw8c3R5bGU7PjtsPERJU1BMQVk6bm9uZTs%2BPj47Ozs7Ozs7Ozs7Pjs7Pjt0PEAwPHA8cDxsPFZpc2libGU7PjtsPG88Zj47Pj47cDxsPHN0eWxlOz47bDxESVNQTEFZOm5vbmU7Pj4%2BOzs7Ozs7Ozs7Oz47Oz47dDxAMDxwPHA8bDxWaXNpYmxlOz47bDxvPGY%2BOz4%2BOz47Ozs7Ozs7Ozs7Pjs7Pjt0PEAwPHA8cDxsPFZpc2libGU7PjtsPG88Zj47Pj47cDxsPHN0eWxlOz47bDxESVNQTEFZOm5vbmU7Pj4%2BOzs7Ozs7Ozs7Oz47Oz47dDxAMDxwPHA8bDxWaXNpYmxlOz47bDxvPGY%2BOz4%2BO3A8bDxzdHlsZTs%2BO2w8RElTUExBWTpub25lOz4%2BPjs7Ozs7Ozs7Ozs%2BOzs%2BO3Q8QDA8O0AwPDs7QDA8cDxsPEhlYWRlclRleHQ7PjtsPOWIm%2BaWsOWGheWuuTs%2BPjs7Ozs%2BO0AwPHA8bDxIZWFkZXJUZXh0Oz47bDzliJvmlrDlrabliIY7Pj47Ozs7PjtAMDxwPGw8SGVhZGVyVGV4dDs%2BO2w85Yib5paw5qyh5pWwOz4%2BOzs7Oz47Ozs%2BOzs7Ozs7Ozs7Pjs7Pjt0PHA8cDxsPFRleHQ7VmlzaWJsZTs%2BO2w85pys5LiT5Lia5YWxODfkuro7bzxmPjs%2BPjs%2BOzs%2BO3Q8cDxwPGw8VmlzaWJsZTs%2BO2w8bzxmPjs%2BPjs%2BOzs%2BO3Q8cDxwPGw8VmlzaWJsZTs%2BO2w8bzxmPjs%2BPjs%2BOzs%2BO3Q8cDxwPGw8VmlzaWJsZTs%2BO2w8bzxmPjs%2BPjs%2BOzs%2BO3Q8cDxwPGw8VGV4dDs%2BO2w8SEhYWTs%";*/
            

        }

        //Get方式获取网页数据
        private string GetWeb(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.Referer = "http://jw1.hustwenhua.net/("+globals.dynamicCode+"/xs_main.aspx?xh="+globals.userid;
            //先前存放的Cookies
            //request.CookieContainer = globals.responseCookies;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader myreader = new StreamReader(response.GetResponseStream(),Encoding.GetEncoding("gb2312"));
            string responseText = myreader.ReadToEnd();
            return responseText;
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            globals.KB = GetWeb("http://jw1.hustwenhua.net/("+globals.dynamicCode+")/xskbcx.aspx?xh=" + globals.userid + "&xm=" + globals.username + "&gnmkdm=N121602");
            KB kb = new KB();
            kb.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            globals.KS = GetWeb("http://jw1.hustwenhua.net/("+globals.dynamicCode+")/xskscx.aspx?xh=" + globals.userid + "&xm=" + globals.username + "&gnmkdm=N121603");
            KS ks = new KS();
            ks.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            globals.KB = GetWeb("http://jw1.hustwenhua.net/("+globals.dynamicCode+")/xsbkkscx.aspx?xh=" + globals.userid + "&xm=" + globals.username + "&gnmkdm=N121604");
            BK bk = new BK();
            bk.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            notify1 frm = new notify1();
            notify2 frm2 = new notify2();
            frm.Show();
            frm2.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            PersonPic pp = new PersonPic();
            pp.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            globals.PYJH = GetWeb("http://jw1.hustwenhua.net/("+globals.dynamicCode+")/pyjh.aspx?xh=" + globals.userid + "&xm=" + globals.username + "&gnmkdm=N121606");
            PYJH pyjh = new PYJH();
            pyjh.Show();
        }
      
    }
}
