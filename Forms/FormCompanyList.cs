using PruebaTecnica1.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PruebaTecnica1
{
    public partial class FormCompanyList : Form
    {
        private readonly AppDbContext _context = new AppDbContext();

        public FormCompanyList()
        {
            InitializeComponent();
            LoadCompanies();
        }

        private void LoadCompanies(string filter = "")
        {            
            filter = filter.ToLower();

            var companies = _context.Empresas
                .AsNoTracking()
                .Where(e => e.Nombre.ToLower().Contains(filter))
                .ToList();

            // Refresca el DataGridView
            dataGridView1.DataSource = null; 
            dataGridView1.DataSource = companies;

            // Oculta la columna ID
            if (dataGridView1.Columns["EmpresaID"] != null)
            {
                dataGridView1.Columns["EmpresaID"].Visible = false;
            }
        }
 

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            LoadCompanies(txtFilter.Text);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var editor = new FormCompanyEditor();

            editor.ShowDialog();

            LoadCompanies();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            // Validar si se ha seleccionado una sola fila
            if (dataGridView1.SelectedRows.Count == 1)
            {
                // Obtener el ID de la empresa seleccionada
                var id = (int)dataGridView1.SelectedRows[0].Cells["EmpresaID"].Value;
               
                var editor = new FormCompanyEditor(id);    
                
                editor.ShowDialog();
                                                  
                LoadCompanies();
                              
            }
            else
            {                
                MessageBox.Show("Seleccione solo una empresa para editar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                var id = (int)row.Cells["EmpresaID"].Value;
                var company = _context.Empresas.Find(id);
                if (company != null)
                    _context.Empresas.Remove(company);
            }
            _context.SaveChanges();
            LoadCompanies();
        }

        private void FormCompanyList_Load(object sender, EventArgs e)
        {

        }
       
    }
}
