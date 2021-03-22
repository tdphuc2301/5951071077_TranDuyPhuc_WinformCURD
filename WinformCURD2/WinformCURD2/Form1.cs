using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinformCURD2
{
    public partial class Form1 : Form
    {
        private SqlConnection con;

        public Form1()
        {
            InitializeComponent();
            GetStudensRecord();
        }

        //public object StudentRecordData { get; set; }

        private void Form1_Load(object sender, EventArgs e)
        {
            GetStudensRecord();
        }

        private void GetStudensRecord()
        {
            SqlConnection con = new SqlConnection(@"Data Source=MYLAP\SQLEXPRESS;Initial Catalog=DemoCRUD;Integrated Security=True");

            SqlCommand cmd = new SqlCommand("SELECT * FROM StudentsTb", con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            dgvStudent.DataSource = dt;

        }
        private bool IsValidData()
        {
            if(  txtHName.Text == string.Empty
              || txtNName.Text == string.Empty
              || txtAddress.Text == string.Empty
              || string.IsNullOrEmpty(txtPhone.Text)
              || string.IsNullOrEmpty(txtRoll.Text))
            {
                MessageBox.Show("Có chỗ chưa nhập dữ liệu !!!",
                                "Lỗi dữ liệu", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);

                return false;
            }
            return true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (IsValidData())
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO StudentsTb VALUES " + "(@Name, @FatherName, @RollNumber, @Address, @Mobile)", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Name", txtHName.Text);
                cmd.Parameters.AddWithValue("@FatherName", txtNName.Text);
                cmd.Parameters.AddWithValue("@RollNumber", txtRoll.Text);
                cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@Mobile", txtPhone.Text);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                GetStudensRecord();
            }
        }
        public int StudentID;
        private object cmd;

        public SqlCommand SqlCommand { get; private set; }

        private void dgvStudent_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            StudentID = Convert.ToInt32(dgvStudent.Rows[0].Cells[0].Value);
            txtHName.Text = dgvStudent.SelectedRows[0].Cells[1].Value.ToString();
            txtNName.Text = dgvStudent.SelectedRows[0].Cells[2].Value.ToString();
            txtRoll.Text = dgvStudent.SelectedRows[0].Cells[3].Value.ToString();
            txtAddress.Text = dgvStudent.SelectedRows[0].Cells[4].Value.ToString();
            txtPhone.Text = dgvStudent.SelectedRows[0].Cells[5].Value.ToString();
        }

        private void btnCapnhat_Click(object sender, EventArgs e)
        {
            if(StudentID > 0)
            {
                SqlCommand cmd = new SqlCommand("UPDATE StudentsTb SET" + "Name = @Name, FatherName = @FatherName, RollNumber = @RollNumber, Address = @Address" + "Mobile = @Mobile WHERE StudentID = @ID", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@name", txtHName.Text);
                cmd.Parameters.AddWithValue("@FatherName", txtNName.Text);
                cmd.Parameters.AddWithValue("@RollNumber", txtRoll.Text);
                cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@Mobile", txtPhone.Text);
                cmd.Parameters.AddWithValue("@ID", this.StudentID);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                GetStudensRecord();
                ResetData();

            }else
            {
                MessageBox.Show("Cập nhật bị lỗi !!!", "Lỗi !",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ResetData()
        {
            throw new NotImplementedException();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (StudentID >0)
            {
                SqlCommand cmd = new SqlCommand("DETELE FROM StudentsTb  WHERE StudentTb = @ID", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@ID", this.StudentID);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                GetStudensRecord();
                ResetData();
            }
            else
            {
                MessageBox.Show("Cập nhật bị lỗi !!!", "Lỗi !",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
    }
    }

