using System;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using QLQuanCafe.BLL;
using QLQuanCafe.Common;
using QLQuanCafe.DTO;

namespace QLQuanCafe.GUI.Dialog
{
    public partial class ThemDonViTinh : MetroForm
    {

        public ThemDonViTinh()
        {
            InitializeComponent(); 
        }

        private void ThemDonViTinh_Load(object sender, EventArgs e)
        {

        }

        private void BAccept_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(DonViTinh.Text.Trim()))
            {
                MessageDialogHelper.CreateErrorMessage("Tên đơn vị tính không được để trống.");
                return;
            }
            UnitData unitToSave = new UnitData();
            unitToSave.UnitName = DonViTinh.Text.Trim();
            LocatorBll.UnitBll.UnitToSave = unitToSave;
            if (LocatorBll.UnitBll.AddUnit())
            {              
                if (MessageDialogHelper.CreateInformationMessage("Lưu thành công.") == DialogResult.OK)
                    this.Close();
            }
        }

        private void BCancel_Click(object sender, EventArgs e)
        {
            this.Close();    
        }
    }
}
