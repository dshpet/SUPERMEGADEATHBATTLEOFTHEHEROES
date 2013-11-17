using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    //cards and else
    abstract class Card
    {
        private string _name;  //title of the card in the deck or smthng
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _image; //image on top of the card
        public string Image
        {
            get { return _image; }
            set { _image = value; }
        }

        private int _resourceCost; //resource needed fro this card to be activated
        public int ResourceCost
        {
            get { return _resourceCost; }
            set { _resourceCost = value; }
        }

        private string _id;             //we can avoid using ID's
        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        //constructors are below
        protected Card(string name, string image, int resourceCost, string id)
        {
            _name = name;
            _image = image;
            _resourceCost = resourceCost;
            _id = id;
        }

        protected Card()
        {
            throw new Exception("Not enough parametrs to create a card");
        }
    }
    
    
    
    class UnitCard : Card
    {
        private int _HP; //Unit health points
        public int HP
        {
            get { return _HP; }
            //set { _HP = value; } you dont set this
        }

        private int _damage; //Damage that can be dealt
        public int Damage
        {
            get { return _damage; }
            //set { _damage = value; } and this
        }

        private bool _onCooldown = false;
        public bool OnCooldown
        {
            get { return _onCooldown; }
            set { _onCooldown = value; }
        }

        //constructors are below
        public UnitCard(string name, string image, int resourceCost, string id, int HP, int damage)
            : base(name, image, resourceCost, id)
        {
            _HP = HP;
            _damage = damage;
        }
        public UnitCard()
            : base()
        {
            ; //just calling base exception //wtf? why u need this?
        }

        //Methods part of this card below
        public void ChangeDamage(int amount) //changing on amount
        {
            if (_damage != null)
                _damage += amount;
        }
        public void ChangeHP(int amount) //same
        {
            if (_HP != null)
                _HP += amount;
        }
        public bool IsAlive() //checking if we can use this card
        {
            if (_HP != null)
                return _HP > 0;
            return false;
        }
        public bool IsOnCooldown()
        {
            return _onCooldown;
        }

        public void StartCooldown()
        {
            _onCooldown = true;
        }
        //end of method parts
    }
   
    
    
    
    class SpellCard : Card
    {
        private SpellType _type; //type of this spellcard (from enum)
        public SpellType Type
        {
            get { return _type; }
            //set { _type = value; } you dont set these things
        }

        private int _power; //amount of smthing that shows influence rate
        public int Power
        {
            get { return _power; }
            //set { _power = value; }
        }

        //constructors are below
        public SpellCard(string name, string image, int resourceCost,
                         string id, SpellType type, int power)
            : base(name, image, resourceCost, id)
        {
            _type = type;
            _power = power;
        }
        public SpellCard()
            : base() //just calling base exception
        {
            ;
        }
    }
    
    
    
    enum SpellType //special enum for spelltypes
    {
        DirectDamage,
        AllAoeDamage,
        NearTargetDamage,
        RandomTargetsDamage,
        AoeHeal,
        DirectHeal,
        DirectAttackBuff,
        DirectAttackDebuff,
        AoeAttackBuff,
        AoeAttackDebuff,
        DirectDisable
    }

    enum DeckType
    {
        Warcraft,
        Marvel,
        LOTR,
        StarWars
    }

}
