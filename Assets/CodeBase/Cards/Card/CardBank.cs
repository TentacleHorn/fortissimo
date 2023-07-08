using System;
using System.Collections.Generic;
using System.IO;
using CodeBase.Cards.Card;
using UnityEngine;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace CodeBase.Cards.Card
{
    public class CardBank
    {
        public Dictionary<String, CardData> Cards;
        public long LastUpdated;
        public bool IsLoaded;

        public String CardsPath = Path.Join(Global.ResourcePath, "cards");

        private void FetchCards()
        {
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

                    Cards.Add(c.CardName, c);
                });
                // Debug.Log($"Reading cards yml finished in {DateTime.Now - logStart}ms");
            }
            
            var p = LastUpdated;
            LastUpdated = ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeSeconds();
        }

        public void Load()
        {
            if (IsLoaded) return;
            FetchCards();
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
            FetchCards();
        }
    }

    public static class SingletonCardBank
    {
        public static CardBank GlobalCardBank = new();
    }
}