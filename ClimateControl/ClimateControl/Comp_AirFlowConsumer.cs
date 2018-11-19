using System.Collections.Generic;
using CentralizedClimateControl;
using RimWorld;
using Verse;

namespace FrontierDevelopments.ClimateControl
{
    public class Comp_AirFlowConsumer : CompAirFlowConsumer
    {
        private bool _connected;
    
        public override bool IsOperating()
        {
            return AirFlowNet != null && _connected;
        }

        public override string CompInspectStringExtra()
        {
            return IsOperating()
                ? ConnectedKey.Translate()
                : NotConnectedKey.Translate();
        }
    
        public override IEnumerable<Gizmo> CompGetGizmosExtra()
        {
            foreach (var gizmo in base.CompGetGizmosExtra())
            {
                yield return gizmo;
            }

            if (parent.Faction == Faction.OfPlayer)
            {
                yield return new Command_Toggle
                {
                    icon = Resources.BuildingClimateControlAirThermal,
                    defaultDesc = "fd.heatsink.net.connect.description".Translate(),
                    defaultLabel = "fd.heatsink.net.connect.label".Translate(),
                    isActive = () => _connected,
                    toggleAction = () => _connected = !_connected
                };
            }
        }
    
        public override void PostExposeData()
        {
            base.PostExposeData();
            Scribe_Values.Look(ref _connected, "connected", true);
        }
    }
}