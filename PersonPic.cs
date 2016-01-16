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
    public partial class PersonPic : Form
    {
        public PersonPic()
        {
            InitializeComponent();
        }

        private void PersonPic_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = globals.personImage;
        }
    }
}
