using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class DecksMaker
    {
        static IEnumerable<Card> MakeDeck(DeckType type)
        {
            switch (type)
            {
                case DeckType.LOTR:
                    {
                        return MakeLOTRDeck();
                    }
                case DeckType.Marvel:
                    {
                        return MakeMarvelDeck();
                    }
                case DeckType.StarWars:
                    {
                        return MakeWarcraftDeck();
                    }
                case DeckType.Warcraft:
                    {
                        return MakeStarWarsDeck();
                    }
                default: return null;
            }
        }

        static IEnumerable<Card> MakeRandomDeck()
        {
            var randomizer = new Random();
            double n = randomizer.NextDouble();
            if (n < 0.25)
                return MakeLOTRDeck();
            else if (n < 0.5)
                return MakeWarcraftDeck();
            else if (n < 0.75)
                return MakeStarWarsDeck();
            return MakeMarvelDeck();
        }

        static IEnumerable<Card> MakeLOTRDeck()
        {
            IEnumerable<Card> cards = new List<Card>();
            //add all cards
            return cards;
        }

        static IEnumerable<Card> MakeMarvelDeck()
        {
            IEnumerable<Card> cards = new List<Card>();
            //add all cards
            return cards;
        }

        static IEnumerable<Card> MakeWarcraftDeck()
        {
            IEnumerable<Card> cards = new List<Card>();
            //add all cards
            return cards;
        }

        static IEnumerable<Card> MakeStarWarsDeck()
        {
            IEnumerable<Card> cards = new List<Card>();
            //add all cards
            return cards;
        }

    }
}
