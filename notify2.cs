using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace spider_1
{
    public partial class notify2 : Form
    {
        public notify2()
        {
            InitializeComponent();
        }

        private void notify2_Load(object sender, EventArgs e)
        {
            webBrowser1.ScriptErrorsSuppressed = true;
            webBrowser1.Navigate("http://jw1.hustwenhua.net/(" + globals.dynamicCode + ")/ggsm.aspx?fbsj=2015-05-14 10:11:25&yxqx=2015-06-26&xh=7");
        }
    }
}
