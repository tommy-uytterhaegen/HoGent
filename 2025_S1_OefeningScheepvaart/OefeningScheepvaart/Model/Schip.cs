namespace OefeningScheepvaart.Model
{
    public abstract class Schip
    {
        public int Lengte { get; set; }
        public int Breedte { get; set; }
        public int Tonnage { get; set; }
        public string Naam { get; set; }

        protected Schip(int lengte, int breedte, int tonnage, string naam)
        {
            Lengte = lengte;
            Breedte = breedte;
            Tonnage = tonnage;
            Naam = naam;
        }

        /// <summary>
        /// Voorbeeld van hoe Equals geïmplementeerd wordt
        /// </summary>
        /// <param name="o">Het object waartegen we willen kijken of de het gelijk is</param>
        /// <returns></returns>
        public override bool Equals(object? o)
        {
            return o is Schip schip &&
                   Lengte == schip.Lengte &&
                   Breedte == schip.Breedte &&
                   Tonnage == schip.Tonnage &&
                   Naam == schip.Naam;
        }

        /// <summary>
        /// Een HashCode is een heel snelle manier om te zien of objecten gelijk zijn zonder de volledige controle te moeten doen. 
        /// Deze moet gelijk zijn voor objecten die als hetzelfde beschouwd worden (meestal objecten met gelijke waarden). Als objecten niet gelijk zijn dan wordt er niet verwacht dat de HashCode verschillend is.
        /// Dit laat toe om veel sneller objecten te vergelijken -> je controlleert hashcode voor elk object, indien gelijk dan is het misschien hetzelfde, dus dan roept men de 'Equals' methode aan, dewelke trager zal zijn.
        /// Hier moeten wij ons zelfde zelf mee bezig houden, de onderliggende objecten regelen dit voor ons, wij implementeren enkel Equals & GetHashCode.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(Lengte, 
                                    Breedte, 
                                    Tonnage, 
                                    Naam);
        }
    }
}
