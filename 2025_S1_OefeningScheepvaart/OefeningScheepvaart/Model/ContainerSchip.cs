namespace OefeningScheepvaart.Model
{
    public class ContainerSchip : CargoSchip
    {
        public int AantalContainers { get; set; }

        public ContainerSchip(int lengte, int breedte, int tonnage, string naam) 
            : base(lengte, breedte, tonnage, naam)
        {
        }
    }
}
