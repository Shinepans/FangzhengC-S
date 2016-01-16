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
    public partial class PYJH : Form
    {
        public PYJH()
        {
            InitializeComponent();
        }

        private void PYJH_Load(object sender, EventArgs e)
        {
            webBrowser1.DocumentText = globals.PYJH;
        }
    }
}
