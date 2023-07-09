using System;
using UnityEngine;

namespace CodeBase.Cards.Looper
{
    public class Looper : MonoBehaviour
    {
        [SerializeField] private int tracks;
        public GameObject Track;
        public GameObject TrackFrame;
        
        public void Awake()
        {
            AddTrack();
            AddTrack();
            AddTrack();
        }

        private void Render()
        {
            
        }

        private void AddTrack()
        {
            tracks += 1;
            var trackHeight = 0;
            Instantiate(Track, new Vector3(0, trackHeight * tracks, 0), Quaternion.identity, TrackFrame.transform);
        }

        public void Update()
        {
            Render();
        }
    }
}