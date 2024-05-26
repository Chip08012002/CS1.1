using UnityEngine;
using static Weapon;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; set; }

    public AudioSource shootingChanel;
    public AudioClip shootingSoundAk47;
    public AudioClip shootingSoundM1911;
    public AudioClip reloadingSoundAk47;


    public AudioClip reloadingSoundM1911;
    public AudioSource emptyManagizeSound;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }

        else
        {
            Instance = this;
        }
    }

    public void PlayShootingSound(WeaponModel weapon)
    {
        switch (weapon)
        {
            case WeaponModel.Ak47:
                shootingChanel.PlayOneShot(shootingSoundAk47);
                break;
            case WeaponModel.M1911:
                shootingChanel.PlayOneShot(shootingSoundM1911);
                break;
        }
    }

    public void PlayReloadSound(WeaponModel weapon)
    {
        switch (weapon)
        {
            case WeaponModel.Ak47:
                shootingChanel.PlayOneShot(reloadingSoundAk47);
                break;
            case WeaponModel.M1911:
                shootingChanel.PlayOneShot(reloadingSoundM1911);
                break;
        }
    }
}
