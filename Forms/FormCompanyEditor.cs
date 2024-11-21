using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PruebaTecnica1.Forms
{
    public partial class FormCompanyEditor : Form
    {
        private readonly AppDbContext _context = new AppDbContext();
        private readonly int? _empresaId;

        public FormCompanyEditor(int? empresaId = null)
        {
            InitializeComponent();
            _empresaId = empresaId;

            if (_empresaId.HasValue)
            {
                // Si hay un ID es edición y se deben cargar los datos de la empresa.
                CargarEmpresa();
                this.Text = "Editando Empresa";
                lblTitulo.Text = "Editando Empresa";
            }
            else
            {
                // Si no hay un ID es creación de una nueva empresa.
                this.Text = "Creando nueva Empresa";
                lblTitulo.Text = "Creando nueva Empresa";
            }
        }

        private void CargarEmpresa()
        {
            var empresa = _context.Empresas.Find(_empresaId);
            if (empresa != null)
            {
                txtNombre.Text = empresa.Nombre;
                txtCodigo.Text = empresa.Codigo.ToString();
                txtDireccion.Text = empresa.Direccion;
                txtTelefono.Text = empresa.Telefono;
                txtCiudad.Text = empresa.Ciudad;
                txtDepartamento.Text = empresa.Departamento;
                txtPais.Text = empresa.Pais;
            }
            else
            {
                MessageBox.Show("La empresa no fue encontrada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Company empresa;
                if (_empresaId.HasValue)
                {
                    // Edición
                    empresa = _context.Empresas.Find(_empresaId);
                    if (empresa == null)
                    {
                        MessageBox.Show("La empresa no fue encontrada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    // Creación
                    empresa = new Company();
                    _context.Empresas.Add(empresa);
                }
                                
                empresa.Nombre = txtNombre.Text;
                empresa.Codigo = int.Parse(txtCodigo.Text);
                empresa.Direccion = txtDireccion.Text;
                empresa.Telefono = txtTelefono.Text;
                empresa.Ciudad = txtCiudad.Text;
                empresa.Departamento = txtDepartamento.Text;
                empresa.Pais = txtPais.Text;
                empresa.FechaUltimaModificacion = DateTime.Now;

                if (!_empresaId.HasValue) 
                {
                    empresa.FechaCreacion = DateTime.Now;
                }

                _context.SaveChanges();
                MessageBox.Show("Los cambios se guardaron correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormCompanyEditor_Load(object sender, EventArgs e)
        {

        }

        //Validación para controlar el campo codigo
        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8)
            {
                
                e.Handled = true;
            }
        }        
    }
}
