using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;
using VoiceLauncher.Models;

namespace VoiceLauncher
{
    public partial class LauncherForm : Form
    {
        readonly VoiceLauncherContext db;
        public LauncherForm()
        {
            InitializeComponent();
            db = new VoiceLauncherContext();
            Text = "Launcher Items";
        }

        private void LauncherForm_Load(object sender, EventArgs e)
        {
            db.Launchers.OrderBy(v => v.Category.CategoryName).ThenBy(v => v.Name).Load();
            db.Categories.Where(v => v.CategoryType == "Launch Applications").OrderBy(o => o.CategoryName).Load();
            ToolStripComboBox comboBox = this.toolStripComboBoxFilterByCategory;
            comboBox.ComboBox.DataSource = db.Categories.Local.ToBindingList();
            comboBox.ComboBox.DisplayMember = "CategoryName";
            comboBox.ComboBox.ValueMember = "ID";
            DataGridViewComboBoxColumn cboBoxColumn = null;
            cboBoxColumn = (DataGridViewComboBoxColumn)launcherDataGridView.Columns["dataGridViewComboBoxColumn4"];
            cboBoxColumn.DataSource = db.Categories.Local.ToBindingList();
            cboBoxColumn.DisplayMember = "CategoryName";
            cboBoxColumn.ValueMember = "ID";
            cboBoxColumn = (DataGridViewComboBoxColumn)launcherDataGridView.Columns[4];
            db.Computers.OrderBy(v => v.ComputerName).Load();
            cboBoxColumn.DataSource = db.Computers.Local.ToBindingList();
            cboBoxColumn.DisplayMember = "ComputerName";
            cboBoxColumn.ValueMember = "ID";
            foreach (DataGridViewColumn column in launcherDataGridView.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            launcherBindingSource.DataSource = db.Launchers.Local.ToBindingList();

            launcherDataGridView.Refresh();
        }

        private void launcherBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            launcherBindingSource.EndEdit();
            db.SaveChanges();
            launcherDataGridView.Refresh();
            Text = $"Saved Successfully at {DateTime.Now.ToShortTimeString()}";
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            //launcherDataGridView.ClearSelection();
            //launcherBindingSource.AddNew();
            //int rowIndex = launcherDataGridView.Rows.Count - 1;
            //launcherDataGridView.Rows[rowIndex].Cells[1].Selected = true;
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            if (launcherBindingSource.Current != null)
            {
                var current = (Launcher)this.launcherBindingSource.Current;
                launcherBindingSource.RemoveCurrent();
                db.Launchers.Local.Remove(current);
            }
        }

        private void toolStripTextBoxSearch_Leave(object sender, EventArgs e)
        {
            IEnumerable<Launcher> filteredData = null;
            if (string.IsNullOrEmpty(toolStripTextBoxSearch.Text))
            {
                bindingNavigatorDeleteItem.Enabled = true;
                filteredData = db.Launchers.Local.ToBindingList();
            }
            else if (toolStripTextBoxSearch.Text.Length > 0)
            {
                bindingNavigatorDeleteItem.Enabled = false;
                filteredData = db.Launchers.Local.ToBindingList().Where(v => v.Name.Contains(toolStripTextBoxSearch.Text) || v.Category.CategoryName.Contains(toolStripTextBoxSearch.Text) || v.CommandLine.Contains(toolStripTextBoxSearch.Text));
            }
            if (filteredData == null)
            {
                bindingNavigatorDeleteItem.Enabled = true;
                return;
            }
            launcherBindingSource.DataSource = filteredData.Count() > 0 ? filteredData : filteredData.ToArray();
        }

        private void launcherDataGridView_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            //e.Row.Cells["Name"].Value = "Test";
            //e.Row.Cells["CommandLine"].Value = "Test";
            //e.Row.Cells["CategoryID"].Value = "1";
            //e.Row.Cells["ComputerID"].Value = "1";
        }

        private void launcherDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //Without this event errors are shown about the ComboBox being value invalid
            //MessageBox.Show(e.Exception.Message);
        }

        private void toolStripComboBoxFilterByCategory_Leave(object sender, EventArgs e)
        {
            IEnumerable<Launcher> filteredData = null;
            if (string.IsNullOrEmpty(toolStripComboBoxFilterByCategory.Text))
            {
                this.bindingNavigatorDeleteItem.Enabled = true;
                filteredData = db.Launchers.Local.ToBindingList();
            }
            else if (toolStripComboBoxFilterByCategory.Text.Length > 0)
            {
                this.bindingNavigatorDeleteItem.Enabled = false;
                var category = db.Categories.Where(v => v.CategoryName == toolStripComboBoxFilterByCategory.Text).FirstOrDefault();
                filteredData = db.Launchers.Local.ToBindingList().Where(v => v.CategoryID == category.ID);
            }
            if (filteredData == null)
            {
                bindingNavigatorDeleteItem.Enabled = true;
                return;
            }
            launcherBindingSource.DataSource = filteredData.Count() > 0 ? filteredData : filteredData.ToArray();
        }
    }
}