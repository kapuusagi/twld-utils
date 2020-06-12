using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MVUtils;

namespace SEditor
{
    public partial class FormSelectableItemList : Form
    {
        public FormSelectableItemList()
        {
            InitializeComponent();
        }

        public void SetItemList(List<IItem> items)
        {
            listBoxItems.Items.Clear();
            foreach (IItem item in items)
            {
                listBoxItems.Items.Add(item);
            }
        }

        public event EventHandler ItemSelected;

        private void OnListBoxDrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();

            ListBox listBox = (ListBox)(sender);
            if (e.Index >= 0)
            {
                IItem item = (IItem)(listBox.Items[e.Index]);
                string text = string.Empty;
                if (item is DataItem di)
                {
                    text = "Item:" + di.Id + ":" + di.Name;
                }
                else if (item is DataWeapon dw)
                {
                    text = "Weapon:" + dw.Id + ":" + dw.Name;
                }
                else if (item is DataArmor da)
                {
                    text = "Weapon:" + da.Id + ":" + da.Name;
                }
                using (Brush brush = new SolidBrush(e.ForeColor))
                {
                    e.Graphics.DrawString(text, e.Font, brush, e.Bounds);
                }
            }
            e.DrawFocusRectangle();
        }

        private void OnButtonAddClick(object sender, EventArgs e)
        {
            if (listBoxItems.SelectedIndex >= 0)
            {
                this.ItemSelected?.Invoke(this, new EventArgs());
            }
        }

        private void OnItemListDoubleClick(object sender, EventArgs e)
        {
            if (listBoxItems.SelectedIndex >= 0)
            {
                this.ItemSelected?.Invoke(this, new EventArgs());
            }
        }

        public IItem SelectedItem {
            get {
                return (IItem)(listBoxItems.SelectedItem);
            }
        }
    }
}
