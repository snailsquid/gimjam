using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ParticleManager : MonoBehaviour
{
    public GameObject E, P;
    private ParticleSystem Enemy, Player;

    private void Start()
    {
        Enemy = E.GetComponent<ParticleSystem>();
        Player = P.GetComponent<ParticleSystem>();
    }

    public void EnemySpawn(Color color)
    {
        var En = Enemy.main;
        En.startColor = color;
        Enemy.Play();
    }
    public void PlayerSpawn(Color color)
    {
        var Pl = Player.main;
        Pl.startColor = color;
        Player.Play();
    }
}
