using System.Collections.ObjectModel;

namespace OefeningScheepvaart.Model
{
    public class Vloot
    {
        // We willen zo snel mogelijk kunnen opzoeken op naam. Een dictionary heeft ook een .Values property waardoor we de gewone lijst ook kunnen opvragen
        private Dictionary<string, Schip> _schepenOpNaam;

        public string Naam { get; set; }

        public ReadOnlyCollection<Schip> Schepen
            => _schepenOpNaam.Values   // We willen enkel de items van onze lijst
                       .ToList()       // .Values geeft ons een 'ValueCollection' dewelke geen 'AsReadOnly' heeft, dus we converteren naar een List<Schip>
                       .AsReadOnly();

        //public double CargoWaarde { get; internal set; }
        //public double AantalPassagiers { get; internal set; }
        //public double Volume { get; internal set; }

        public Vloot(string naam, List<Schip> schepen)
        {
            Naam = naam;

            // Een List<> heeft altijd een ToDictionary methode waar je in een lambda vorm meegeeft wat je voor dictionary key wilt.
            // PAS OP: Dit kan je alleen toepassen als je weet dat de gekozen key unique is, anders zal je een exception krijgen
            _schepenOpNaam = schepen.ToDictionary(x => x.Naam); 
        }

        public Schip ZoekSchip(string naam)
        {
            // We proberen het schip uit de dictionary te halen, lukt het niet dan krijgen we 'false'
            if (_schepenOpNaam.TryGetValue(naam, out var schip))
                return schip;

            return default;
        }

        public bool HeeftSchip(string naam)
        {
            if (_schepenOpNaam.ContainsKey(naam))
                return true;

            return false;
        }

        public void VoegSchipToe(Schip schip)
        {
            // Dit probeert toe te voegen, als het niet lukt (omdat de key al bestaat bv.) krijgen we 'false' terug.
            var isGelukt = _schepenOpNaam.TryAdd(schip.Naam, schip);
            if ( ! isGelukt )
                throw new Exception($"Het schip {schip.Naam} kon niet toegevoegd worden omdat het al bestaat."); 
        }

        public void VerwijderSchip(Schip schip)
            => VerwijderSchip(schip.Naam);

        public void VerwijderSchip(string schipNaam)
        {
            var isGelukt = _schepenOpNaam.Remove(schipNaam);
            if (!isGelukt)
                throw new Exception($"Het schip {schipNaam} kon niet verwijderd worden omdat het niet bestaat in de vloot.");
        }

        public double BerekenCargoWaardeInEuro()
        {
            return _schepenOpNaam.Values
                                 .Sum(schip => schip is CargoSchip cargoSchip ? cargoSchip.CargoWaardeInEuro : 0);
        }

        public int BerekenAantalPassagiers()
        {
            return _schepenOpNaam.Values
                                 .Sum(schip => schip is PassagiersSchip passagiersSchip ? passagiersSchip.AantalPassagiers : 0);
        }

        public int BerekenVolumeInLiters()
        {
            return _schepenOpNaam.Values
                                 .Sum(schip => schip is TankerSchip tankerSchip ? tankerSchip.VolumeInLiters : 0);
        }

        public List<Sleepboot> GeefSleepboten()
        {
            return _schepenOpNaam.Values
                                 .OfType<Sleepboot>() // Geef enkel de objecten van type 'Sleepboot' terug
                                 .ToList();
        }

        public List<Schip> GeefSchepenGesorteerdPerTonnageAflopend()
        {
            return _schepenOpNaam.Values
                                 .OrderByDescending(schip => schip.Tonnage) // LINQ om aflopend te sorteren
                                 .ToList();
        }
    }
}
