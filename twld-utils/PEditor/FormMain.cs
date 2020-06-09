using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PEditor
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            InitializeDataGridView();
        }

        /// <summary>
        /// データグリッドビューを初期化する。
        /// </summary>
        private void InitializeDataGridView()
        {
            // 空のモデルを作って設定するのと、カラムを1つ用意するだけ。
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn() { DataType = typeof(int), ColumnName = "id" });
            dataGridView.DataSource = dt;            
        }

        /// <summary>
        /// 編集中のファイルパス
        /// </summary>
        private string EditingFilePath { get; set; } = null;

        /// <summary>
        /// クローズメニューが選択された時に処理を行う。
        /// </summary>
        /// <param name="sender">送信元オブジェクト</param>
        /// <param name="e">イベントオブジェクト</param>
        private void OnMenuItemExitClick(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// オープンメニューが選択された時の処理を行う。
        /// </summary>
        /// <param name="sender">送信元オブジェクト</param>
        /// <param name="e">イベントオブジェクト</param>
        private void OnMenuItemOpenClick(object sender, EventArgs e)
        {
            openFileDialog.InitialDirectory = Properties.Settings.Default.LastOpenDirectory;
            openFileDialog.FileName = "Profiles.json";
            if (openFileDialog.ShowDialog(this) != DialogResult.OK)
            {
                return;
            } 
            try
            {
                string path = openFileDialog.FileName;
                this.EditingFilePath = path;
                Properties.Settings.Default.LastOpenDirectory = System.IO.Path.GetDirectoryName(path);

                // TODO :読み出し処理
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, Properties.Resources.CaptionError);
            }


        }

        /// <summary>
        /// 保存メニューが選択された時の処理を行う。
        /// </summary>
        /// <param name="sender">送信元オブジェクト</param>
        /// <param name="e">イベントオブジェクト</param>
        private void OnMenuItemSaveClick(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(EditingFilePath))
                {
                    ProcessSaveAs();
                }
                else
                {
                    ProcessSave(EditingFilePath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, Properties.Resources.CaptionError);
            }
        }

        /// <summary>
        /// 名前をつけて保存メニューが選択された時の処理を行う。
        /// </summary>
        /// <param name="sender">送信元オブジェクト</param>
        /// <param name="e">イベントオブジェクト</param>
        private void OnMenuItemSaveAsClick(object sender, EventArgs e)
        {
            try
            {
                ProcessSaveAs();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, Properties.Resources.CaptionError);
            }

        }

        /// <summary>
        /// 名前をつけて保存処理する。
        /// </summary>
        private void ProcessSaveAs()
        {
            // ファイル選択させる。
            saveFileDialog.InitialDirectory = Properties.Settings.Default.LastOpenDirectory;
            saveFileDialog.FileName = "Profile.json";
            if (saveFileDialog.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            string path = saveFileDialog.FileName;
            Properties.Settings.Default.LastOpenDirectory = System.IO.Path.GetDirectoryName(path);
            ProcessSave(path);
        }

        /// <summary>
        /// 保存処理する。
        /// </summary>
        /// <param name="path">ファイルパス</param>
        private void ProcessSave(string path)
        {
            // TODO : 保存する。
        }

        private void OnButtonAddClick(object sender, EventArgs e)
        {
        }

        private void OnButtonDeleteClick(object sender, EventArgs e)
        {
        }
    }
}
