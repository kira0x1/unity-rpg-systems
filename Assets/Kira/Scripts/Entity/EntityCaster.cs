using UnityEngine;

namespace Kira
{
    [RequireComponent(typeof(EntityCharacter), typeof(EntityController))]
    public class EntityCaster : MonoBehaviour
    {
        [SerializeField]
        private bool enableDebugLogs;

        private EntityCharacter entityCharacter;
        private EntityController entityController;
        private CastBar _castBar;
        private CastJob _curJob;
        private bool _isCasting;

        private void Awake()
        {
            entityCharacter = GetComponent<EntityCharacter>();
            entityController = GetComponent<EntityController>();
            _castBar = FindObjectOfType<CastBar>();
        }

        private void Update()
        {
            if (!_isCasting) return;

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                CancelSpell();
            }

            if (!CanMoveCast(_curJob.spellData))
            {
                CancelSpell();
            }
        }

        public void CastSpell(SpellData spell)
        {
            var canCast = CanCast(spell);
            if (!canCast) return;

            _isCasting = true;

            Entity target = entityCharacter.entity;
            if (spell.requiresTarget) target = TargetingManager.Instance.Target.entity;

            _curJob = new CastJob(spell, target);
            _curJob.OnSpellDoneCast += OnCastDone;
            _castBar.SetSpell(_curJob);
        }

        private void OnCastDone(SpellData spellData, Entity entity)
        {
            entityCharacter.entity.entityStats.mana.Reduce(spellData.cost);
            spellData.OnCast(entity);

            if (enableDebugLogs)
                Debug.Log($"{spellData.spellName} done casting!");

            _isCasting = false;
        }

        /// <summary>
        /// Checks if can cast a spell
        /// I.E if has target, resources, is alive, etc.
        /// </summary>
        /// <param name="spell"></param>
        /// <returns></returns>
        private bool CanCast(SpellData spell)
        {
            // Casting Check
            if (_isCasting)
            {
                if (enableDebugLogs)
                    Debug.Log("Cannot cast another spell while casting");

                return false;
            }

            float cost = spell.cost;
            float curMana = entityCharacter.entity.entityStats.mana.value;

            // Mana Check
            if (cost > curMana)
            {
                if (enableDebugLogs)
                    Debug.Log("Not enough mana to cast this spell");

                return false;
            }

            // Cooldown Check
            if (spell.curCD > 0)
            {
                if (enableDebugLogs)
                    Debug.Log("Spell is on cooldown");

                return false;
            }

            // Target Check
            if (spell.requiresTarget && !TargetingManager.HasTarget)
            {
                if (enableDebugLogs)
                    Debug.Log("This spell requires a target");

                return false;
            }

            // Death Check
            if (spell.requiresTarget && TargetingManager.HasTarget && !spell.canCastOnDead && TargetingManager.Instance.Target.IsDead)
            {
                if (enableDebugLogs)
                    Debug.Log("Cant cast this spell on a dead target");

                return false;
            }

            // Movement Check
            if (!CanMoveCast(spell))
            {
                if (enableDebugLogs)
                {
                    Debug.Log("Cant cast this spell while moving");
                }

                return false;
            }

            return true;
        }

        public bool CanMoveCast(SpellData spell) => !(!spell.canCastOnMove && (!entityController.IsGrounded || entityController.IsMoving));

        public void CancelSpell()
        {
            if (enableDebugLogs)
                Debug.Log("Canceled Cast");

            _castBar.CancelCast();
            _isCasting = false;
        }
    }
}