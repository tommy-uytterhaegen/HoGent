namespace OefeningScheepvaart.Model
{
    public class OlieTanker : TankerSchip
    {

        public OlieTypes LadingType { get; set; }
        public OlieTanker(int lengte, int breedte, int tonnage, string naam) 
            : base(lengte, breedte, tonnage, naam)
        {
        }

    }
}
