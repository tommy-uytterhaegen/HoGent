namespace OefeningScheepvaart.Model
{
    public abstract class CargoSchip : Schip
    {
        public double CargoWaardeInEuro { get; set; }

        public CargoSchip(int lengte, int breedte, int tonnage, string naam) 
            : base(lengte, breedte, tonnage, naam)
        {
        }
    }
}
