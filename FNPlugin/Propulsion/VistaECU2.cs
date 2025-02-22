﻿using LibNoise;

namespace FNPlugin
{
	class ZPinchFusionEngine : VistaECU2 { }


    class VistaECU2 : FusionECU2, IUpgradeableModule
    {
        const float maxMin = defaultMinIsp / defaultMaxIsp;
        const float defaultMaxIsp = 27200;
        const float defaultMinIsp = 15500;
        const float defaultMaxSteps = 100;
        const float defaultSteps = (defaultMaxIsp - defaultMinIsp) / defaultMaxSteps;
        const float stepNumb = 0;

        // Persistant setting
        [KSPField(isPersistant = true, guiActive = true, guiActiveEditor = true, guiName = "Selected Isp"), UI_FloatRange(stepIncrement = defaultSteps, maxValue = defaultMaxIsp, minValue = defaultMinIsp)]
        public float localIsp = defaultMinIsp + (stepNumb * defaultSteps);
       
        // settings
        [KSPField]
        public float neutronAbsorptionFractionAtMinIsp = 0.5f;
        [KSPField]
        public float maxThrustEfficiencyByIspPower = 2;
        [KSPField]
        public float gearDivider = -1;
        [KSPField]
        public float minIsp = defaultMinIsp;
        [KSPField]
        public float initialGearRatio = 0;

        private FloatCurve atmophereCurve;

        protected override FloatCurve BaseFloatCurve
        {
            get { return atmophereCurve ?? curEngineT.atmosphereCurve; }
            set { atmophereCurve = value; }
        }

        protected override bool ShowIspThrottle 
        { 
            get { return Fields["localIsp"].guiActive; } 
            set { Fields["localIsp"].guiActive = value; } 
        } 

        protected override float InitialGearRatio {get { return initialGearRatio; }}
        protected override float SelectedIsp { get { return localIsp; } set { if (value > 0) { localIsp = value; } } }
        protected override float MinIsp { get { return minIsp; } set { if (value <= 10) { minIsp = value + .01f; } else { minIsp = value; } } }
        protected override float MaxIsp { get { return minIsp / maxMin; } }
        protected override float GearDivider { get { return gearDivider >= 0 ? gearDivider : maxMin; } }
        protected override float MaxSteps { get { return defaultMaxSteps; } }
        protected override float MaxThrustEfficiencyByIspPower { get { return maxThrustEfficiencyByIspPower; } }
        protected override float NeutronAbsorptionFractionAtMinIsp { get { return neutronAbsorptionFractionAtMinIsp; } }


    }
}
