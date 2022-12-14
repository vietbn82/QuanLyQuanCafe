using System;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using QLQuanCafe.BLL;
using QLQuanCafe.Common;

namespace QLQuanCafe.GUI.Dialog
{
    public partial class SuaBan : MetroForm
    {
        public SuaBan()
        {
            InitializeComponent();
        }

        private void SuaBan_Load(object sender, EventArgs e)
        {
            txtTenBan.Text = LocatorBll.AreaAndTableBll.TableSelected.TableName;
        }

        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTenBan.Text.Trim()))
            {
                MessageDialogHelper.CreateErrorMessage("Tên bàn không được để trống.");
                return;
            }
            LocatorBll.AreaAndTableBll.TableToSave.TableName = txtTenBan.Text.Trim();
            if (LocatorBll.AreaAndTableBll.EditTable())
            {
                if (MessageDialogHelper.CreateInformationMessage("Lưu thành công.") == DialogResult.OK)
                    this.Close();
            }
        }
    }
}
