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
            Invalid
        }


        public async Task<Validity> CheckIDValidity(string IDNumber)
        {
            var client = new HttpClient();
            try
            {
                var responds = await client.GetAsync($"http://aplikace.mvcr.cz/neplatne-doklady/doklady.aspx?dotaz={IDNumber}&doklad=0");
                var data = await responds.Content.ReadAsStringAsync();
                using (var stream = await responds.Content.ReadAsStreamAsync())
                {
                    var serializer = new XmlSerializer(typeof(doklady_neplatne));
                    var result = (doklady_neplatne)serializer.Deserialize(stream);
                    return result.chyba == null ? result.odpoved.evidovano == "ne" ? Validity.Valid : Validity.Invalid : Validity.Unknown;
                }
            }
            catch (Exception)
            {
                return Validity.Unknown;
            }
        }
    }


    [XmlType(AnonymousType = true), XmlRoot(Namespace = "", IsNullable = false)]
    public partial class doklady_neplatne
    {
        public doklady_neplatneDotaz dotaz { get; set; }
        public doklady_neplatneOdpoved odpoved { get; set; }
        public doklady_neplatneChyba chyba { get; set; }
        private string posl_zmenaField;
        private string pristi_zmenyField;

        [XmlAttribute()]
        public string posl_zmena
        {
            get => this.posl_zmenaField;
            set => this.posl_zmenaField = value;
        }

        [XmlAttribute()]
        public string pristi_zmeny
        {
            get => this.pristi_zmenyField;
            set => this.pristi_zmenyField = value;
        }
    }


    [XmlType(AnonymousType = true)]
    public partial class doklady_neplatneDotaz
    {
        private string typField;
        private uint cisloField;
        private string serieField;

        [XmlAttribute()]
        public string typ
        {
            get => this.typField;
            set => this.typField = value;
        }

        [XmlAttribute()]
        public uint cislo
        {
            get => this.cisloField;
            set => this.cisloField = value;
        }

        [XmlAttribute()]
        public string serie
        {
            get => this.serieField;
            set => this.serieField = value;
        }
    }


    [XmlType(AnonymousType = true)]
    public partial class doklady_neplatneOdpoved
    {
        private string aktualizovanoField;
        private string evidovanoField;
        private string evidovano_odField;

        [XmlAttribute()]
        public string aktualizovano
        {
            get => this.aktualizovanoField;
            set => this.aktualizovanoField = value;
        }

        [XmlAttribute()]
        public string evidovano
        {
            get => this.evidovanoField;
            set => this.evidovanoField = value;
        }

        [XmlAttribute()]
        public string evidovano_od
        {
            get => this.evidovano_odField;
            set => this.evidovano_odField = value;
        }
    }


    [XmlType(AnonymousType = true)]
    public partial class doklady_neplatneChyba
    {
        private string spatny_dotazField;
        private string valueField;

        [XmlAttribute()]
        public string spatny_dotaz
        {
            get => this.spatny_dotazField;
            set => this.spatny_dotazField = value;
        }

        [XmlText()]
        public string Value
        {
            get => this.valueField;
            set => this.valueField = value;
        }
    }

}
