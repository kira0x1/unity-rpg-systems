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
            _curJob = new CastJob(spell);
            _curJob.OnSpellDoneCast += OnCastDone;
            _castBar.SetSpell(_curJob);
        }

        private void OnCastDone(SpellData spellData)
        {
            entityCharacter.entity.entityStats.mana.Reduce(spellData.cost);
            spellData.OnCast(entityCharacter.entity);
            Debug.Log($"{spellData.spellName} done casting!");
            _isCasting = false;
        }

        private bool CanCast(SpellData spell)
        {
            if (_isCasting)
            {
                Debug.Log("Cannot cast another spell while casting");
                return false;
            }

            float cost = spell.cost;
            float curMana = entityCharacter.entity.entityStats.mana.value;

            if (cost > curMana)
            {
                Debug.Log("Not enough mana to cast this spell");
                return false;
            }

            if (spell.curCD > 0)
            {
                Debug.Log("Spell is on cooldown");
                return false;
            }

            if (spell.requiresTarget)
            {
                Debug.Log("This spell requires a target");
                return false;
            }

            return true;
        }

        public void CancelSpell()
        {
            Debug.Log("Canceled Cast");
            _castBar.CancelCast();
            _isCasting = false;
        }
    }
}