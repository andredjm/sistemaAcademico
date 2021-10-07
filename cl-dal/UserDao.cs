﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;
using Common.Cache;


namespace DataAccess
{
    public class UserDao : connectionToSql
    {
        public bool Login(string user, string pass)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using(var comand = new MySqlCommand())
                {
                    comand.Connection = connection;
                    comand.CommandText = "select * from users where userLogin = @user and userPassword=@password;";
                    comand.Parameters.AddWithValue("@user", user);
                    comand.Parameters.AddWithValue("@password", pass);
                    comand.CommandType = CommandType.Text;
                    MySqlDataReader reader = comand.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            UserLoginCache.UserLogin = reader.GetString(0);
                            UserLoginCache.UserName = reader.GetString(2);
                        }
                        return true;
                    } 
                    else
                    {
                        return false;
                    }

                }
            }
        }
    }
}