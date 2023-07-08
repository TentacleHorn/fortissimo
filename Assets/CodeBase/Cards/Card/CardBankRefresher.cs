using UnityEngine;
using static CodeBase.Cards.Card.SingletonCardBank;

namespace CodeBase.Cards.Card
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
            if (lastChecked == 0f || (Time.time - lastChecked) * 1000 > refreshRatems)
            {
                GlobalCardBank.CheckForUpdates();
                lastChecked = Time.time;
            }
        }
    }
}