using TMPro;
using UnityEngine;

namespace Kira
{
    public class GameProto : MonoBehaviour
    {
        public Entity entity;
        public float amount = 25;

        [SerializeField]
        private TextMeshProUGUI statsText;
        private EntityStats stats;
        private Stat healthStat;

        private void Start()
        {
            stats = entity.entityStats;

            var content = $"Stats for {entity.name}\n";
            content += $"Health: {stats.health.value} / {stats.health.max}\n";
            content += $"Speed: {stats.speed.value} / {stats.speed.max}";

            statsText.text = content;

            healthStat = entity.GetStat(StatType.HEALTH);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                healthStat.Reduce(amount);
            }

            if (Input.GetKeyDown(KeyCode.G))
            {
                healthStat.Increase(amount);
            }
        }
    }
}