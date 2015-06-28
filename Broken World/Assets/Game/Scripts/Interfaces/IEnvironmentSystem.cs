using UnityEngine;
using System.Collections;

namespace BrokenWorld.Game
{ 
    public interface IEnvironmentSystem 
    {
        string Name { get; set; }
       
        ParticleSystem TargetPS { get; set; }
        Sprite AirParticle { get; set; }
        Vector2 WindDirection { get; set; }
        float WindStrength { get; set; }
       
        Color ScreenTemp { get; set; }
        
        AudioSource AmbientSound { get; set; }
        AudioSource BackgroundMusic { get; set; }
    }
}