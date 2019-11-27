using System;
using System.Collections.Generic;
using System.Text;

namespace gulfsoccer.utilities.MDB
{
    /// <summary>
    /// card: BaseCard, cards: Array<ICardData>, headerText: string, gridHeaderLink: string, paragraphTxt: string
    /// </summary>
    public class Grid
    {
        public string type { get; set; }
        public string card { get; set; }
        public List<Card> cards { get; set; }
        public string headerText { get; set; }

        public string gridHeaderLink { get; set; }

        public string paragraphTxt { get; set; }
        public string element { get; set; }

    }
}
