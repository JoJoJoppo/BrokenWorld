using UnityEngine;
using System.Collections;

namespace BrokenWorld.Game
{
    [System.Serializable] 
    public class EnvironmentSystem : IEnvironmentSystem
    {
        [SerializeField] string _name;
        [SerializeField] ParticleSystem _targetPS;
        [SerializeField] Sprite _airParticle;
        [SerializeField] Vector2 _windirection;
        [SerializeField] float _windStrength;
        [SerializeField] Color _screenTemp;
        [SerializeField] AudioSource _ambientSound;
        [SerializeField] AudioSource _backgroundMusic;
            

        #region Getters & Setters

        public string Name
        {
            get{ return _name; }
            set{ _name = value; }
        }

        public ParticleSystem TargetPS
        {
            get{ return _targetPS; }
            set{ _targetPS = value; }
        }

        public Sprite AirParticle
        {
            get{return _airParticle;}
            set{_airParticle = value;}
        }

        public Vector2 WindDirection
        {
            get{ return _windirection; }
            set{ _windirection = value; }
        }

        public float WindStrength
        {
            get{ return _windStrength; }
            set{ _windStrength = value; }
        }

        public Color ScreenTemp
        {
            get{ return _screenTemp; }
            set{ _screenTemp = value; }
        }

        public AudioSource AmbientSound
        {
            get{ return _ambientSound; }
            set{ _ambientSound = value; }
        }

        public AudioSource BackgroundMusic
        {
            get{ return _backgroundMusic; }
            set{ _backgroundMusic = value; }
        }

        #endregion

    }
}