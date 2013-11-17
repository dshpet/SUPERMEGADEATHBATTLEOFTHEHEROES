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
        }

        private DeckType _deck;
        public DeckType Deck
        {
            get { return _deck; }
        }

        private IEnumerable<Card> _hand;

        private IEnumerable<Card> _currentDeck;

        private IEnumerable<Card> _activeCards;

        private IEnumerable<Card> _defence;

        private int _hp;
        public int HP
        {
            get { return _hp; }
        }

        private int _resource;
        public int Resource
        {
            get { return _resource; }
        }

        private int _maxResource;
        public int MaxResource
        {
            get { return _maxResource; }
        }

        public Player(DeckType deck, IPAddress ip)
        {
            _deck = deck;
            _ip = ip;
            _hand = new List<Card>();
            _activeCards = new List<Card>();
            _defence = new List<Card>();
            if (deck != null)
                _currentDeck = DecksMaker.MakeDeck(deck);
            else
                _currentDeck = DecksMaker.MakeRandomDeck();
            _hp = 25;
            _maxResource = 1;
            _resource = 1;
        }

        #region METHODS
        // COMMENT IF YOU DO ANY SHIT
        //methods part
        public void SetUpNewTurn()
        {
            DefenseBackToActive();
            if (_maxResource < 10) _maxResource++;
            _resource = _maxResource;
        }

        private void MoveCard(ref IEnumerable<Card> from, int index, ref IEnumerable<Card> where)
        {
            var a = where.ToList<Card>();
            a.Add(from.ToList<Card>()[index]);
            where = a;

            a = from.ToList<Card>();
            a.RemoveAt(index);
            from = a;
        }

        public bool IsAlive()
        {
            return _hp > 0;
        }

        public void TakeDamage(int amount)
        {
            if (amount > 0)
                _hp -= amount;
        }

        public void Heal(int amount)
        {
            _hp += amount;
            if (_hp > 50) _hp = 50;
        }

        public void UseResource(int amount)
        {
            if (amount <= _resource)
                _resource -= amount;
        }

        public bool HasCardsInDefense()
        {
            return _defence.Count() > 0;
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

        private bool ChangeUnitHP(ref IEnumerable<Card> where, int amount, int index)
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
        
        public bool ChangeDefensiveUnitHP(int amount, int index)
        {
            return ChangeUnitHP(ref _defence, amount, index);
        }

        public bool ChangeActiveUnitHP(int amount, int index)
        {
            return ChangeUnitHP(ref _activeCards, amount, index);
        }

        private void ChangeUnitAttack(ref IEnumerable<Card> where, int amount, int index)
        {
            var a = where.ToList<Card>();
            var unit = (a[index] as UnitCard);
            if (unit.Damage >= amount)
                unit.ChangeDamage(amount);
            else unit.ChangeDamage(unit.Damage * -1);

            where = a;
        }

        public void ChangeDefensiveCardAttack(int amount, int index)
        {
            ChangeUnitAttack(ref _defence, amount, index);
        }

        public void ChangeActiveCardAttack(int amount, int index)
        {
            ChangeUnitAttack(ref _activeCards, amount, index);    
        }

        #endregion
    }
}
