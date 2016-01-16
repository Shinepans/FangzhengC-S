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
    public partial class KB : Form
    {
        public KB()
        {
            InitializeComponent();
        }

        private void KB_Load(object sender, EventArgs e)
        {
            webBrowser1.ScriptErrorsSuppressed = true;
            webBrowser1.DocumentText = globals.KB;
        }
    }
}
