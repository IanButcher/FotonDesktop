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
    public partial class frmClientes : Form
    {
        public frmClientes()
        {
            InitializeComponent();
        }

        private void btnMnimizar_Click(object sender, EventArgs e)
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

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmAgregarCliente AC = new frmAgregarCliente();
            AC.Show();
        }

        private void cargarProd()
        {
            cmbID.ValueMember = "id";
            cmbID.DisplayMember = "Text";
            cmbID.Text = "Seleccione el tipo de producto";
            //using (Foton_databaseEntities1 BD = new Foton_databaseEntities1())
            {
             
                //var consulta = from p in BD.Clientes
                  //             orderby p.Nombre

            }
        }

        private void Buscar()
        {
            // Verificar que se haya seleccionado un valor en el ComboBox
            if (cmbID.SelectedItem == null)
            {
                MessageBox.Show("Seleccione un criterio de búsqueda.");
                return;
            }

            string valorCMB = cmbID.SelectedItem.ToString();

            using (Foton_databaseEntities1 BD = new Foton_databaseEntities1())
            {
                IQueryable<dynamic> Consulta = null;

                // Mostrar todos los clientes si el TextBox está vacío
                if (string.IsNullOrEmpty(textBox1.Text))
                {
                    Consulta = from p in BD.Clientes
                               where p.DiscontinueDate == null
                               orderby p.Nombre
                               select new
                               {
                                   ID = p.ID_cliente,
                                   Nombre = p.Nombre,
                                   DNI = p.DNI,
                                   Domicilio = p.Domicilio,
                                   Contacto = p.Contacto,
                                   Email = p.Email
                               };
                }
                else
                {
                    switch (valorCMB)
                    {
                        case "ID_Cliente":
                            if (long.TryParse(textBox1.Text.Trim(), out long idValue))
                            {
                                Consulta = from p in BD.Clientes
                                           where p.ID_cliente == idValue && p.DiscontinueDate == null
                                           orderby p.Nombre
                                           select new
                                           {
                                               ID = p.ID_cliente,
                                               Nombre = p.Nombre,
                                               DNI = p.DNI,
                                               Domicilio = p.Domicilio,
                                               Contacto = p.Contacto,
                                               Email = p.Email
                                           };
                            }
                            else
                            {
                                MessageBox.Show("Ingrese un ID válido.");
                                return;
                            }
                            break;

                        case "Nombre":
                            Consulta = from p in BD.Clientes
                                       where p.Nombre.Contains(textBox1.Text.Trim()) && p.DiscontinueDate == null
                                       orderby p.Nombre
                                       select new
                                       {
                                           ID = p.ID_cliente,
                                           Nombre = p.Nombre,
                                           DNI = p.DNI,
                                           Domicilio = p.Domicilio,
                                           Contacto = p.Contacto,
                                           Email = p.Email
                                       };
                            break;

                        case "DNI":
                            if (long.TryParse(textBox1.Text.Trim(), out long dniValue))
                            {
                                Consulta = from p in BD.Clientes
                                           where p.DNI == dniValue && p.DiscontinueDate == null
                                           orderby p.Nombre
                                           select new
                                           {
                                               ID = p.ID_cliente,
                                               Nombre = p.Nombre,
                                               DNI = p.DNI,
                                               Domicilio = p.Domicilio,
                                               Contacto = p.Contacto,
                                               Email = p.Email
                                           };
                            }
                            else
                            {
                                MessageBox.Show("Ingrese un DNI válido.");
                                return;
                            }
                            break;

                        case "Domicilio":
                            Consulta = from p in BD.Clientes
                                       where p.Domicilio.Contains(textBox1.Text.Trim()) && p.DiscontinueDate == null
                                       orderby p.Nombre
                                       select new
                                       {
                                           ID = p.ID_cliente,
                                           Nombre = p.Nombre,
                                           DNI = p.DNI,
                                           Domicilio = p.Domicilio,
                                           Contacto = p.Contacto,
                                           Email = p.Email
                                       };
                            break;

                        case "Contacto":
                            Consulta = from p in BD.Clientes
                                       where p.Contacto.ToString().Contains(textBox1.Text.Trim()) && p.DiscontinueDate == null
                                       orderby p.Nombre
                                       select new
                                       {
                                           ID = p.ID_cliente,
                                           Nombre = p.Nombre,
                                           DNI = p.DNI,
                                           Domicilio = p.Domicilio,
                                           Contacto = p.Contacto,
                                           Email = p.Email
                                       };
                            break;

                        case "Email":
                            Consulta = from p in BD.Clientes
                                       where p.Email.Contains(textBox1.Text.Trim()) && p.DiscontinueDate == null
                                       orderby p.Nombre
                                       select new
                                       {
                                           ID = p.ID_cliente,
                                           Nombre = p.Nombre,
                                           DNI = p.DNI,
                                           Domicilio = p.Domicilio,
                                           Contacto = p.Contacto,
                                           Email = p.Email
                                       };
                            break;

                        default:
                            MessageBox.Show("Seleccione un criterio de búsqueda válido.");
                            return;
                    }
                }

                var resultados = Consulta.ToList();

                if (resultados.Count == 0)
                {
                    MessageBox.Show("No se encontraron resultados.");
                }
                else
                {
                    this.dgvClientes.DataSource = resultados;
                }
            }
        }



        private void btnBuscar_Click(object sender, EventArgs e)
        {
            // Llamar a la función Buscar cuando se hace clic en el botón
            Buscar();
        }

        private void Eliminar()
        {
            int selectedRow = (int)this.dgvClientes.SelectedRows[0].Cells[1].Value;
            try
            {
                using (Foton_databaseEntities1 BD = new Foton_databaseEntities1())
                {
                    var consult = from c in BD.Clientes
                                  where c.ID_cliente == selectedRow
                                  select c;
                    var juan = consult.FirstOrDefault();
                    if (juan != null)
                    {
                        juan.DiscontinueDate = true;
                        BD.SaveChanges();
                    }
                }
            }
            catch
            {
                MessageBox.Show("No se ha podido eliminar el cliente");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Eliminar();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            int selectedRow = (int)this.dgvClientes.SelectedRows[0].Cells[1].Value;
            using (frmAgregarCliente formul = new frmAgregarCliente())
            {
                formul.CargarClientMod(selectedRow);
                formul.ShowDialog();
            }
        }
    }
}
