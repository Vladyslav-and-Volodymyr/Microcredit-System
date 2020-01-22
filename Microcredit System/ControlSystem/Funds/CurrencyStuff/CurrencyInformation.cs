using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Microcredit_System.ControlSystem.Funds.CurrencyStuff
{
    /// <summary>
    /// Information about currency according to Narodowy Bank Polski. 
    /// This is additional class for CurrencyRatesInformation. Do not change it or use it
    /// </summary>
    [DataContract]
    public class CurrencyInformation
    {
        [DataMember]
        private String table;
        [DataMember]
        private String currency;
        [DataMember]
        private String code;
        [DataMember]
        private RatesInformation[] rates;

        public string Table { get => table; set => table = value; }
        public string Currency { get => currency; set => currency = value; }
        public string Code { get => code; set => code = value; }
        public RatesInformation[] Rates { get => rates; set => rates = value; }

        [DataContract]
        public class RatesInformation
        {
            [DataMember]
            private double mid;
            [DataMember]
            private string no;
            [DataMember]
            private string effectiveDate;

            public double Mid { get => mid; set => mid = value; }
            public string No { get => no; set => no = value; }
            public string EffectiveDate { get => effectiveDate; set => effectiveDate = value; }
        }
    }
}
