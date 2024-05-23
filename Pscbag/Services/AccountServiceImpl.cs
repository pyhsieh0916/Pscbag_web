using MySql.Data.MySqlClient;
using Pscbag.Models;
using System.Data;

namespace Pscbag.Services
{
    public class AccountServiceImpl : AccountService
    {
        MySqlConnection mysql = new MySqlConnection("server=127.0.0.1;port=3306;user id=root;password=;database=pscbag;charset=utf8;");

        public AccountServiceImpl()
        {
        }

        public bool update(string product_name, string price, string quantity, string booth_name, string booth_num, string phone, string demo, out string fullname)
        {

            //Default = fail
            int Success = 0;
            fullname = "Guest";

            if (mysql.State != System.Data.ConnectionState.Open)
                mysql.Open();

            string Comd = @"INSERT INTO `product`  (product_name, price, quantity, booth_name, booth_num, phone, demo ) VALUES (CONVERT(_utf8 " + "'" + product_name + "' USING utf8), '" + price + "', '" + quantity + "', '" + booth_name + "', '" + booth_num + "', '" + phone + "', '" + demo + "')";
            using (MySqlCommand cmd = new MySqlCommand(Comd, mysql))
            {

                int i = cmd.ExecuteNonQuery();

                if (i == 1)    //成功
                {
                    Success = 1;
                    fullname = product_name;
                }
                else
                {
                    Success = 0;
                    fullname = "Guest";
                }
            }


            if (mysql.State != ConnectionState.Closed)
                mysql.Close();
            return Success == 1 ? true : false;
        }

        public bool orderBag(string booth_name, string booth_num, string total_price, out string fullname)
        {

            //Default = fail
            int Success = 0;
            fullname = "Guest";

            if (mysql.State != System.Data.ConnectionState.Open)
                mysql.Open();

            string Comd = @"INSERT INTO `order_bag`  (booth_name, booth_num, total_price) VALUES (CONVERT(_utf8 " + "'" + booth_name + "' USING utf8), '" + booth_num + "', '" + total_price + "')";
            using (MySqlCommand cmd = new MySqlCommand(Comd, mysql))
            {

                int i = cmd.ExecuteNonQuery();

                if (i == 1)    //成功
                {
                    Success = 1;
                    fullname = booth_name;
                }
                else
                {
                    Success = 0;
                    fullname = "Guest";
                }
            }


            if (mysql.State != ConnectionState.Closed)
                mysql.Close();
            return Success == 1 ? true : false;
        }

        //Check_date
        public void getInfo(out List<List<object>> dataList)
        {
            dataList = null;
            if (mysql.State != System.Data.ConnectionState.Open)
                mysql.Open();

            string Comd = @"SELECT * FROM order_bag";
            using (MySqlCommand cmd = new MySqlCommand(Comd, mysql))
            {
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    dataList = new List<List<object>>();
                    int i = 0;
                    while (reader.Read())
                    {
                        //5.判斷資料列是否為空

                        if (!reader[0].Equals(DBNull.Value))
                        {
                            dataList.Add(new List<object>()
                            {
                                reader["id"],
                                reader["booth_name"],
                                reader["booth_num"],
                                reader["total_price"],
                                reader["time"]
                            });
                            //string id = reader["id"].ToString();
                            //string booth_name = reader["booth_name"].ToString();
                            //string booth_num = reader["booth_num"].ToString();
                            //string total_price = reader["total_price"].ToString();
                            //DateTime time = (DateTime)reader["time"];

                            //dataList[i].Add(reader["id"]);
                            //dataList[i].Add(reader["booth_name"]);
                            //dataList[i].Add(reader["booth_num"]);
                            //dataList[i].Add(reader["total_price"]);
                            //dataList[i].Add(reader["time"]);
                        }
                        i++;
                    }
                }
            }

            if (mysql.State != ConnectionState.Closed)
                mysql.Close();
        }

        //Check (all)
        public void getInfo_all(out List<List<object>> dataList)
        {
            dataList = null;
            if (mysql.State != System.Data.ConnectionState.Open)
                mysql.Open();

            string Comd = @"SELECT * FROM product";
            using (MySqlCommand cmd = new MySqlCommand(Comd, mysql))
            {
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    dataList = new List<List<object>>();
                    int i = 0;
                    while (reader.Read())
                    {
                        //5.判斷資料列是否為空

                        if (!reader[0].Equals(DBNull.Value))
                        {
                            dataList.Add(new List<object>()
                            {
                                reader["id"],
                                reader["product_name"],
                                reader["price"],
                                reader["quantity"],
                                reader["booth_name"],
                                reader["booth_num"],
                                reader["phone"],
                                reader["demo"],
                                reader["time"]
                            });
                        }
                        i++;
                    }
                }
            }

