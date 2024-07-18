using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;

namespace AccountBook_New
{
    internal class DBCommon
    {
        private static OracleConnection conn;

        internal static string ConnectionString()
        {
            /*
            var connectinfo = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)" +
                              "(HOST=localhost)" +
                              "(PORT=1541))" +
                              "(CONNECT_DATA=(SERVICE_NAME=xe)));" +
                              "User Id=Main" +
                              ";Password=Fund2408";
            */
            string connectinfo = "DATA SOURCE=localhost; User Id=Main; Password=Fund2408";
            conn = new OracleConnection(connectinfo);
            conn.Open();

            if (conn.State == ConnectionState.Open)
            {
                return "Success";
            }
            else
            {
                return "Fail";
            }
        }


        public static DataTable GetBalanceDT(int salary, string dtpFromText)
        {
            DataTable dt = new DataTable();
            try
            {
                if (conn.State == ConnectionState.Open)
                {
                    string sql = " Select To_Char(A.Insertdate, 'YYYY-MM') As Insertdate" +
                                 "      , A.Price" +
                                 "      , B.Wage" +
                                 "      , B.Traffic" +
                                 "      , B.Lottecard" +
                                 "      , B.Kbpay" +
                                 "      , B.Hanacard" +
                                 "      , B.Lottecapital" +
                                 "      , B.Houseloan" +
                                 "      , B.Insurance" +
                                 "   From Salary A" +
                                 "   Full Join Inputprice B" +
                                 "     On  To_Char(A.Insertdate, 'YYYY-MM') = To_Char(B.Insertdate, 'YYYY-MM')" +
                                 " Where 1=1" +
                                 " And To_Char(A.Insertdate, 'YYYY-MM') = :value ";

                    using (OracleCommand cmd = new OracleCommand(sql, conn))
                    {
                        cmd.Parameters.Add(new OracleParameter("value", OracleDbType.Varchar2)).Value = dtpFromText;
                        using (OracleDataAdapter adapter = new OracleDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            return dt;
        }

        public static object GetLastBalance(int lestFund, DateTimePicker dtpFrom, DateTimePicker dtpTo)
        {
            DataTable dt = new DataTable();

            try
            {

            }
            catch (Exception e)
            {
                return null;
            }

            return dt;
        }

        public static int getBalance(string dtpFromText, string wage, string traffic, string lottecard, string kbpay, string hanacard, string lottecapital, string houseloan, string insurance)
        {
            int rowAffected = 0;
            try
            {
                string sql = "insert into INPUTPRICE ( WAGE, TRAFFIC, LotteCard, KBPay, HanaCard, LotteCapital, HouseLoan, Insurance )" +
                             "values(:WAGE, :TRAFFIC, :LotteCard, :KBPay, :HanaCard, :LotteCapital, :HouseLoan, :Insurance);";
                using (OracleCommand command = new OracleCommand(sql, conn))
                {
                    command.Parameters.Add(":WAGE", OracleDbType.Int32).Value = wage;
                    command.Parameters.Add("TRAFFIC", OracleDbType.Int32).Value = traffic;
                    command.Parameters.Add("LotteCard", OracleDbType.Int32).Value = lottecard;
                    command.Parameters.Add("KBPay", OracleDbType.Int32).Value = kbpay;
                    command.Parameters.Add("HanaCard", OracleDbType.Int32).Value = hanacard;
                    command.Parameters.Add("LotteCapital", OracleDbType.Int32).Value = lottecapital;
                    command.Parameters.Add("HouseLoan", OracleDbType.Int32).Value = houseloan;
                    command.Parameters.Add("Insurance", OracleDbType.Int32).Value = insurance;

                    rowAffected = command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                //log.Debug(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + ex.Message);
                //Log.WriteLog(Log.enumLogLevel.MIDDLE, "Insert Data", ex.Message);
                return rowAffected;
            }

            return rowAffected;
        }
    }
}
