using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microcredit_System.ControlSystem.Funds;
using Microcredit_System.ControlSystem.Persons.ClientStuff;
using Microcredit_System.ControlSystem.Persons.EmployeeStuff;
using MySql.Data.MySqlClient;


namespace Microcredit_System.ControlSystem.DatabaseStuff
{
    class Database
    {
        private static Database _database = new Database();

        private MySqlConnection connection;

        /// <summary>
        /// Logging in
        /// </summary>
        internal Employee LogIn(string login, string password)
        {
           
            MySqlCommand command = new MySqlCommand("select * from employee where login=\"" + login + 
                                                                            "\" and password=\"" + password + "\"", 
                                                                            connection);
            MySqlDataReader dataReader = command.ExecuteReader();

            if (dataReader.Read())
            {
                if (((byte[])dataReader["is_admin"])[0].Equals(49))
                {
                    var admin = new Admin(dataReader);
                    dataReader.Close();
                    return admin;
                }
                else
                {
                    var employee = new Employee(dataReader);
                    dataReader.Close();
                    return employee;
                }
            }
            dataReader.Close();
            return null;
        }

        /// <summary>
        /// Adds new worker to database
        /// </summary>
        internal void AddEmployeeToBase(Employee employee, bool isAdmin)
        {
            string query = string.Format("insert into employee(name, surname, pesel, login, password, is_admin) " +
                                         "values('{0}', '{1}', '{2}', '{3}', '{4}', {5});", 
                                        new string[] { employee.Name,
                                                       employee.Surname,
                                                       employee.Pesel,
                                                       employee.Login,
                                                       employee.Password,
                                                       isAdmin.ToString() });
            MySqlCommand command = new MySqlCommand(query, connection);
            command.ExecuteScalar();
        }

        /// <summary>
        /// Gets list of clients from database whose debt > 0
        /// </summary>
        internal List<Client> GetDebtors()
        {
            List<Client> ans = new List<Client>();

            MySqlCommand command = new MySqlCommand("select * from client where debt > 0;", connection);
            MySqlDataReader dataReader = command.ExecuteReader();

            while(dataReader.Read())
            {
                ans.Add(new Client(dataReader));
            }
            dataReader.Close();

            return ans;
        }

        /// <summary>
        /// Deletes client from database
        /// </summary>
        internal void DeleteClient(Client client)
        {
            string query = "delete from client where passport='" + client.Passport + "';";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.ExecuteScalar();
        }

        /// <summary>
        /// Executes SQL-query
        /// </summary>
        public void ExecuteQuery(string query)
        {
            MySqlCommand command = new MySqlCommand(query, connection);
            command.ExecuteScalar();
        }

        /// <summary>
        /// Gets list of workers from database
        /// </summary>
        internal List<Employee> GetEmployees()
        {
            List<Employee> ans = new List<Employee>();

            MySqlCommand command = new MySqlCommand("select * from employee;", connection);
            MySqlDataReader dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                if (((byte[])dataReader["is_admin"])[0].Equals(49))
                {
                    ans.Add(new Admin(dataReader));
                }
                else
                {
                    ans.Add(new Employee(dataReader));
                }
            }
            dataReader.Close();

            return ans;
        }

        /// <summary>
        /// Update balance from database
        /// </summary>
        public Balance GetBalance()
        {
            string query = "select * from balance";

            MySqlCommand command = new MySqlCommand(query, connection);
            var reader = command.ExecuteReader();

            reader.Read();

            Balance.Instance.Init(reader);

            return Balance.Instance;
        }

        /// <summary>
        /// Gets list of clients from database
        /// </summary>
        internal List<Client> GetClients()
        {
            List<Client> ans = new List<Client>();

            MySqlCommand command = new MySqlCommand("select * from client;", connection);
            MySqlDataReader dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                ans.Add(new Client(dataReader));
            }
            dataReader.Close();

            return ans;
        }

        private string _connectionString;

        private Database()
        {
        }

        /// <summary>
        /// Opens connection to database configured in configure.txt
        /// </summary>
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

        private void InitInfo()
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
                        MessageBox.Show("Cannot connect to server. Contact administrator");
                        Application.Current.Shutdown();
                        break;

                    case 1045:
                        MessageBox.Show("Invalid username/password, please try again");
                        Application.Current.Shutdown();
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
            private string login;
            [DataMember]
            private string password;

            private DatabaseInfo() { }

            private static DatabaseInfo _databaseInfo = new DatabaseInfo();

            public string Server { get => server; set => server = value; }
            public string Database { get => database; set => database = value; }
            public string Username { get => login; set => login = value; }
            public string Password { get => password; set => password = value; }
            internal static DatabaseInfo Info { get => _databaseInfo; set => _databaseInfo = value; }
        }

        internal static Database DB { get => _database; }

        /// <summary>
        /// Adds delta to client in parameters
        /// </summary>
        internal void ChangeDebt(Client client, float delta)
        {
            client.Debt += delta;
            MySqlCommand command = new MySqlCommand("UPDATE client " +
                "SET debt = " + client.Debt + " WHERE passport = '" + client.Passport + "';", connection);

            command.ExecuteScalar();

            Balance.Instance.AddDeltaToPln(-delta);
        }

        /// <summary>
        /// Adding client to database
        /// </summary>
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