            if (mysql.State != ConnectionState.Closed)
                mysql.Close();
        }



        public bool Login(string username, string password, out string fullname)
        {
            //int h_p = password.GetHashCode();
            //Default = fail
            int Success = 0;
            fullname = "Guest";
            string Comd = @"SELECT * FROM users WHERE `Account`='" + username + "'and `Password`='" + password + "'";
            using (MySqlCommand cmd = new MySqlCommand(Comd, mysql))
            {
                if (mysql.State != System.Data.ConnectionState.Open)
                    mysql.Open();

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    int numberOfResults = dt.Rows.Count;
                    if (numberOfResults > 0)
                    {
                        Success = 1;
                        //foreach (DataRow dr in dt.Rows)
                        //{
                        //    fullname = dr["chat_name"].ToString();
                        //}
                    }
                    else
                    {
                        Success = 0;
                        fullname = "Guest";
                    }

                }

                if (mysql.State != ConnectionState.Closed)
                    mysql.Close();
            }

            return Success == 1 ? true : false;
        }
    }
   
        //public bool Login(string username, string password, out string fullname)
        //{
        //    int h_p = password.GetHashCode();
        //    //Default = fail
        //    int Success = 0;
        //    fullname = "Guest";
        //    string Comd = @"SELECT `chat_name` FROM chat_account WHERE `Account`='" + username + "'and `Password`='" + h_p + "'";
        //    using (MySqlCommand cmd = new MySqlCommand(Comd, mysql))
        //    {
        //        if (mysql.State != System.Data.ConnectionState.Open)
        //            mysql.Open();

        //        using (MySqlDataReader reader = cmd.ExecuteReader())
        //        {
        //            DataTable dt = new DataTable();
        //            dt.Load(reader);
        //            int numberOfResults = dt.Rows.Count;
        //            if (numberOfResults > 0)
        //            {
        //                Success = 1;
        //                foreach (DataRow dr in dt.Rows)
        //                {
        //                    fullname = dr["chat_name"].ToString();
        //                }
        //            }
        //            else
        //            {
        //                Success = 0;
        //                fullname = "Guest";
        //            }

        //        }

        //        if (mysql.State != ConnectionState.Closed)
        //            mysql.Close();
        //    }

        //    return Success == 1 ? true : false;
        //}


        //
        //public bool Register(string chat_name, string username, string password, string password2, out string fullname)
        //{
        //    //Default = fail
        //    int Success = 0;
        //    fullname = "Guest";

        //    //判斷相等
        //    if ((password != password2) || password.ToString().Length < 8)
        //    {
        //        Success = 0;
        //    }

        //    else
        //    {
        //        int h_p = password.GetHashCode();
        //        int h_p2 = password2.GetHashCode();

        //        string Comd = @"SELECT `Account` FROM chat_account WHERE `Account`='" + username + "'";
        //        using (MySqlCommand cmd = new MySqlCommand(Comd, mysql))
        //        {
        //            if (mysql.State != System.Data.ConnectionState.Open)
        //                mysql.Open();

        //            using (MySqlDataReader reader = cmd.ExecuteReader())
        //            {
        //                DataTable dt = new DataTable();
        //                dt.Load(reader);
        //                int numberOfResults = dt.Rows.Count;
        //                if (numberOfResults > 0)    //判斷已註冊
        //                {
        //                    Success = 0;
        //                    /*foreach (DataRow dr in dt.Rows)
        //                    {
        //                        fullname = chat_name;
        //                    }*/
        //                }
        //                else
        //                {
        //                    string Comd2 = @"INSERT INTO `chat_account`  (Account, Password, chat_name ) VALUES (CONVERT(_utf8 " + "'" + username + "' USING utf8), '" + h_p + "', '" + chat_name + "')";
        //                    using (MySqlCommand cmd2 = new MySqlCommand(Comd2, mysql))
        //                    {

        //                        int i = cmd2.ExecuteNonQuery();

        //                        if (i == 1)    //註冊成功
        //                        {
        //                            Success = 1;
        //                            fullname = chat_name;
        //                        }
        //                        else
        //                        {
        //                            Success = 0;
        //                            fullname = "Guest";
        //                        }
        //                    }

        //                }

        //                if (mysql.State != ConnectionState.Closed)
        //                    mysql.Close();
        //            }
        //        }
        //    }

        //    return Success == 1 ? true : false;
        //}
        //


    }
