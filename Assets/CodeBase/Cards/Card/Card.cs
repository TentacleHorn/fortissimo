using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static CodeBase.Cards.Card.SingletonCardBank;

namespace CodeBase.Cards.Card
{
    public enum CardType
    {
        beat,
        spell,
        synergy,
        minion
    }

    public class CardData
    {
        public String CardName;
        public String FlavorText = "";
        public String Description;
        public String Effect;
        public CardType Type;
        public Dictionary<String, String> Additional;
    }
    
    public class Card : MonoBehaviour
    {
        public string cardName;
        public TextMeshProUGUI title;
        public TextMeshProUGUI description;

        private CardData _cardData;
        private long _upToDate;

        public void Awake()
        {
            GlobalCardBank.Load();
            _cardData = GlobalCardBank.Cards[cardName];
            UpdateVisuals();
        }

        private void UpdateVisuals()
        {
            if (Math.Abs(_upToDate -GlobalCardBank.LastUpdated) < 0.01) return;
            _cardData = GlobalCardBank.Cards[cardName];

            _upToDate = GlobalCardBank.LastUpdated;
            title.text = _cardData.CardName;
            description.text = _cardData.Description;

            var flavor = "";
            if (!description.text.Equals(""))
            {
                flavor = "\n\n";
            }
            flavor += _cardData.FlavorText;
            description.text += flavor;
        }

        public void FixedUpdate()
        {
            UpdateVisuals(); // this will only do something in the case card data was changed
        }
    }
}