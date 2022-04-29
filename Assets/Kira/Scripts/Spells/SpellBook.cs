using System.Collections.Generic;
using UnityEngine;

namespace Kira
{
    public class SpellBook : ScriptableObject
    {
        public List<Spell> spells = new();
    }
}