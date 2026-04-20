using QuanLyNhaHang_EF.Model;
using System;
using System.Windows.Forms;

namespace QuanLyNhaHang_EF.Interface_layer.Admin
{
    public partial class frmDanhMuc : Form
    {
        public DanhMuc DanhMucResult { get; private set; }
        private DanhMuc _danhMucEdit;

        public frmDanhMuc(DanhMuc danhMucEdit)
        {
            InitializeComponent();
            _danhMucEdit = danhMucEdit;
        }

        private void frmDanhMuc_Load(object sender, EventArgs e)
        {
            nudThuTu.Minimum = 0;
            nudThuTu.Maximum = 99;

            if (_danhMucEdit != null)
            {
                this.Text = "Sửa danh mục";
                txtTenDanhMuc.Text = _danhMucEdit.TenDanhMuc;
                nudThuTu.Value = _danhMucEdit.ThuTu;
            }
            else
            {
                this.Text = "Thêm danh mục";
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTenDanhMuc.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập tên danh mục!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DanhMucResult = new DanhMuc
            {
                Id = _danhMucEdit?.Id ?? 0,
                TenDanhMuc = txtTenDanhMuc.Text.Trim(),
                ThuTu = (byte)nudThuTu.Value
            };

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        private void txtTenDanhMuc_Click(object sender, EventArgs e)
        {
            if (txtTenDanhMuc.PlaceholderText == "Nhập tên danh mục")
            {
                txtTenDanhMuc.PlaceholderText = "";
            }

        }
        private void txtTenDanhMuc_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTenDanhMuc.Text.Trim()))
            {
                txtTenDanhMuc.PlaceholderText = "Nhập tên danh mục";
            }
        }
    }
}