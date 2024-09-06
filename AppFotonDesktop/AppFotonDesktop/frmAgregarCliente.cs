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
    public partial class frmAgregarCliente : Form
    {
        private int ClientesID;
        public frmAgregarCliente()
        {
            InitializeComponent();
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btnTamaño_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
                WindowState = FormWindowState.Maximized;
            else
                WindowState = FormWindowState.Normal;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmAgregarCliente_Load(object sender, EventArgs e)
        {

        }

        public void CargarClientMod(int ID)
        {
            this.ClientesID = ID;
            using (Foton_databaseEntities1 BD = new Foton_databaseEntities1())
            {
                var consult = from c in BD.Clientes
                              where c.ID_cliente == this.ClientesID
                              select c;
                var juanamanso = consult.FirstOrDefault();
                if (juanamanso == null)
                {
                    this.textBox1.Text = juanamanso.Nombre;
                }
            }
        }

        private void GuardarClientMod()
        {
            using (Foton_databaseEntities1 BD = new Foton_databaseEntities1())
            {
                var consult = from c in BD.Clientes
                              where c.ID_cliente == this.ClientesID
                              select c;
                var juanamanso = consult.FirstOrDefault();
                if (juanamanso == null)
                {
                    NuevoCliente();
                }
            }
        }

        // Método para verificar si el email tiene un formato correcto
        private bool ComprobarEmail()
        {
            string email = this.textBox4.Text;

            // Verifica que el email contenga un '@' y un '.' después del '@'
            if (email.Contains("@") && email.IndexOf(".", email.IndexOf("@")) > email.IndexOf("@"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Método para verificar si el contacto es un número válido de 8 dígitos
        private bool ComprobarContacto()
        {
            string contacto = this.textBox5.Text;

            // Verifica que sea un número de 8 dígitos
            return Int64.TryParse(contacto, out long numeroConvertido) && contacto.Length == 10;
        }

        private void NuevoCliente()
        {
            using (Foton_databaseEntities1 DB = new Foton_databaseEntities1())
            {
                try
                {
                    var Cliente = new Clientes();

                    Cliente.Nombre = this.textBox1.Text;

                    // Verifica si el DNI es un número válido
                    if (int.TryParse(this.textBox2.Text, out int dni))
                    {
                        Cliente.DNI = dni;
                    }
                    else
                    {
                        MessageBox.Show("Ingrese un DNI válido.");
                        return;
                    }

                    Cliente.Domicilio = this.textBox3.Text;

                    // Verifica si el contacto es válido
                    if (ComprobarContacto())
                    {
                        Cliente.Contacto = Convert.ToInt64(textBox5.Text);
                    }
                    else
                    {
                        MessageBox.Show("Ingrese un número de contacto válido.");
                        return;
                    }

                    // Verifica si el email es válido
                    if (ComprobarEmail())
                    {
                        Cliente.Email = this.textBox4.Text.ToLower();
                    }
                    else
                    {
                        MessageBox.Show("Ingrese un email válido.");
                        return;
                    }

                    DB.Clientes.Add(Cliente);
                    DB.SaveChanges();
                    MessageBox.Show("Cliente agregado exitosamente.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR. Verifique los datos ingresados. Detalle: " + ex.Message);
                }
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            NuevoCliente();
        }
    }
}
