using System.Collections.ObjectModel;

namespace OefeningScheepvaart.Model
{

    public class Rederij
    {
        private Dictionary<string,Vloot> _vlotenOpNaam;
        private SortedList<string, Haven> _havensGesorteerdOpNaam;

        public ReadOnlyCollection<Vloot> Vloten 
            => _vlotenOpNaam.Values   // We willen enkel de items van onze lijst
                            .ToList() // .Values geeft ons een 'ValueCollection' dewelke geen 'AsReadOnly' heeft, dus we converteren naar een List<Schip>
                            .AsReadOnly();

        public ReadOnlyCollection<Haven> Havens // Havens zou ook een lijst van strings kunnen zijn.
            => _havensGesorteerdOpNaam.Values // Gesorteerd volgens zijn key
                                      .ToList()
                                      .AsReadOnly();

        public double CargoWaarde
            => _vlotenOpNaam.Values.Sum(vloot => vloot.BerekenCargoWaardeInEuro());

        public double AantalPassagiers
            => _vlotenOpNaam.Values.Sum(vloot => vloot.BerekenAantalPassagiers());

        public double Volume
            => _vlotenOpNaam.Values.Sum(vloot => vloot.BerekenVolumeInLiters());

        public Rederij(List<Vloot> vloten, List<Haven> havens)
        {
            // By default zal hij sorteren op de key dewelke een string is. Hiervoor is er ingebouwde support.
            // Mocht de key 'Haven' zijn dan kan een Comparer gebruikt worden zoals HavenComparer.
            // En alternatief is op het model 'Equals' en 'GetHashCode' implementeren. Zie 'Schip' voor een voorbeeld.
            _havensGesorteerdOpNaam = new SortedList<string, Haven>(havens.ToDictionary(haven => haven.Naam)); // haven => haven.Name is de lambda die zal gebruikt worden om de key te bepalen voor elk element

            // Een List<> heeft altijd een ToDictionary methode waar je in een lambda vorm meegeeft wat je voor dictionary key wilt.
            // PAS OP: Dit kan je alleen toepassen als je weet dat de gekozen key unique is, anders zal je een exception krijgen
            _vlotenOpNaam = vloten.ToDictionary(x => x.Naam);
        }

        public Vloot ZoekVloot(string naam)
        {
            // We proberen de vloot uit de dictionary te halen, lukt het niet dan krijgen we 'false'
            if (_vlotenOpNaam.TryGetValue(naam, out var vloot))
                return vloot;

            return default;
        }

        public void VoegVlootToe(Vloot vloot)
        {
            // Dit probeert toe te voegen, als het niet lukt (omdat de key al bestaat bv.) krijgen we 'false' terug.
            var isGelukt = _vlotenOpNaam.TryAdd(vloot.Naam, vloot);
            if (!isGelukt)
                throw new Exception($"De vloot {vloot.Naam} kon niet toegevoegd worden omdat het al bestaat.");
        }

        public void VerwijderVloot(Vloot vloot)
        {
            var isGelukt = _vlotenOpNaam.Remove(vloot.Naam);
            if (!isGelukt)
                throw new Exception($"De vloot {vloot.Naam} kon niet verwijderd worden omdat het niet bestaat in de rederij.");
        }

        public void VoegHavenToe(Haven haven)
        {
            // Dit probeert toe te voegen, als het niet lukt (omdat de key al bestaat bv.) krijgen we 'false' terug.
            var isGelukt = _havensGesorteerdOpNaam.TryAdd(haven.Naam, haven);
            if (!isGelukt)
                throw new Exception($"De haven {haven.Naam} kon niet toegevoegd worden omdat het al bestaat.");
        }

        public void VerwijderHaven(Haven Haven)
        {
            var isGelukt = _vlotenOpNaam.Remove(Haven.Naam);
            if (!isGelukt)
                throw new Exception($"De haven {Haven.Naam} kon niet verwijderd worden omdat het niet bestaat in de rederij.");
        }

        public Schip ZoekSchip(string naam)
        {
            // _vlotenOpNaam is een key/value pair, we zullen dus ook een KeyValue pair krijgen als we een foreach  gebruiken
            // .Key (Vloot naam) en .Value (Vloot) beschikbaar
            foreach (var pair in _vlotenOpNaam)
            {
                var schip = pair.Value.ZoekSchip(naam);
                if (schip != null)
                    return schip;
            }
            return default;
        }

        public bool HeeftSchip(Schip s)
        {
            return _vlotenOpNaam.Values // We gebruiken hier niet .Vloten omdat we weten dat er meer bewerkingen gedaan worden op .Vloten. We hebben genoeg om rechtstreeks _vlotenOpNaam.Values te gebruiken
                                .Any(vloot => vloot.HeeftSchip(s.Naam));
        }

        public void VerplaatsSchip(string schipNaam, string naarVlootNaam)
        {
            // We zoeken eerst de vloot, want we willen het schip niet verwijderen van zijn huidige vloot als blijkt dat een ongeldige vloot meegegeven was. Dit zou onze data in een ongeldige staat achterlaten.
            if ( ! _vlotenOpNaam.TryGetValue(naarVlootNaam, out var vloot))
                throw new Exception($"De vloot {naarVlootNaam} kon niet gevonden worden.");

            foreach (var vlootOpNaam in _vlotenOpNaam)
            {
                var schip = vlootOpNaam.Value.ZoekSchip(schipNaam);
                if (schip != null)
                {
                    // Verwijder in de huidige vloot
                    vlootOpNaam.Value.VerwijderSchip(schipNaam);

                    // Voeg nu toe aan de nieuwe vloot
                    vloot.VoegSchipToe(schip);
                    break;
                }
            }
        }
    }
}
