using Newtonsoft.Json;
using System;

namespace OverheidioApi.NET.Models
{
    public class Vehicle
    {
        [JsonProperty("aantalcilinders")]
        public int Cilinders { get; set; }

        [JsonProperty("aantalzitplaatsen")]
        public int Seats { get; set; }

        [JsonProperty("bpm")]
        public int Tax { get; set; }

        [JsonProperty("catalogusprijs")]
        public int ListPrice { get; set; }

        [JsonProperty("cilinderinhoud")]
        public int Displacement { get; set; }

        [JsonProperty("datumaanvangtenaamstelling")]
        public DateTime DateAscription { get; set; }

        [JsonProperty("datumeersteafgiftenederland")]
        public DateTime DateRelease { get; set; }

        [JsonProperty("datumeerstetoelating")]
        public DateTime DateAdmission { get; set; }

        [JsonProperty("eerstekleur")]
        public string ColorFirst { get; set; }

        [JsonProperty("g3installatie")]
        public string G3Installation { get; set; }

        [JsonProperty("handelsbenaming")]
        public string CommercialDesignation { get; set; }

        [JsonProperty("hoofdbrandstof")]
        public string FuelTypeMain { get; set; }

        [JsonProperty("inrichting")]
        public string Construction { get; set; }

        [JsonProperty("kenteken")]
        public string LicensePlate { get; set; }

        [JsonProperty("massaleegvoertuig")]
        public int MassEmpty { get; set; }

        [JsonProperty("massarijklaar")]
        public int MassRoadworthy { get; set; }

        [JsonProperty("merk")]
        public string Brand { get; set; }

        [JsonProperty("milieuclassificatie")]
        public string EnvironmentalClassification { get; set; }

        [JsonProperty("nevenbrandstof")]
        public string FuelTypeSecond { get; set; }

        // TODO to boolean
        [JsonProperty("retrofitroetfilter")]
        public string IsRetrofitParticleFilter { get; set; }

        [JsonProperty("toegestanemaximummassavoertuig")]
        public int AllowedMaximumMass { get; set; }

        [JsonProperty("tweedekleur")]
        public string ColorSecond { get; set; }

        [JsonProperty("vermogen")]
        public int Power { get; set; }

        [JsonProperty("vervaldatumapk")]
        public DateTime ExpirationDateApk { get; set; }

        [JsonProperty("voertuigsoort")]
        public string VehicleType { get; set; }

        // TODO to boolean
        [JsonProperty("wachtopkeuren")]
        public string IsWaitingForApproval { get; set; }

        // TODO to boolean
        [JsonProperty("wamverzekerdgeregistreerd")]
        public string IsWamInsuredRegistered { get; set; }

        [JsonProperty("zuinigheidslabel")]
        public string EconomyLabel { get; set; }

        [JsonProperty("_links")]
        public HalLinks Links { get; set; }
    }
}