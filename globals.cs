using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace spider_1
{
    class globals
    {
        //通用类,所有的可以访问这些变量

        //存放个人头像
        public static Image personImage = null;
        //用不着,存放各类表查询对应的号码,通过抓包获取
        public static string gnkm = null;
        //学生课表的URL,之前的测试
        public static string urlCXKB;
        //默认的地址,登录地址
        public static string urlDefault;
        //动态码
        public static string dynamicCode;
        //viewstate码
        public static string viewstate;
        //这些是之前的测试
        public static string viewstate1;
        public static string viewstate2;
        public static string viewstate3;
        //默认的viewstate码
        public static string viewstatedefault;
        //登录用的地址
        public static string postUrl;
        //登录的请求正文
        public static string postData;
        //用户的级别
        public static string userLevel="%D1%A7%C9%FA";//默认为学生
        //登录后的主页
        public static string indexHtml;
        //个人头像的地址
        public static string personPicUrl;
        //用来测试
        public static string testUrl;
        //用户学号
        public static string userid;
        //用户姓名
        public static string username;
        //主页地址 用于 Get方法的 Referer
        public static string mainUrl;
        //验证码地址
        public static string picUrl;
        //测试用的Cookies
        public static string strCookies;
        //存放课表
        public static string KB;
        //存放考试
        public static string KS;
        //存放补考
        public static string BK;
        //存放培养计划
        public static string PYJH;
        //存放cookies
        public static CookieContainer requestCookies = null; //全局的request cookies
        public static CookieContainer responseCookies = null; //全局的response cookies
    }

}
