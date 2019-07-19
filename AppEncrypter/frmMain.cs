using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppEncrypter
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void BtnCifrar_Click(object sender, EventArgs e)
        {
            txtTextoResultado.Text = Tools.Security.Encrypt(txtTextoOriginal.Text);
        }

        private void BtnDesencriptar_Click(object sender, EventArgs e)
        {
            txtTextoResultado.Text = Tools.Security.Decrypt(txtTextoOriginal.Text);
        }
    }
}
