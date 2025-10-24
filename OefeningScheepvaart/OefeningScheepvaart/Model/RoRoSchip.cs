namespace OefeningScheepvaart.Model
{
    public class RoRoSchip : CargoSchip
    {
        public int AantalAutos { get; set; }
        public int AantalTrucks { get; set; }

        public RoRoSchip(int lengte, int breedte, int tonnage, string naam) 
            : base(lengte, breedte, tonnage, naam)
        {
        }
    }
}
