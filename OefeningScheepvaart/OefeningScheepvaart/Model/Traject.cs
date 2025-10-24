namespace OefeningScheepvaart.Model
{
    public class Traject
    {
        public List<Haven> Havens { get; set; }

        public Traject(List<Haven> havens)
        {
            Havens = havens;
        }
    }
}
