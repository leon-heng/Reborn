using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Reborn
{    public class PlayerBehavior : MonoBehaviour
    {
        [SerializeField] private PlayerInput _PlayerInput;
        [SerializeField] public BulletPool bulletPool;
        [SerializeField] private Transform gunPos;
        [SerializeField] private Transform firingPos;

        [SerializeField] public NavigationBaker nb;
        [SerializeField] public GameManager gameManager;
        [SerializeField] public Atlas atlas;
        [SerializeField] private GameObject turretPrefab;

        [SerializeField] private float health = 4f;
        [SerializeField] public HealthBar healthBar;

        // Sound Effects
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip fireSFX;
        [SerializeField] private float sfxVolume = 1f;

        private void Update()
        {
            if (_PlayerInput.IsAttack)
            {
                audioSource.PlayOneShot(fireSFX, sfxVolume);
                GameObject bullet = bulletPool.GetBullet();
                if (bullet != null)
                {
                    bullet.transform.position = firingPos.transform.position;
                    bullet.transform.rotation = gunPos.transform.rotation;
                    bullet.SetActive(true);
                }
            }

            if (_PlayerInput.IsSudoku)
            {
                CommitSoduku();
            }
        }

        public void TakeDamage(float damagePoint)
        {
            health -= damagePoint;
            healthBar.SetHealth((int)health);
            if (health <= 0)
            {
                CommitSoduku();
            }
        }

        private void CommitSoduku()
        {
            health = 4;
            healthBar.SetHealth((int)health);
            SpawnTurret();
            Die();
        }

        private void Die()
        {
            this.gameObject.SetActive(false);
            gameManager.PlayerDie();
        }

        private void SpawnTurret()
        {
            Vector3 pos = new Vector3 ((int)this.transform.position.x, 0.5f, (int)this.transform.position.z);
            GameObject newTurret = Instantiate(turretPrefab, pos, Quaternion.identity);
            Turret turret = newTurret.GetComponent<Turret>();
            turret.bulletPool = this.bulletPool;
            turret.atlas = this.atlas; ;
            turret.nb = this.nb;
            nb.BakeNavMesh();
        }
    }
}