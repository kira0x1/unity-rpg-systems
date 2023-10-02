using UnityEngine;

namespace Kira
{
    [RequireComponent(typeof(EntityCharacter))]
    public class PlayerUI : MonoBehaviour
    {
        private EntityCharacter entityCharacter;

        [SerializeField]
        private StatProgressBar healthBar;

        [SerializeField]
        private StatProgressBar manaBar;

        private void Awake()
        {
            entityCharacter = GetComponent<EntityCharacter>();
            Entity entity = entityCharacter.entity;
            healthBar.SetEntity(entity);
            manaBar.SetEntity(entity);
        }
    }
}