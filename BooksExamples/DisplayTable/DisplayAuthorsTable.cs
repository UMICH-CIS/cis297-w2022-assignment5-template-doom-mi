using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DisplayTable
{
    public partial class DisplayAuthorsTable : Form
    {

    private bool searchCheck = false;

    public DisplayAuthorsTable()
        {
            InitializeComponent();
        }

        //Entity Framework DbContext
        private BooksExamples.BooksEntities dbcontext = new BooksExamples.BooksEntities();
        //load data from database into DataGridView

        private void DisplayAuthorsTable_Load(object sender, EventArgs e)
        {
            if (!searchCheck)
            {
                //load Authors table ordered by LastName then FirstName
                dbcontext.Authors
                    .OrderBy(author => author.LastName)
                    .ThenBy(author => author.FirstName)
                    .Load();
                //specify datasource for authorBindingSource
                authorBindingSource.DataSource = dbcontext.Authors.Local;
            }

        }

        private void authorDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void authorBindingNavigator_RefreshItems(object sender, EventArgs e)
        {
            if (!searchCheck)
            {
                //load Authors table ordered by LastName then FirstName
                dbcontext.Authors
                .OrderBy(author => author.LastName)
                .ThenBy(author => author.FirstName)
                .Load();
                //specify datasource for authorBindingSource
                authorBindingSource.DataSource = dbcontext.Authors.Local;
            }
        }

        private void authorBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            Validate();
            authorBindingSource.EndEdit();
            try
            {
                dbcontext.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException)
            {
                MessageBox.Show("FirstName and LastName must contain values", "Entity Validation Exception");
            }
        }

        private void Reset_Click(object sender, EventArgs e)
        {
            searchCheck = true;
            authorBindingNavigator_RefreshItems(sender, e);
        }

        private void Search_Click(object sender, EventArgs e)
        {
            searchCheck = true;
            authorBindingSource.DataSource = dbcontext.Authors.Local
                .Where(author => author.LastName.StartsWith(textBox1.Text))
                .OrderBy(author => author.LastName)
                .ThenBy(author => author.FirstName);
            authorBindingSource.MoveFirst();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
