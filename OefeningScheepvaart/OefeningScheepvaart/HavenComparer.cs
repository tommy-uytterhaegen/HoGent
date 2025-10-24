using OefeningScheepvaart.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OefeningScheepvaart
{
    public class HavenComparer : IComparer<Haven>
    {
        public int Compare(Haven? x, Haven? y)
        {
            // 'IgnoreCase' omdat we bijna altijd hoofd en kleine letters als hetzelfde willen beschouwen.
            // 'Ordinal' zal karakter waardes vergelijken en niet de referentie van de string
            return string.Compare(x?.Naam, y?.Naam, StringComparison.OrdinalIgnoreCase);
        }
    }
}
