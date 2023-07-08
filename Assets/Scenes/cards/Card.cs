using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Scenes
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

    public class CardBank
    {
        public Dictionary<String, CardData> Cards;
        public bool IsLoaded;

        public String CardsPath = Path.Join(Global.ResourcePath, "cards");

        public void Load()
        {
            if (IsLoaded) return;

            Debug.Log("Reading cards yml");
            var logStart = DateTime.Now;
            
            Cards = new Dictionary<string, CardData>();
            string yamlContent;
            var files = Directory.EnumerateFiles(CardsPath, "*.yaml", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                using (var reader = new StreamReader(file.Normalize()))
                {
                    yamlContent = reader.ReadToEnd();
                }

                var deserializer = new DeserializerBuilder()
                    .WithNamingConvention(PascalCaseNamingConvention.Instance)
                    .Build();
                var data = deserializer.Deserialize<List<CardData>>(yamlContent);
                data.ForEach(c =>
                {
                    if (Cards.ContainsKey(c.CardName))
                    {
                        throw new Exception($"card with the name {c.CardName} already exists");
                    }
                    Debug.Log($"added card {c.CardName}");
                    Cards.Add(c.CardName, c);
                });
            }
            Debug.Log($"Reading cards yml finished in {DateTime.Now - logStart}ms");
            IsLoaded = true;
        }

        public void CheckForUpdates()
        {
            if (!IsLoaded) return;
            /*TODO: optimize
                store a table of file md5 hashes.
                and check against the table before loading into yaml.
                also add benchmark of loading time first.
            */
            Load();
        }
    }

    public class Card : MonoBehaviour
    {
        public string CardName;
        public static CardBank GlobalCardBank = new();
        public TextMeshPro Title;

        private CardData _cardData;

        private void Awake()
        {
            GlobalCardBank.Load();
            _cardData = GlobalCardBank.Cards[CardName];
            UpdateVisuals();
        }

        private void UpdateVisuals()
        {
            TextMeshProUGUI title = gameObject.transform.Find("Title").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI description = gameObject.transform.Find("Description").GetComponent<TextMeshProUGUI>();
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

        private void Update()
        {
            throw new NotImplementedException();
        }

        // private void OnGUI()
        // {
        //     throw new NotImplementedException();
        // }
    }
}