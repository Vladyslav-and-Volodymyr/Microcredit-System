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

        public double Pln { get => _pln; }
        public double Usd { get => _usd; }
        public double Eur { get => _eur; }
        internal static Balance Instance { get => instance; set => instance = value; }

        public void Init(double pln, double usd, double eur)
        {
            _pln = pln;
            _usd = usd;
            _eur = eur;
        }

        public void Init(MySqlDataReader reader)
        {
            Init(reader.GetDouble(2), reader.GetDouble(1), reader.GetDouble(0));
            reader.Close();
        }

        void Init()
        {
            Database.DB.GetBalance();
        }

        public void AddDeltaToUsd(double delta)
        {
            Init();
            _usd += delta;

            Database.DB.ExecuteQuery("update balance " +
                                     "set usd=" + _usd);
        }

        public void AddDeltaToPln(double delta)
        {
            Init();
            _pln += delta;

            Database.DB.ExecuteQuery("update balance " +
                                     "set pln=" + _pln);
        }

        public void AddDeltaToEur(double delta)
        {
            Init();
            _eur += delta;

            Database.DB.ExecuteQuery("update balance " +
                                     "set eur=" + _eur);
        }
    }
}
