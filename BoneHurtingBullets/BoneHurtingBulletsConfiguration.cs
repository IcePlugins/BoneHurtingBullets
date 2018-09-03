using Rocket.API;
using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoneHurtingBullets
{
    public class BoneHurtingBulletsConfiguration : IRocketPluginConfiguration
    {
        public List<BulletLimbDamageChance> boneBreakingChances;

        public void LoadDefaults()
        {
            boneBreakingChances = new List<BulletLimbDamageChance>();

            var limbs = Enum.GetValues(typeof(ELimb)).Cast<ELimb>().Select(x => x.ToString());

            foreach (var limb in limbs)
            {
                boneBreakingChances.Add(new BulletLimbDamageChance
                {
                    BreakChance = 25,
                    Limb = limb
                });
            }
        }
    }
}
