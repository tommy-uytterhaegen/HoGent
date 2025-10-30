namespace OefeningScheepvaart.Model
{
    /// <summary>
    /// NOTE: We gaan in de code exceptions gooien. Typisch zal je boodschappen bedoelt voor een gebruiker niet in een bussines laag gooien. Je zal codes gebruiken, die dan vertaalt worden in de UI laag.
    /// Om deze code niet overcomplex te maken gooi ik hier toch teksten
    /// </summary>
    public class RederijBeheer
    {
        public Rederij Rederij { get; set; }

        public void MaakRederij(List<Vloot> vloten, List<Haven> havens)
        {
            Rederij = new Rederij(vloten, havens);
        }

        public void VoegSchipToe(Vloot v, Schip s)
        {
            /*
            // Onderstaande is via LINQ, maar gezien we de verantwoordelijkheid op de juiste plek willen gaan we het anders aanpakken
            var bestaatSchip = Rederij.Vloten
                                      .Any(vloot => vloot.Schepen
                                                         .Any(schip => schip.Naam == s.Naam));
            */

            if ( Rederij.HeeftSchip(s))
                throw new Exception("Het schip zit al in een vloot.");

            v.VoegSchipToe(s);
        }

        public Schip ZoekSchip(string naam)
        {
            foreach (var vloot in Rederij.Vloten)
            {
                var schip = vloot.ZoekSchip(naam);
                if (schip != null)
                    return schip;
            }

            return default;
        }
    }
}
