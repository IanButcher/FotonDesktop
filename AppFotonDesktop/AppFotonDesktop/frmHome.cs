using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppFotonDesktop
{
    public partial class Form1 : Form
    {
     
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void iconPictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void iconPictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void btnTamaño_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
                WindowState = FormWindowState.Maximized;
            else
                WindowState = FormWindowState.Normal;
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btnVentas_Click(object sender, EventArgs e)
        {
            frmVentas Ventas = new frmVentas();

            // Mostrar el segundo formulario
            Ventas.Show();
        }

        private void btnDispositivos_Click(object sender, EventArgs e)
        {
            frmDispostivo Dispo = new frmDispostivo();
            Dispo.Show();
        }

        private void btnVendedores_Click(object sender, EventArgs e)
        {
            frmVendedores Vendedores = new frmVendedores();
            Vendedores.Show();
        }

        private void panelLateral_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            frmClientes C = new frmClientes();
            C.Show();
        }

        private void panelTop_Paint(object sender, PaintEventArgs e)
        {

        }

        
    }
}
