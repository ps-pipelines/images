using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using Microsoft.Extensions.Logging;

namespace PetStore.Images.Infrastructure
{
    public class SqlStore : IStore
    {
        private string connectionString =
            "Data Source=.\\SQLEXPRESS;Initial Catalog=Images;Persist Security Info=True;User ID=testuser;Password=ABC123!!";

        private readonly ILogger<SqlStore> _logger;

        public SqlStore(ILogger<SqlStore> logger)
        {
            _logger = logger;


            Env.Variables.TryGetValue("ConnectionString", out var connStr);
            
            if(!string.IsNullOrEmpty(connStr))
            {
                _logger.LogInformation("using connection string from env var");
                connectionString = connStr;
            }

            try
            {
                InitDb();    
            }
            catch (Exception ex)
            {
                //again this is not production code, do not do the following.
                //throw new Exception(connectionString, ex);

                _logger.LogError(1, ex, ex.ToString());
                throw new Exception("unable to initialize the database");
            }
            
        }

        private void InitDb ()
        {
            var needToInit = false;

            Thread.Sleep(5000);

            _logger.LogInformation("checking database");
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT name from sys.databases where [name] = 'images'";
                conn.Open();

                var dbNname = cmd.ExecuteScalar();
                needToInit = dbNname == DBNull.Value || string.IsNullOrEmpty((string)dbNname);
            }

            if (!needToInit)
                return;

            _logger.LogInformation("creating images database");
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "CREATE DATABASE images";
                conn.Open();

                var result = cmd.ExecuteNonQuery();
            }

            using (var conn = new SqlConnection(connectionString))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText =   
                    @"USE [Images]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Assets](
	[Id] [varchar](128) NOT NULL,
	[Content] [varbinary](max) NULL,
	[Name] [varchar](max) NULL,
 CONSTRAINT [PK_Assets] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
";
                conn.Open();

                var result = cmd.ExecuteNonQuery();
            }

            _logger.LogInformation("created images database");
        }

        public string Save(string name, byte[] asset)
        {
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "insert into [dbo].[assets] ([id],[content]) values (@id, @content)";

                var param = cmd.Parameters.Add("@content", SqlDbType.VarBinary);
                param.Value = asset;

                var id = Guid.NewGuid().ToString();
                param = cmd.Parameters.Add("@id", SqlDbType.VarChar);
                param.Value = id;

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                return id;
            }
        }

        public void UpdateName(string id, string name)
        {
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "update [dbo].[assets] set [name] = @name  where [id] = @id";

                var param = cmd.Parameters.Add("@name", SqlDbType.VarChar);
                param.Value = name;

                param = cmd.Parameters.Add("@id", SqlDbType.VarChar);
                param.Value = id;

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }


        public IEnumerable<Image> Find(string filterByName = null)
        {

            var cmdText = "select Id, [Name], [Content] from [dbo].[assets]";
            if (!string.IsNullOrEmpty(filterByName))
            {
                cmdText += " where [Name] like '%" + filterByName + "%'";
            }

            using (var conn = new SqlConnection(connectionString))
            using (var cmd = conn.CreateCommand())
            {
                //hackable
                cmd.CommandText = cmdText;

                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        yield return new Image()
                        {
                            Id = (string) reader["Id"],
                            Name = GetValue(reader, "Name"),
                            Content = (byte[]) reader["Content"]

                        };
                    }
                }
            }
        }

        string GetValue(IDataReader reader, string colName)
        {
            var val = reader["Name"];

            if (val == DBNull.Value)
            {
                return null;
            }

            return (string) val;
        }

        public Image Load(string id)
        {
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = conn.CreateCommand())
            {
                //hackable
                cmd.CommandText = "select Id, [Name], [Content] from [dbo].[assets] where id = '" + id + "'";

                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        return new Image()
                        {
                            Id = (string) reader["Id"],
                            Name =GetValue(reader, "Name"),
                            Content = (byte[]) reader["Content"]

                        };
                    }
                }
            }

            return null;

        }
    }
}