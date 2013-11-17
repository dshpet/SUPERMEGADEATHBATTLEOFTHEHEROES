using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Server
{
    class Player
    {
        private IPAddress _ip;
        public IPAddress IP
        {
            get { return _ip; }
            //set { _ip = value; }
        }

        private DeckType _deck;
        public DeckType Deck
        {
            get { return _deck; }
            //set { _deck = value; }
        }

        private IEnumerable<Card> _hand;

        private IEnumerable<Card> _currentDeck;

        private IEnumerable<Card> _activeCards;

        private IEnumerable<Card> _defence;

        private int _hp;
        public int HP
        {
            get { return _hp; }
            //set { _hp = value; } // ne nado nam set zdes'. budet metod
        }

        private int _resource;
        public int Resource
        {
            get { return _resource; }
            //set { _resource = value; } //i zdes toje
        }

        #region METHODS
        // COMMENT IF YOU DO ANY SHIT
        //methods part

        private void MoveCard(ref IEnumerable<Card> from, int index, ref IEnumerable<Card> where)
        {
            var a = where.ToList<Card>();
            a.Add(from.ToList<Card>()[index]);
            where = a;

            a = from.ToList<Card>();
            a.RemoveAt(index);
            from = a;
        }

        public void TakeDamage(int amount)
        {
            if (amount >= 0)
                _hp += amount;
        }

        public void DrawCard()
        {
            var randomizer = new Random();
            MoveCard(ref _currentDeck,
                randomizer.Next(0, _currentDeck.Count()),
                ref _hand);
        }

        public void ActivateCard(int index) //from hand to active cards
        {
            MoveCard(ref _hand, index, ref _activeCards);
        }
        public void DefendWithCard(int index)
        {
            MoveCard(ref _activeCards, index, ref _defence);
            (_defence.Last() as UnitCard).StartCooldown();
        }

        public void DefenseBackToActive()
        {
            _activeCards.Concat(_defence);

            var a = _defence.ToList<Card>();
            a.Clear();
            _defence = a;
        }

        public bool CardTakeDamage(ref IEnumerable<Card> where, int amount, int index)
        {
            var a = where.ToList<Card>();
            var unit = (a[index] as UnitCard);
            unit.ChangeHP(amount * -1);

            if (!unit.IsAlive()) 
            {
                a.RemoveAt(index);
                return true;
            }

            where = a;
            return false;
        }

        #endregion
    }
}
