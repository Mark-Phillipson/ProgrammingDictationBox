using System;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using VoiceLauncher.Models;

namespace VoiceLauncher
{
    public partial class Categories : Form
    {
        private VoiceLauncherContext db;
        bool formIsClosed = false;
        public Categories()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            db = new VoiceLauncherContext();
            db.Categories.Where(v => v.CategoryType == "IntelliSense Command").OrderBy(o => o.CategoryName).Load();
            this.categoryBindingSource.DataSource = db.Categories.Local.ToBindingList();
            this.BackColor = Color.Black;
            this.ForeColor = Color.White;
            FontFamily fontFamily = new FontFamily("Calibri");
            Font font = new Font(fontFamily, (float)12, FontStyle.Regular, GraphicsUnit.Point);
            var style = new DataGridViewCellStyle
            { BackColor = Color.Black, ForeColor = Color.White, Font = font };

            categoryDataGridView.DefaultCellStyle = style;
            categoryDataGridView.ColumnHeadersDefaultCellStyle = style;
            categoryDataGridView.RowHeadersDefaultCellStyle = style;
            categoryDataGridView.RowsDefaultCellStyle = style;
            categoryDataGridView.EnableHeadersVisualStyles = false;
            customIntelliSensesDataGridView.DefaultCellStyle = style;
            customIntelliSensesDataGridView.ColumnHeadersDefaultCellStyle = style;
            customIntelliSensesDataGridView.RowHeadersDefaultCellStyle = style;
            customIntelliSensesDataGridView.RowsDefaultCellStyle = style;
            customIntelliSensesDataGridView.EnableHeadersVisualStyles = false;
            categoryBindingNavigator.BackColor = Color.FromArgb(38, 38, 38);
            categoryBindingNavigator.ForeColor = Color.White;
            BackColor = Color.FromArgb(100, 100, 100);
            ForeColor = Color.White;
            FilterTextBox.BackColor = Color.FromArgb(38, 38, 38);
            FilterTextBox.ForeColor = Color.White;
            FilterTextBox.BorderStyle = BorderStyle.FixedSingle;
            foreach (DataGridViewColumn column in categoryDataGridView.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
            //customIntelliSensesBindingSource.Sort = "LanguageID ASC, Display_Value ASC, SendKeys_Value ASC";
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            formIsClosed = true;
            this.db.Dispose();
        }

        private void categoryBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            foreach (var customIntelliSense in db.CustomIntelliSenses.Local.ToList())
            {
                if (customIntelliSense.Category == null)
                {
                    db.CustomIntelliSenses.Remove(customIntelliSense);
                }
            }
            db.SaveChanges();
            this.categoryDataGridView.Refresh();
            this.customIntelliSensesDataGridView.Refresh();
        }

        private void categoryDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs anError)
        {
            //MessageBox.Show("Error happened " + anError.Context.ToString());

            if (anError.Context == DataGridViewDataErrorContexts.Commit)
            {
                MessageBox.Show("Commit error");
            }
            if (anError.Context == DataGridViewDataErrorContexts.CurrentCellChange)
            {
                MessageBox.Show("Cell change");
            }
            if (anError.Context == DataGridViewDataErrorContexts.Parsing)
            {
                MessageBox.Show("parsing error");
            }
            if (anError.Context == DataGridViewDataErrorContexts.LeaveControl)
            {
                MessageBox.Show("leave control error");
            }

            if ((anError.Exception) is ConstraintException)
            {
                DataGridView view = (DataGridView)sender;
                view.Rows[anError.RowIndex].ErrorText = "an error";
                view.Rows[anError.RowIndex].Cells[anError.ColumnIndex].ErrorText = "an error";

                anError.ThrowException = false;
            }
        }

        private void customIntelliSensesDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs anError)
        {
            //MessageBox.Show("Error happened " + anError.Context.ToString());

            if (anError.Context == DataGridViewDataErrorContexts.Commit)
            {
                MessageBox.Show("Commit error");
            }
            if (anError.Context == DataGridViewDataErrorContexts.CurrentCellChange)
            {
                MessageBox.Show("Cell change");
            }
            if (anError.Context == DataGridViewDataErrorContexts.Parsing)
            {
                MessageBox.Show("parsing error");
            }
            if (anError.Context == DataGridViewDataErrorContexts.LeaveControl)
            {
                MessageBox.Show("leave control error");
            }

            if ((anError.Exception) is ConstraintException)
            {
                DataGridView view = (DataGridView)sender;
                view.Rows[anError.RowIndex].ErrorText = "an error";
                view.Rows[anError.RowIndex].Cells[anError.ColumnIndex].ErrorText = "an error";

                anError.ThrowException = false;
            }
        }

        private void Categories_Load(object sender, EventArgs e)
        {
            DataGridViewComboBoxColumn cboBoxColumn = (DataGridViewComboBoxColumn)customIntelliSensesDataGridView.Columns[0];
            db = new VoiceLauncherContext();
            db.Languages.OrderBy(o => o.LanguageName).Load();
            cboBoxColumn.DataSource = db.Languages.Local.ToBindingList();
            cboBoxColumn.DisplayMember = "LanguageName";  // the Name property in Choice class
            cboBoxColumn.ValueMember = "ID";  // ditto for the Value property        }
            foreach (DataGridViewColumn column in customIntelliSensesDataGridView.Columns)
            {
                if (column.Name.Contains("3"))
                {
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
                else
                {
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
            }
            customIntelliSensesDataGridView.Columns[0].HeaderText = "Language";
            customIntelliSensesDataGridView.Columns[2].HeaderText = "Display Value";
            customIntelliSensesDataGridView.Columns[3].HeaderText = "SendKeys Value";
            customIntelliSensesDataGridView.Columns[4].HeaderText = "Command Type";
            customIntelliSensesDataGridView.Columns[6].HeaderText = "Delivery Type";
            customIntelliSensesBindingSource.Sort = "LanguageID ASC, Display_Value ASC, SendKeys_Value ASC";
        }

        private void FilterTextBox_Leave(object sender, EventArgs e)
        {
            if (formIsClosed)
            {
                return;
            }
            if (String.IsNullOrWhiteSpace(FilterTextBox.Text) == false)
            {
                var filteredData = db.Categories.Local.ToBindingList()
                    .Where(v => v.CategoryName.Contains(FilterTextBox.Text));
                this.categoryBindingSource.DataSource = filteredData.Count() > 0 ? filteredData : filteredData.ToArray();
            }
            else
            {
                categoryBindingSource.DataSource = db.Categories.Local.ToBindingList();
            }
            categoryDataGridView.Refresh();
        }

        private void toolStripButtonRemoveFilter_Click(object sender, EventArgs e)
        {
            categoryBindingSource.DataSource = db.Categories.Local.ToBindingList();
        }

        private void FilterTextBox_Click(object sender, EventArgs e)
        {

        }
    }
}
