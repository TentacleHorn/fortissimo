using System;
using UnityEngine;

namespace Scenes
{
    public class CardBankRefresher : MonoBehaviour
    {
        public bool Enabled = true;
        public float refreshRatems = 2000;
        private float lastChecked = 0f;

        public void Awake()
        {
            if (!Enabled) return;
        }

        public void Update()
        {
            if (lastChecked == 0f || Time.time - lastChecked > refreshRatems)
            {
                Card.GlobalCardBank.CheckForUpdates();
                lastChecked = Time.time;
            }
        }
    }
}