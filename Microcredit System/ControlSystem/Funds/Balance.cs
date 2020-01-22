using Microcredit_System.ControlSystem.DatabaseStuff;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microcredit_System.ControlSystem.Funds
{
    class Balance
    {

        private static Balance instance = new Balance();

        private Balance() { }

        private double _pln;
        private double _usd;
        private double _eur;

        /// <summary>
        /// Current balance of pln
        /// </summary>
        public double Pln { get => _pln; }
        /// <summary>
        /// Current balance of usd
        /// </summary>
        public double Usd { get => _usd; }
        /// <summary>
        /// Current balance of eur
        /// </summary>
        public double Eur { get => _eur; }
        internal static Balance Instance { get => instance; set => instance = value; }

        /// <summary>
        /// Initialization from doubles
        /// </summary>
        public void Init(double pln, double usd, double eur)
        {
            _pln = pln;
            _usd = usd;
            _eur = eur;
        }

        /// <summary>
        /// Initialization from SqlReader
        /// </summary>
        public void Init(MySqlDataReader reader)
        {
            Init(reader.GetDouble(2), reader.GetDouble(1), reader.GetDouble(0));
            reader.Close();
        }

        /// <summary>
        /// Initialization from Database
        /// </summary>
        void Init()
        {
            Database.DB.GetBalance();
        }

        /// <summary>
        /// Adds delta to Usd in Database
        /// </summary>
        public void AddDeltaToUsd(double delta)
        {
            Init();
            _usd += delta;

            Database.DB.ExecuteQuery("update balance " +
                                     "set usd=" + _usd);
        }

        /// <summary>
        /// Adds delta to Pln in Database
        /// </summary>
        public void AddDeltaToPln(double delta)
        {
            Init();
            _pln += delta;

            Database.DB.ExecuteQuery("update balance " +
                                     "set pln=" + _pln);
        }

        /// <summary>
        /// Adds delta to Eur in Database
        /// </summary>
        public void AddDeltaToEur(double delta)
        {
            Init();
            _eur += delta;

            Database.DB.ExecuteQuery("update balance " +
                                     "set eur=" + _eur);
        }
    }
}
