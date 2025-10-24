namespace OefeningScheepvaart.Model
{
    public class GasTanker : TankerSchip
    {
        public GasTypes LadingType { get; set; }

        public GasTanker(int lengte, int breedte, int tonnage, string naam) 
            : base(lengte, breedte, tonnage, naam)
        {
        }

    }
}
