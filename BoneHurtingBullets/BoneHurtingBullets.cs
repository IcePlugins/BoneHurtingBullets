using Rocket.Core.Plugins;
using SDG.Unturned;
using Steamworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace BoneHurtingBullets
{
    public class BoneHurtingBullets : RocketPlugin<BoneHurtingBulletsConfiguration>
    {
        private static readonly System.Random rand = new System.Random();

        protected override void Load()
        {
            Rocket.Core.Logging.Logger.Log("BoneBreakingBullets Loaded, Chances:\n" + String.Join("\n", Configuration.Instance.boneBreakingChances.Select(x => $"{x.Limb}: {x.BreakChance}%").ToArray()));
            DamageTool.playerDamaged += OnPlayerDamage;
        }

        protected override void Unload()
        {
            DamageTool.playerDamaged -= OnPlayerDamage;
        }

        private void OnPlayerDamage(Player player, ref EDeathCause cause, ref ELimb limb, ref CSteamID killer, ref Vector3 direction, ref float damage, ref float times, ref bool canDamage)
        {
            if (cause != EDeathCause.GUN || cause != EDeathCause.PUNCH || cause != EDeathCause.MELEE)
                return;

            var limbName = limb.ToString();

            var chance = Configuration.Instance.boneBreakingChances.FirstOrDefault(x => x.Limb == limbName);
            
            if (chance != null && rand.Next(1, 101) <= chance.BreakChance)
            {
                player.life.breakLegs();
            }
        }
    }
}
