using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace Server
{
    class DecksMaker
    {
        private const string _filePathSW = "\\Resources\\Cards\\cardsSW.xml";
        private const string _filePathLOTR = "\\Resources\\Cards\\cardsLOTR.xml";
        private const string _filePathMarvel = "\\Resources\\Cards\\cardsMarvel.xml";
        private const string _filePathWarcraft = "\\Resources\\Cards\\cardsWarcraft.xml";

        public static IEnumerable<Card> MakeDeck(DeckType type)
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

       public static IEnumerable<Card> MakeRandomDeck()
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

        private static Card ExtractCard(XElement element)
        {
            switch (element.Element("Type").Value)
            {
                case "Unit":
                    {
                        return new UnitCard(
                            element.Element("Name").Value,
                            element.Element("Image").Value,
                            Convert.ToInt32(element.Element("ResourceCost").Value),
                            element.Element("Description").Value,
                            Convert.ToInt32(element.Element("HP").Value),
                            Convert.ToInt32(element.Element("Damage").Value)
                            );
                    }
                case "Spell":
                    {
                        return new SpellCard(
                            element.Element("Name").Value,
                            element.Element("Image").Value,
                            Convert.ToInt32(element.Element("ResourceCost").Value),
                            element.Element("Description").Value,
                            (SpellType)Enum.Parse(typeof(SpellType), element.Element("SpellType").Value),
                            Convert.ToInt32(element.Element("Power").Value)
                            );
                    }
                default: return null;
            }
        }
        
        static IEnumerable<Card> MakeLOTRDeck()
        {
            var cards = new List<Card>();
            //add all cards

            var document = new XDocument(_filePathLOTR);
            foreach (XElement element in document.Root.Elements())
            {
                cards.Add(ExtractCard(element));
            }

            return cards;
        }

        static IEnumerable<Card> MakeMarvelDeck()
        {
            var cards = new List<Card>();
            //add all cards

            var document = new XDocument(_filePathMarvel);
            foreach (XElement element in document.Root.Elements())
            {
                cards.Add(ExtractCard(element));
            }

            return cards;
        }

        static IEnumerable<Card> MakeWarcraftDeck()
        {
            var cards = new List<Card>();
            //add all cards

            var document = new XDocument(_filePathWarcraft);
            foreach (XElement element in document.Root.Elements())
            {
                cards.Add(ExtractCard(element));
            }

            return cards;
        }

        static IEnumerable<Card> MakeStarWarsDeck()
        {
            var cards = new List<Card>();
            //add all cards

            var document = new XDocument(_filePathSW);
            foreach (XElement element in document.Root.Elements())
            {
                cards.Add(ExtractCard(element));
            }

            return cards;
        }

    }
}
