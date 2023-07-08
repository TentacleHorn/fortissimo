using System;
using UnityEngine;

namespace Scenes
{
    public class CardBankRefresher : MonoBehaviour
    {
        public bool Enabled = true;
        public float refreshRatems = 3000;
        private float lastChecked;

        public void Awake()
        {
            if (!Enabled) return;
        }

        public void Update()
        {
            
            if (lastChecked == 0f || (Time.time - lastChecked)*1000 > refreshRatems)
            {
                Card.GlobalCardBank.CheckForUpdates();
                lastChecked = Time.time;
            }
        }
    }
}