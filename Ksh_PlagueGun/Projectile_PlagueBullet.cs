using Verse;
using RimWorld;

namespace Ksh_PlagueGun
{
    public class Projectile_PlagueBullet : Bullet
    {

        // Ovveride
        protected override void Impact(Thing hitThing, bool blockedByShield = false)
        {
            ModExtension_PlagueGun Props = base.def.GetModExtension<ModExtension_PlagueGun>();
            if (Props == null)
                return;

            base.Impact(hitThing, blockedByShield);

            if( Props != null && hitThing != null && hitThing is Pawn hitPawn )
            {
                float rand = Rand.Value;
                if( rand <= Props.addHediffChance )
                {
                    Messages.Message("Ksh_PlagueBullet_SuccessMessage".Translate(this.launcher.Label, hitPawn.Label), MessageTypeDefOf.NeutralEvent);

                    Hediff plagueOnPawn = hitPawn.health?.hediffSet?.GetFirstHediffOfDef(Props.hediffToAdd);

                    float randomSeverity = Rand.Range(0.15f, 0.30f);

                    if( plagueOnPawn != null )
                    {
                        plagueOnPawn.Severity += randomSeverity;
                    }
                    else
                    {
                        Hediff hediff = HediffMaker.MakeHediff(Props.hediffToAdd, hitPawn);
                        hediff.Severity = randomSeverity;
                        hitPawn.health?.AddHediff(hediff);
                    }
                }
            }
        }
    }
}

