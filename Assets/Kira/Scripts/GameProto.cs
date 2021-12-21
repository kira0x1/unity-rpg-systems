using TMPro;
using UnityEngine;

namespace Kira
{
    public class GameProto : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI statsText;

        public Entity entity;
        private EntityStats stats;

        public float amount = 25;

        private Stat healthStat;

        private void Start()
        {
            stats = entity.entityStats;

            string content = $"Stats for {entity.name}\n";
            content += $"Health: {stats.health.value} / {stats.maxHealth.value}\n";
            content += $"Speed: {stats.speed.value} / {stats.maxSpeed.value}";

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