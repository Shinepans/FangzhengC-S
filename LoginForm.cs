using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace spider_1
{
    public partial class LoginForm : Form
    {
        //学生:%D1%A7%C9%FA
        //老师:%BD%CC%CA%A6
        //部门:%B2%BF%C3%C5
        //访客无意义

        public LoginForm()
        {
            InitializeComponent();
        }

        //登录Post方法
        private string Login(string Url, string postDataStr)
        {

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);

            request.Method = "POST";

            request.ContentType = "application/x-www-form-urlencoded";

            request.ContentLength = Encoding.UTF8.GetByteCount(postDataStr);

            Stream myRequestStream = request.GetRequestStream();

            StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding("gb2312"));

            myStreamWriter.Write(postDataStr);

            myStreamWriter.Close();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();

            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("gb2312"));

            string retString = myStreamReader.ReadToEnd();

            myStreamReader.Close();

            myResponseStream.Close();

            return retString;

        }


        private void GetImagePersonPic(string url)
        {

            //            CookieContainer cookies = new CookieContainer();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Accept = "*/*";
            request.Method = "GET";
            request.UserAgent = "Mozilla/5.0";

            request.CookieContainer = new CookieContainer();
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            MemoryStream ms = null;
            using (var stream = response.GetResponseStream())
            {
                Byte[] buffer = new Byte[response.ContentLength];
                int offset = 0, actuallyRead = 0;
                do
                {
                    actuallyRead = stream.Read(buffer, offset, buffer.Length - offset);
                    offset += actuallyRead;
                } while (actuallyRead > 0);
                ms = new MemoryStream(buffer);
            }
            response.Close();
            //            globals.cookies = request.CookieContainer;
            //            globals.strCookies = request.CookieContainer.GetCookieHeader(request.RequestUri);
            try
            {
                Image image = new Bitmap(ms, true);
                globals.personImage = image;
                pictureBox2.Image = image;
            }catch(Exception e)
            {
                MessageBox.Show("密码错误!");
                this.Hide();
                LoginForm frm = new LoginForm();
                frm.Show();

            }
            
        }

        //获取验证码方法
        private void GetImage(string url)
        {
            
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Accept = "*/*";
            request.Method = "GET";
            request.UserAgent = "Mozilla/5.0";
//            request.CookieContainer = new CookieContainer();
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            MemoryStream ms = null;
            using (var stream = response.GetResponseStream())
            {
                Byte[] buffer = new Byte[response.ContentLength];
                int offset = 0, actuallyRead = 0;
                do
                {
                    actuallyRead = stream.Read(buffer, offset, buffer.Length - offset);
                    offset += actuallyRead;
                } while (actuallyRead > 0);
                ms = new MemoryStream(buffer);
            }
            response.Close();
            Image image = new Bitmap(ms, true);
            pictureBox1.Image = image;
        }

       //LoginForm : Load 获取验证码
        private void Form1_Load(object sender, EventArgs e)
        {
            radioButton1.Checked = true;
            GetImage(globals.picUrl);
        }

        //点击登录
       private void button2_Click(object sender, EventArgs e)
       {

           //将用户Id放到globals类中的userid中
           globals.userid = userNameTextBox.Text.ToString();

           //将个人头像信息的地址放到globals类中的personPicUrl变量中
           globals.personPicUrl = "http://jw1.hustwenhua.net/" + "(" + globals.dynamicCode + ")" + "/" + "readimagexs.aspx?xh=" + userNameTextBox.Text.ToString();

           //准备postData
           globals.postData = "__VIEWSTATE=" + globals.viewstate + "&txtUserName=" + userNameTextBox.Text.ToString() + "&TextBox2=" + PassWordTextBox.Text.ToString() + "&txtSecretCode=" + secretCodeTextBox.Text.ToString() + "&RadioButtonList1=" + globals.userLevel + "&Button1=&lbLanguage=&hidPdrs=&hidsc=";

           //调用Login方法,将返回的主业信息放到IndexHtml里面
              globals.indexHtml = Login(globals.urlDefault, globals.postData);
              
           //展示个人照片
              GetImagePersonPic(globals.personPicUrl);


           //获取个人信息
              //获取学生的姓名
              string temp = globals.indexHtml;
              string first = temp.Substring(temp.LastIndexOf("\"xhxm\">") + 7);
              string last = first.Substring(0, first.LastIndexOf("同学"));
              globals.username = last;

           //提示登录成功

              this.Text = "登录成功,欢迎您:"+globals.username;

       }
  
        //老师按钮
      private void radioButton2_CheckedChanged(object sender, EventArgs e)
      {
          globals.userLevel = "%BD%CC%CA%A6";
      }
        //部门按钮
      private void radioButton3_CheckedChanged(object sender, EventArgs e)
      {
          globals.userLevel = "%B2%BF%C3%C5";
      }

      private void label4_Click(object sender, EventArgs e)
      {
          GetImage(globals.picUrl);
      }

      private void button1_Click(object sender, EventArgs e)
      {
          this.Hide();
          MainForm mfrm = new MainForm();
          mfrm.Show();
      }



    }
}

