using JetBrains.Annotations;
using UnityEngine;

namespace Kira
{
    [RequireComponent(typeof(EntityCharacter))]
    public class EntityCaster : MonoBehaviour
    {
        private EntityCharacter entityCharacter;
        private CastBar _castBar;

        private bool _isCasting;
        private CastJob _curJob;

        [SerializeField]
        private bool enableDebugLogs;

        private void Awake()
        {
            entityCharacter = GetComponent<EntityCharacter>();
            _castBar = FindObjectOfType<CastBar>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (_isCasting)
                {
                    CancelSpell();
                }
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

        private bool CanCast(SpellData spell)
        {
            if (_isCasting)
            {
                if (enableDebugLogs)
                    Debug.Log("Cannot cast another spell while casting");

                return false;
            }

            float cost = spell.cost;
            float curMana = entityCharacter.entity.entityStats.mana.value;

            if (cost > curMana)
            {
                if (enableDebugLogs)
                    Debug.Log("Not enough mana to cast this spell");

                return false;
            }

            if (spell.curCD > 0)
            {
                if (enableDebugLogs)
                    Debug.Log("Spell is on cooldown");

                return false;
            }

            if (spell.requiresTarget && !TargetingManager.HasTarget)
            {
                if (enableDebugLogs)
                    Debug.Log("This spell requires a target");

                return false;
            }

            if (spell.requiresTarget && TargetingManager.HasTarget && !spell.canCastOnDead && TargetingManager.Instance.Target.IsDead)
            {
                if (enableDebugLogs)
                    Debug.Log("Cant cast this spell on a dead target");

                return false;
            }


            return true;
        }

        public void CancelSpell()
        {
            if (enableDebugLogs)
                Debug.Log("Canceled Cast");

            _castBar.CancelCast();
            _isCasting = false;
        }
    }
}