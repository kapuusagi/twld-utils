﻿using MVUtils;
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
        // アイテムリスト画面
        private FormSelectableItemList itemListForm;
        // 選択アイテムリスト
        private List<IItem> selectableItemList = new List<IItem>();
        // 名前取得リスト
        private List<DataItem> items = new List<DataItem>();
        // 名前取得リスト
        private List<DataWeapon> weapons = new List<DataWeapon>();
        // 名前取得リスト
        private List<DataArmor> armors = new List<DataArmor>();

        /// <summary>
        /// 編集しているリスト
        /// </summary>
        private List<DataShop> Shops { get; set; } = new List<DataShop>();

        // 編集フォルダ
        private string EditingFolder { get; set; } = "";

        // フォルダ選択ダイアログ
        private FolderSelectDialog folderSelectDialog;


        public FormMain()
        {
            InitializeComponent();
            InitializeShopDataTable();
            InitializeItemListDataTable();
        }


        private void InitializeShopDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn()
            {
                DataType = typeof(string),
                ColumnName = "id",
            });
            dataGridViewShops.DataSource = dt;

            Shops.Add(null);
            Shops.Add(new DataShop() { Id = 1 });
        }

        private void InitializeItemListDataTable()
        {
            DataTable dt = new DataTable();
            // アイテム名
            dt.Columns.Add(new DataColumn()
            {
                DataType = typeof(string),
                ColumnName = "ItemName",
                ReadOnly = true,
            });
            dt.Columns.Add(new DataColumn()
            {
                DataType = typeof(int),
                ColumnName = "Min",
            });
            dt.Columns.Add(new DataColumn()
            {
                DataType = typeof(int),
                ColumnName = "Max"
            });
            dt.Columns.Add(new DataColumn()
            {
                DataType = typeof(string),
                ColumnName = "Condition"
            });
            dataGridViewItems.DataSource = dt;

            dataGridViewItems.Columns[0].Width = 240;
            dataGridViewItems.Columns[1].Width = 48;
            dataGridViewItems.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewItems.Columns[2].Width = 48;
            dataGridViewItems.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewItems.Columns[3].Width = 360;
        }

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
                    EditingFolder = dir;
                }
                catch 
                {
                    // フォルダが移動されただけかもしれないので通知しない。
                }
            }
            else
            {
                ModelToUI();
            }
        }

        /// <summary>
        /// フォルダをオープンする。
        /// </summary>
        /// <param name="dir">フォルダ</param>
        private void ProcessOpen(string dir)
        {
            selectableItemList.Clear();
            items.Clear();
            weapons.Clear();
            armors.Clear();

            ReadDataFiles(dir);

            AddSelectableItems(selectableItemList, items);
            AddSelectableItems(selectableItemList, weapons);
            AddSelectableItems(selectableItemList, armors);
            if (itemListForm != null)
            {
                itemListForm.SetItemList(selectableItemList);
            }

            ModelToUI();
        }

        /// <summary>
        /// モデルをUIに反映させる。
        /// </summary>
        private void ModelToUI()
        {
            DataTable dt = (DataTable)(dataGridViewShops.DataSource);
            dt.Clear();

            for (int i = 1; i < Shops.Count; i++)
            {
                var row = dt.NewRow();
                var shop = Shops[i];
                row[0] = string.Format("{0,4:D}:{1}", shop.Id, shop.Name);
                dt.Rows.Add(row);
            }

        }

        /// <summary>
        /// データファイルを読み込む。
        /// 
        /// </summary>
        /// <param name="dir">フォルダ</param>
        private void ReadDataFiles(string dir) { 
            string itemsPath = System.IO.Path.Combine(dir, "Items.json");
            if (System.IO.File.Exists(itemsPath))
            {
                items = DataItemListParser.Read(itemsPath);
            }

            string weaponsPath = System.IO.Path.Combine(dir, "Weapons.json");
            if (System.IO.File.Exists(weaponsPath))
            {
                weapons = DataWeaponListParser.Read(weaponsPath);
            }

            string armorsPath = System.IO.Path.Combine(dir, "Armors.json");
            if (System.IO.File.Exists(armorsPath))
            {
                armors = DataArmorListParser.Read(armorsPath);
            }

            Shops.Clear();

            string shopPath = System.IO.Path.Combine(dir, "Shops.json");
            if (System.IO.File.Exists(shopPath))
            {
                Shops = DataShopListReader.Read(shopPath);
                if (!Shops.Contains(null))
                {
                    Shops.Add(null);
                }
                Shops.Sort((a, b) => {
                    if (a == null)
                    {
                        return -1;
                    }
                    else if (b == null)
                    {
                        return 1;
                    }
                    else
                    {
                        return a.Id - b.Id;
                    }
                });
            }
            else
            {
                Shops.Add(null);
                Shops.Add(new DataShop() { Id = 1 });
            }
        }

        /// <summary>
        /// 選択アイテムを追加する。
        /// 名前未設定だとか、nullなエントリは表示しないようにする。
        /// </summary>
        /// <param name="list">追加するリスト</param>
        /// <param name="sourceList">追加元のリスト</param>
        private void AddSelectableItems(List<IItem> list, System.Collections.IList sourceList)
        {
            foreach (IItem item in sourceList)
            {
                if (item == null)
                {
                    continue;
                }
                if (string.IsNullOrEmpty(item.Name))
                {
                    continue;
                }
                list.Add(item);
            }
        }


        /// <summary>
        /// オープンメニューが選択された
        /// </summary>
        /// <param name="sender">送信元オブジェクト</param>
        /// <param name="e">イベントオブジェクト</param>
        private void OnMenuItemOpenClick(object sender, EventArgs e)
        {
            try
            {
                if (folderSelectDialog == null)
                {
                    folderSelectDialog = new FolderSelectDialog();
                }
                folderSelectDialog.Path = Properties.Settings.Default.LastOpenDirectory;
                if (folderSelectDialog.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }
                string dir = folderSelectDialog.Path;
                Properties.Settings.Default.LastOpenDirectory = dir;
                ProcessOpen(dir);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error");
            }
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
                    string dir = folderSelectDialog.Path;
                    Properties.Settings.Default.LastOpenDirectory = dir;
                    EditingFolder = dir;
                }
                ProcessSave(EditingFolder);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error");
            }
        }

        /// <summary>
        /// ショップデータを書き出す。
        /// </summary>
        /// <param name="dir">フォルダ</param>
        private void ProcessSave(string dir)
        {
            DataShopListWriter.Write(dir, Shops);
        }

        /// <summary>
        /// 店数変更処理されたときに通知を受け取る。
        /// </summary>
        /// <param name="sender">送信元オブジェクト</param>
        /// <param name="e">イベントオブジェクト</param>
        private void OnButtonChangeShopCountClick(object sender, EventArgs e)
        {
            FormNumberInput form = new FormNumberInput();
            form.Number = Shops.Count - 1;
            if (form.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            int newCount = form.Number + 1;
            if (newCount >= Shops.Count)
            {
                while (Shops.Count < newCount)
                {
                    int id = Shops.Count;
                    Shops.Add(new DataShop() { Id = id });
                }
            }
            else if (newCount < Shops.Count)
            {
                while (Shops.Count > newCount)
                {
                    Shops.RemoveAt(Shops.Count - 1);
                }
            }

            ModelToUI();
        }

        /// <summary>
        /// 店選択リストで選択が変更されアタ時の処理を行う。
        /// </summary>
        /// <param name="sender">送信元オブジェクト</param>
        /// <param name="e">イベントオブジェクト</param>
        private void OnDataGridViewShopsSelectionChanged(object sender, EventArgs e)
        {
            DataShop shop = GetSelectedShop();
            if (shop == null)
            {
                textBoxShopName.Text = string.Empty;
                numericUpDownShopLevel.Value = 1;
                ((DataTable)(dataGridViewShops.DataSource)).Rows.Clear();
            }
            else
            {
                textBoxShopName.Text = shop.Name;
                int level = shop.Level;
                numericUpDownShopLevel.Value = Math.Max(1, Math.Min(99, level));
                UpdateDataGridViewItems();
            }
            textBoxShopName.Enabled = (shop != null);
            numericUpDownShopLevel.Enabled = (shop != null);
            dataGridViewShops.Enabled = (shop != null);
            buttonAddItem.Enabled = (shop != null);
        }

        /// <summary>
        /// アイテム一覧データグリッドビューのコンテンツを更新する。
        /// </summary>
        private void UpdateDataGridViewItems()
        {
            DataShop shop = GetSelectedShop();
            DataTable dt = (DataTable)(dataGridViewItems.DataSource);
            dt.Rows.Clear();
            for (int i = 0; i <  shop.ItemList.Count; i++)
            {
                var itemEntry = shop.ItemList[i];
                var row = dt.NewRow();
                row[0] = GetDisplayItemName(itemEntry);
                row[1] = itemEntry.MinCount;
                row[2] = itemEntry.MaxCount;
                row[3] = itemEntry.Condition;
                dt.Rows.Add(row);
            }

        }

        /// <summary>
        /// 表示するアイテム名を得る。
        /// </summary>
        /// <param name="entry">エントリ</param>
        /// <returns>アイテム名</returns>
        private string GetDisplayItemName(ItemEntry entry)
        {
            switch (entry.Kind)
            {
                case (int)(ItemType.Item):
                    return "Item:" + GetItemName(items, entry.Id);
                case (int)(ItemType.Weapon):
                    return "Weapon:" + GetItemName(weapons, entry.Id);
                case (int)(ItemType.Armor):
                    return "Armor:" + GetItemName(armors, entry.Id);
                default:
                    return $"Type{entry.Kind}:{entry.Id}";
            }
        }

        /// <summary>
        /// アイテム名を得る。
        /// </summary>
        /// JavaScriptみたいに優しくないから、インデックス範囲外の設定されてたりすると
        /// 例外でちゃうのを防ぐ。
        /// <param name="items">アイテムリスト</param>
        /// <param name="id">ID</param>
        /// <returns></returns>
        private static string GetItemName(System.Collections.IList items, int id)
        {
            if ((id >= 0) && (id < items.Count))
            {
                return ((IItem)(items[id])).Name;
            } 
            else
            {
                return id.ToString();
            }
        }

        /// <summary>
        /// 選択されている項目を取得する。
        /// </summary>
        /// <returns>選択されている項目</returns>
        private DataShop GetSelectedShop()
        {
            var rows = dataGridViewShops.SelectedRows;
            if (rows.Count == 0)
            {
                return null;
            }
            int index = rows[0].Index + 1; // 先頭のnull抜かしてるからね。
            if ((index >= 0) && (index < Shops.Count))
            {
                return Shops[index];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// ショップ名テキストボックスの内容が変更されたときに通知を受け取る。
        /// </summary>
        /// <param name="sender">送信元オブジェクト</param>
        /// <param name="e">イベントオブジェクト</param>
        private void OnTextBoxShopNameTextChanged(object sender, EventArgs e)
        {
            DataShop shop = GetSelectedShop();
            if (shop == null)
            {
                return;
            }
            shop.Name = textBoxShopName.Text;

            var rows = dataGridViewShops.SelectedRows;
            int index = rows[0].Index;

            DataTable dt = (DataTable)(dataGridViewShops.DataSource);
            var row = dt.Rows[index];
            row.SetField(0, string.Format("{0,4:D}:{1}", shop.Id, shop.Name));
        }

        /// <summary>
        /// アイテム追加ボタンがクリックされたときに通知を受け取る。
        /// </summary>
        /// <param name="sender">送信元オブジェクト</param>
        /// <param name="e">イベントオブジェクト</param>
        private void OnButtonAddItemClick(object sender, EventArgs e)
        {
            if (itemListForm == null)
            {
                itemListForm = new FormSelectableItemList();
                itemListForm.FormClosed += OnItemListFormClosed;
                itemListForm.ItemSelected += OnItemListItemSelected;
                itemListForm.SetItemList(selectableItemList);
            }
            itemListForm.Show(this);
        }

        /// <summary>
        /// アイテム選択ウィンドウが閉じられたときに通知を受け取る。
        /// </summary>
        /// <param name="sender">送信元オブジェクト</param>
        /// <param name="e">イベントオブジェクト</param>
        private void OnItemListFormClosed(object sender, FormClosedEventArgs e)
        {
            itemListForm = null;
        }

        /// <summary>
        /// アイテムが選択された時に通知を受け取る。
        /// </summary>
        /// <param name="sender">送信元オブジェクト</param>
        /// <param name="e">イベントオブジェクト</param>
        private void OnItemListItemSelected(object sender, EventArgs e)
        {
            IItem item = itemListForm.SelectedItem;
            DataShop shop = GetSelectedShop();
            if (shop == null)
            {
                return;
            }

            ItemEntry itemEntry = new ItemEntry();
            itemEntry.Id = item.Id;
            itemEntry.Kind = GetItemKind(item);
            itemEntry.MinCount = 0;
            itemEntry.MaxCount = 1;
            shop.ItemList.Add(itemEntry);

            DataTable dt = (DataTable)(dataGridViewItems.DataSource);
            var row = dt.NewRow();
            row[0] = GetDisplayItemName(itemEntry);
            row[1] = itemEntry.MinCount;
            row[2] = itemEntry.MaxCount;
            row[3] = itemEntry.Condition;
            dt.Rows.Add(row);
        }

        /// <summary>
        /// アイテムの種類番号を取得する
        /// </summary>
        /// <remarks>
        /// これってMVUtils側に用意するべきインタフェースだよね。
        /// </remarks>
        /// <param name="item">アイテム</param>
        /// <returns>種類番号</returns>
        private int GetItemKind(IItem item)
        {
            if (item is DataItem)
            {
                return (int)(ItemType.Item);
            }
            else if (item is DataWeapon)
            {
                return (int)(ItemType.Weapon);
            }
            else if (item is DataArmor)
            {
                return (int)(ItemType.Armor);
            }
            return 0;
        }

        /// <summary>
        /// レベル入力欄の数値が変更されたときに通知を受け取る。
        /// </summary>
        /// <param name="sender">送信元オブジェクト</param>
        /// <param name="e">イベントオブジェクト</param>
        private void OnNumericUpDownShopLevelValueChanged(object sender, EventArgs e)
        {
            DataShop shop = GetSelectedShop();
            int level = (int)(numericUpDownShopLevel.Value);
            shop.Level = Math.Max(1, Math.Min(99, level));
        }

        /// <summary>
        /// セルの値が変更されたときの処理を行う。
        /// </summary>
        /// <param name="sender">送信元オブジェクト</param>
        /// <param name="e">イベントオブジェクト</param>
        private void OnDataGridViewCellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataShop shop = GetSelectedShop();
                DataTable dt = (DataTable)(dataGridViewItems.DataSource);
                ItemEntry entry = shop.ItemList[e.RowIndex];
                DataRow row = dt.Rows[e.RowIndex];
                switch (e.ColumnIndex)
                {
                    case 1:
                        entry.MinCount = (int)(row[1]);
                        break;
                    case 2:
                        entry.MaxCount = (int)(row[2]);
                        break;
                    case 3:
                        entry.Condition = (string)(row[3]);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error");
            }
        }
    }
}
