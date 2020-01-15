using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microcredit_System.ControlSystem.Persons.ClientStuff;
using Microcredit_System.ControlSystem.Persons.EmployeeStuff;
using MySql.Data.MySqlClient;


namespace Microcredit_System.ControlSystem.DatabaseStuff
{
    class Database
    {
        private static Database _database = new Database();

        private MySqlConnection connection;

        internal Employee LogIn(string login, string password)
        {
           
            MySqlCommand command = new MySqlCommand("select * from employee where login=\"" + login + 
                                                                            "\" and password=\"" + password + "\"", connection);
            MySqlDataReader dataReader = command.ExecuteReader();

            if (dataReader.Read())
            {
                if (((byte[])dataReader["is_admin"])[0].Equals(49))
                {
                    return new Admin(dataReader);
                }
                else
                {
                    return new Employee(dataReader);
                }
            }
            dataReader.Close();
            return null;
        }

        private string _connectionString;

        private Database()
        {
        }

        internal void Init()
        {
            InitInfo();
            _connectionString = "SERVER=" + DatabaseInfo.Info.Server + ";"
                              + "DATABASE=" + DatabaseInfo.Info.Database + ";"
                              + "UID=" + DatabaseInfo.Info.Username + ";"
                              + "PASSWORD=" + DatabaseInfo.Info.Password + ";";
            connection = new MySqlConnection(_connectionString);
            OpenConnection();
        }

        internal void InitInfo()
        {
            MemoryStream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(File.ReadAllText(ConfigPath())));
            DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(DatabaseInfo));
            DatabaseInfo.Info = (DatabaseInfo)jsonSerializer.ReadObject(memoryStream);
        }

        private string ConfigPath()
        {
            // TODO: normal link
            return Environment.CurrentDirectory + "\\config.txt";
        }

        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based 
                //on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        MessageBox.Show("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }

        [DataContract]
        private class DatabaseInfo
        {
            [DataMember]
            private string server;
            [DataMember]
            private string database;
            [DataMember]
            private string username;
            [DataMember]
            private string password;

            private DatabaseInfo() { }

            private static DatabaseInfo _databaseInfo = new DatabaseInfo();

            public string Server { get => server; set => server = value; }
            public string Database { get => database; set => database = value; }
            public string Username { get => username; set => username = value; }
            public string Password { get => password; set => password = value; }
            internal static DatabaseInfo Info { get => _databaseInfo; set => _databaseInfo = value; }
        }

        internal static Database DB { get => _database; }

        internal void AddClientToBase(Client client)
        {
            /*
             INSERT INTO Customers (CustomerName, ContactName, Address, City, PostalCode, Country)
                    VALUES ('Cardinal', 'Tom B. Erichsen', 'Skagen 21', 'Stavanger', '4006', 'Norway');
             */
            MySqlCommand command = new MySqlCommand(string.Format("insert into client (name, surname, passport) " +
                                                            "values ('{0}', '{1}', '{2}');", new string[] { client.Name, client.Surname, client.Passport }), connection);
            command.ExecuteScalar();
        }
    }
}