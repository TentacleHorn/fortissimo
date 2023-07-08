using System;
using System.IO;
using UnityEngine;

namespace Scenes
{
    public class Card : MonoBehaviour
    {
        public string name;
        private void Awake()
        {
            using (var reader = new StreamReader(filepath)) {
                // Load the stream
                var yaml = new YamlStream();
                yaml.Load(reader);
                // the rest
            }
        }
        
        // private void Start()
        // {
        //     throw new NotImplementedException();
        // }

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