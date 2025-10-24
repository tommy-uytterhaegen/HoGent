namespace OefeningScheepvaart.Model
{
    public abstract class TankerSchip : CargoSchip
    {
        public int VolumeInLiters { get; set; }

        protected TankerSchip(int lengte, int breedte, int tonnage, string naam) 
            : base(lengte, breedte, tonnage, naam)
        {
        }

    }
}
