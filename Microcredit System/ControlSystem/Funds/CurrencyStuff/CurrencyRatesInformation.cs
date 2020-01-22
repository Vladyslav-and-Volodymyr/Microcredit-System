using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Microcredit_System.ControlSystem.Funds.CurrencyStuff
{
    /// <summary>
    /// Class to get currency rates information according to Narodowy Bank Polski
    /// </summary>
    class CurrencyRatesInformation
    {
        private CurrencyRatesInformation() { }

        /// <summary>
        /// Gets currency rate from NBP
        /// </summary>
        /// <example>
        /// GetCurrencyRate("usd")
        /// </example>
        public static string GetCurrencyRate(string currencyCode)
        {
            try
            {
                CurrencyInformation currentCurrency;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://api.nbp.pl/api/exchangerates/rates/A/" + currencyCode);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader streamReader = new StreamReader(response.GetResponseStream());
                DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(CurrencyInformation));
                currentCurrency = (CurrencyInformation)jsonSerializer.ReadObject(response.GetResponseStream());
                return currentCurrency.Rates[0].Mid.ToString();
            }
            catch (WebException)
            {
                return "check your internet connection";
            }
        }
    }
}
