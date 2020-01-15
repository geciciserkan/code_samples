using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SampleWebApiAspNetCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SampleWebApiAspNetCore.Repositories
{
    public class WebAppDbContext
    {
        string ConnectionString;
        public WebAppDbContext(IConfiguration configuration)
        {
            ConnectionString = configuration["ConnectionStrings:DefaultConnection"];
        }

        public WebAppDataEntity Get(string guid)
        {
            var result = new WebAppDataEntity();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("GetUserDataByGuid"))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Connection = connection;

                    SqlParameter param1 = new SqlParameter();
                    param1.ParameterName = "@guid";
                    param1.DbType = System.Data.DbType.String;
                    param1.Direction = System.Data.ParameterDirection.Input;
                    param1.Value = guid;
                    command.Parameters.Add(param1);
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            result.guid = guid;
                            result.lastModified = Convert.ToDateTime(reader[2]);
                            result.userData = ConverToDict(reader[1].ToString());
                        }
                    }

                    reader.Close();
                    connection.Close();
                }
            }

            return result;
        }


        public WebAppDataEntity Set(string guid, DateTime lastModified, IDictionary<string, object> userData)
        {
            var result = new WebAppDataEntity();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("AddOrUpdateUserDataByGuid"))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Connection = connection;

                    SqlParameter param1 = new SqlParameter();
                    param1.ParameterName = "@guid";
                    param1.DbType = System.Data.DbType.String;
                    param1.Direction = System.Data.ParameterDirection.Input;
                    param1.Value = guid;
                    command.Parameters.Add(param1);

                    SqlParameter param2 = new SqlParameter();
                    param2.ParameterName = "@lastModified";
                    param2.DbType = System.Data.DbType.DateTime2;
                    param2.Direction = System.Data.ParameterDirection.Input;
                    param2.Value = lastModified;
                    command.Parameters.Add(param2);

                    SqlParameter param3 = new SqlParameter();
                    param3.ParameterName = "@userData";
                    param3.DbType = System.Data.DbType.String;
                    param3.Direction = System.Data.ParameterDirection.Input;
                    param3.Value = ConverToDBString(userData);
                    command.Parameters.Add(param3);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }

            return result;
        }


        string ConverToDBString(IDictionary<string, object> userData)
        {
            StringBuilder stringBuild = new StringBuilder();
            foreach (var item in userData)
            {
                if (item.Key.Length > 0)
                {
                    stringBuild.Append(item.Key + ":" + item.Value.ToString() + "-");
                }
            }
            var result = stringBuild.ToString();
            return result.Length > 0 ? result.TrimEnd('-') : string.Empty;
        }

        Dictionary<string, object> ConverToDict(string userData)
        {
            var tempDict = new Dictionary<string, object>();
            var dict = userData.Trim().Split('-');
            foreach (var item in dict)
            {
                var arr = item.Split(":");
                if (arr[0].Length > 0)
                {
                    tempDict.Add(arr[0], Convert.ToInt32(arr[1]));

                }
            }
            return tempDict;
        }
    }
}