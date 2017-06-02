using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using net_core_mvc.Models;

namespace net_core_mvc.Factories
{
    public class MessageFactory : IFactory<Message>
    {
        private readonly IOptions<MySqlOptions> MySqlConfig;
        
        public MessageFactory(IOptions<MySqlOptions> config)
        {
            MySqlConfig = config;
        }
        
        internal IDbConnection Connection
        {
            get { return new MySqlConnection(MySqlConfig.Value.ConnectionString);}
        }

        public void Add(Message Item)
        {
            using(IDbConnection dbConnection = Connection)
            {
                Console.WriteLine(Item);
                string Query = string.Format("INSERT INTO message (Name, Email, Note, CreatedAt, UpdateAt) VALUES (@Name, @Email, @Note, NOW(), NOW())", 
                    Item.Name, Item.Email, Item.Note, DateTime.Now, DateTime.Now);
                dbConnection.Open();
                //MySqlCommand c = new MySqlCommand(Query, dbConnection);
                //c.ExecuteNonQuery();
                
                //dbConnection.Execute(Query, Item);
            }
        }
        public List<Message> All()
        {
            using(IDbConnection dbConnection = Connection)
            {
                string Query = "SELECT * FROM message";
                dbConnection.Open();
                //return dbConnection.Query<Message>(Query).ToList();
                return null;
            }
        }

        public Message GetUserById()
        {
            using(IDbConnection dbConnection = Connection)
            {
                string Query = $"SELECT CURRENT_USER from message;";
                dbConnection.Open();
                  //return dbConnection.Query<Message>(Query).Last();
                  return null;
            }
        }
        public void DeleteMessage(int Id)
        {
            using(IDbConnection dbConnection = Connection)
            {
                string Query = $"DELETE FROM message WHERE ID = {Id}";
                dbConnection.Open();
                //dbConnection.Execute(Query);
            }
        }
    }
}