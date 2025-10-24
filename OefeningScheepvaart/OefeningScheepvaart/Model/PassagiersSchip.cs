namespace OefeningScheepvaart.Model
{
    public abstract class PassagiersSchip : Schip
    {
        public int AantalPassagiers { get; set; }
        public Traject Traject { get; set; }

        protected PassagiersSchip(int lengte, int breedte, int tonnage, string naam) 
            : base(lengte, breedte, tonnage, naam)
        {
        }
    }
}
