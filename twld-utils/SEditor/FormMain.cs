using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SEditor
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        // 編集フォルダ
        private string EditingFolder { get; set; } = "";

        // フォルダ選択ダイアログ
        private FolderSelectDialog folderSelectDialog;


        /// <summary>
        /// フォームが閉じられた時に通知を受け取る。
        /// </summary>
        /// <param name="sender">送信元オブジェクト</param>
        /// <param name="e">イベントオブジェクト</param>
        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Properties.Settings.Default.Save();
            }
            catch 
            {
                // Ignored.
            }
        }

        /// <summary>
        /// フォームが表示されたときに通知を受けとる。
        /// </summary>
        /// <param name="sender">送信元オブジェクト</param>
        /// <param name="e">イベントオブジェクト</param>
        private void OnFormShown(object sender, EventArgs e)
        {
            string dir = Properties.Settings.Default.LastOpenDirectory;
            if (!string.IsNullOrEmpty(dir))
            {
                try
                {
                    ProcessOpen(dir);
                }
                catch 
                {
                    // フォルダが移動されただけかもしれないので通知しない。
                }
            }
        }

        /// <summary>
        /// フォルダをオープンする。
        /// </summary>
        /// <param name="dir">フォルダ</param>
        private void ProcessOpen(string dir)
        {

        }



        /// <summary>
        /// オープンメニューが選択された
        /// </summary>
        /// <param name="sender">送信元オブジェクト</param>
        /// <param name="e">イベントオブジェクト</param>
        private void OnMenuItemOpenClick(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// クローズメニューが選択された
        /// </summary>
        /// <param name="sender">送信元オブジェクト</param>
        /// <param name="e">イベントオブジェクト</param>
        private void OnMenuItemExitClick(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// 保存メニューが選択された
        /// </summary>
        /// <param name="sender">送信元オブジェクト</param>
        /// <param name="e">イベントオブジェクト</param>
        private void OnMenuItemSaveClick(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(EditingFolder))
                {
                    if (folderSelectDialog == null)
                    {
                        folderSelectDialog = new FolderSelectDialog();
                    }
                    if (folderSelectDialog.ShowDialog(this) != DialogResult.OK)
                    {
                        return;
                    }
                    EditingFolder = folderSelectDialog.Path;
                    ProcessSave(EditingFolder);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error");
            }
        }

        private void ProcessSave(string dir)
        {

        }
    }
}
