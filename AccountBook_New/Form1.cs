using System;
using System.Globalization;
using System.Windows.Forms;
using TextBox = System.Windows.Forms.TextBox;

namespace AccountBook_New
{
    public partial class Form1 : Form
    {
        private int _Salary = 0;
        private int _balance = 0;
        private int _Traffic = 0;
        private int _LotteCard = 0;
        private int _KBPay = 0;
        private int _HanaCard = 0;
        private int _LotteCapital = 0;
        private int _HouseLoan = 0;
        private int _Insurance = 0;

        private bool isUpdatingText = false;

        private int Total = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "가계부";

            this.dtpFrom.Text = DateTime.Now.ToString("yyy-MM");

            // using System.Globalization; 추가 해줘야 날짜가 나옴           
            CultureInfo cultures = CultureInfo.CreateSpecificCulture("ko-KR");

            try
            {
                var chk = DBCommon.ConnectionString();

                switch (chk)
                {
                    case "Success":
                        ts_DBConn.Text = "오늘 : " + DateTime.Now.ToString(string.Format("yyyy년 MM월 dd일 ddd요일", cultures)) + "DB Connect Success!";
                        break;
                    case "Fail":
                        ts_DBConn.Text = "오늘 : " + DateTime.Now.ToString(string.Format("yyyy년 MM월 dd일 ddd요일", cultures)) + "DB Connect Fail!";
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            // DataGridView의 각 열에 대해 헤더 텍스트 가운데 정렬
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SetTextBoxValueToZero(tbSalary);
            SetTextBoxValueToZero(tbTraffic);
            SetTextBoxValueToZero(tbLotteCard);
            SetTextBoxValueToZero(tbKBPay);
            SetTextBoxValueToZero(tbHanaCard);
            SetTextBoxValueToZero(tbLotteCapital);
            SetTextBoxValueToZero(tbHouseLoan);
            SetTextBoxValueToZero(tbInsurance);

            int balance = DBCommon.getBalance(dtpFrom.Text, tbSalary.Text, tbTraffic.Text, tbLotteCard.Text,
                tbKBPay.Text, tbHanaCard.Text, tbLotteCapital.Text, tbHouseLoan.Text
                , tbInsurance.Text);
            if (balance > 0)
            {
                Total = _Traffic
                        + _LotteCard
                        + _KBPay
                        + _HanaCard
                        + _LotteCapital
                        + _HouseLoan
                        + _Insurance;
            }

            int LestFund = balance - Total;

            //dataGridView1.DataSource = DBCommon.GetLastBalance(LestFund, dtpTo, dtpFrom);
        }

        private void SetTextBoxValueToZero(TextBox textBox)
        {
            if (string.IsNullOrEmpty(textBox.Text))
            {
                textBox.Text = "0";
            }
            else
            {
                // 숫자로 변환하여 변수에 저장
                int value = Convert.ToInt32(textBox.Text.Replace(",", ""));
                switch (textBox.Name)
                {
                    case "tbSalary":
                        _Salary = value;
                        break;
                    case "tbTraffic":
                        _Traffic = value;
                        break;
                    case "tbLotteCard":
                        _LotteCard = value;
                        break;
                    case "tbKBPay":
                        _KBPay = value;
                        break;
                    case "tbHanaCard":
                        _HanaCard = value;
                        break;
                    case "tbLotteCapital":
                        _LotteCapital = value;
                        break;
                    case "tbHouseLoan":
                        _Insurance = value;
                        break;
                    case "tbInsurance":
                        _HouseLoan = value;
                        break;
                }
            }
        }

        private string prevalue = String.Empty;

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;

            string text = textBox.Text.Replace(",", "");

            double num = 0;

            if (double.TryParse(text, out num))
            {
                textBox.Text = string.Format("{0:#,##0}", num);
                textBox.SelectionStart = textBox.TextLength;
                textBox.SelectionLength = 0;
            }
            else
            {
                textBox.Text = prevalue;
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = DBCommon.GetBalanceDT(0, dtpFrom.Text.ToString());

            dataGridView1.Columns[0].HeaderText = "입금날짜";
            dataGridView1.Columns[1].HeaderText = "잔액";
            dataGridView1.Columns[2].HeaderText = "급여";
            dataGridView1.Columns[3].HeaderText = "교통카드";
            dataGridView1.Columns[4].HeaderText = "롯데카드";
            dataGridView1.Columns[5].HeaderText = "국민카드";
            dataGridView1.Columns[6].HeaderText = "하나카드";
            dataGridView1.Columns[7].HeaderText = "롯데캐피탈";
            dataGridView1.Columns[8].HeaderText = "전세대출이자";
            dataGridView1.Columns[9].HeaderText = "보험";

            // 컬럼 헤더 가운데 정렬
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[01].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            int cnt = dataGridView1.ColumnCount;

            for (int i = 1; i < cnt; i++)
            {
                dataGridView1.Columns[i].DefaultCellStyle.Format = "N0";
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {

        }
    }
}
