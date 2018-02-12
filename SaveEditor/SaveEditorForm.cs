using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SaveEditor
{
    public partial class SaveEditorForm : Form
    {
        public SaveEditorForm()
        {
            InitializeComponent();
        }

        QuickSave mQuickSave;
        string mCurrentFilePath;
        string mDialogFilter = "Abe's Exoddus Save (*.sav)|*.sav|Abe's Exoddus NxtP (*.nxtp)|*.nxtp";

        private void SaveEditorForm_Load(object sender, EventArgs e)
        {
            saveToolStripMenuItem.Enabled = false;
            saveAsToolStripMenuItem.Enabled = false;
            reloadToolStripMenuItem.Enabled = false;
        }

        void OpenFile(string filePath)
        {
            try
            {
                var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                mQuickSave = new QuickSave(fs);
                propertyGrid1.SelectedObject = mQuickSave;
                fs.Close();
                saveToolStripMenuItem.Enabled = true;
                saveAsToolStripMenuItem.Enabled = true;
                reloadToolStripMenuItem.Enabled = true;
                mCurrentFilePath = filePath;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = mDialogFilter;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                OpenFile(dialog.FileName);
            }
        }
        
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fs = new FileStream(mCurrentFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            mQuickSave.SerializeSave(fs);
            fs.Close();
        }

        private void SaveEditorForm_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        private void SaveEditorForm_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files)
            {
                OpenFile(file);
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = mDialogFilter;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                mQuickSave.SerializeSave(new FileStream(dialog.FileName, FileMode.OpenOrCreate, FileAccess.ReadWrite));
            }
        }

        private void reloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFile(mCurrentFilePath);
        }
    }
}
