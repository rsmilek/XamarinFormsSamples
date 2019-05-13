using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace IDCardValidator.Tools
{
    class IDValidityManager
    {
        public enum Validity
        {
            Unknown,
            Valid,
            Invalid,
            Error
        }


        public async Task<Validity> CheckIDValidity(string IDNumber)
        {
            var client = new HttpClient();
            try
            {
                var responds = await client.GetAsync($"https://aplikace.mvcr.cz/neplatne-doklady/doklady.aspx?dotaz={IDNumber}&doklad=0");
                var data = await responds.Content.ReadAsStringAsync();
                using (var stream = await responds.Content.ReadAsStreamAsync())
                {
                    var serializer = new XmlSerializer(typeof(doklady_neplatne));
                    var result = (doklady_neplatne)serializer.Deserialize(stream);
                    return result.chyba == null ? result.odpoved.evidovano == "ne" ? Validity.Valid : Validity.Invalid : Validity.Unknown;
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                return Validity.Error;
            }
        }
    }


    [XmlType(AnonymousType = true), XmlRoot(Namespace = "", IsNullable = false)]
    public partial class doklady_neplatne
    {
        public doklady_neplatneDotaz dotaz { get; set; }
        public doklady_neplatneOdpoved odpoved { get; set; }
        public doklady_neplatneChyba chyba { get; set; }

        [XmlAttribute()]
        public string posl_zmena { get; set; }

        [XmlAttribute()]
        public string pristi_zmeny { get; set; }
    }


    [XmlType(AnonymousType = true)]
    public partial class doklady_neplatneDotaz
    {
        [XmlAttribute()]
        public string typ { get; set; }

        [XmlAttribute()]
        public uint cislo { get; set; }

        [XmlAttribute()]
        public string serie { get; set; }
    }


    [XmlType(AnonymousType = true)]
    public partial class doklady_neplatneOdpoved
    {
        [XmlAttribute()]
        public string aktualizovano { get; set; }

        [XmlAttribute()]
        public string evidovano { get; set; }

        [XmlAttribute()]
        public string evidovano_od { get; set; }
    }


    [XmlType(AnonymousType = true)]
    public partial class doklady_neplatneChyba
    {
        [XmlAttribute()]
        public string spatny_dotaz { get; set; }

        [XmlText()]
        public string Value { get; set; }
    }

}
