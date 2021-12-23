using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kira
{
    [RequireComponent(typeof(EntityCharacter))]
    public class EntityCaster : MonoBehaviour
    {
        private EntityCharacter entityCharacter;
        private Stack castJobs = new Stack();
        private CastBar _castBar;

        [SerializeField]
        private List<Spell> _spells = new List<Spell>();

        private bool _isCasting;
        private CastJob _curJob;

        private void Awake()
        {
            entityCharacter = GetComponent<EntityCharacter>();
            _castBar = FindObjectOfType<CastBar>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                CastNextSpell();
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (_isCasting)
                {
                    CancelSpell();
                }
            }
        }

        public void CastNextSpell()
        {
            if (_isCasting)
            {
                Debug.Log("Cannot cast another spell while casting");
                return;
            }

            _isCasting = true;
            var spell = _spells[0];
            Debug.Log("Casting Spell " + spell.spellName);
            float cost = spell.resourceCost;
            float curMana = entityCharacter.entity.entityStats.mana.value;

            if (cost > curMana)
            {
                Debug.Log("Not enough mana to cast this spell");
                return;
            }

            _curJob = new CastJob(spell.GetData());
            _curJob.OnSpellDoneCast += OnCastDone;
            _castBar.SetSpell(_curJob);
        }

        private void OnCastDone(SpellData spellData)
        {
            entityCharacter.entity.entityStats.mana.Reduce(spellData.cost);
            Debug.Log($"{spellData.spellName} done casting!");
            _isCasting = false;
        }

        public void CancelSpell()
        {
            Debug.Log("Canceled Cast");
            _castBar.CancelCast();
            _isCasting = false;
        }
    }
}