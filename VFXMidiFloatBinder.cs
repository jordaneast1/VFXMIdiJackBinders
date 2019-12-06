using UnityEngine.VFX;
using MidiJack;

namespace UnityEngine.VFX.Utility
{
    [AddComponentMenu("VFX/Property Binders/Midi Float Binder")]
    [VFXBinder("Midi/Float")]
    class VFXMidiFloatBinder : VFXBinderBase
    {
        public string Property { get { return (string)m_Property; } set { m_Property = value; } }

        [VFXPropertyBinding("System.Single"), SerializeField, UnityEngine.Serialization.FormerlySerializedAs("m_Parameter")]
        protected ExposedProperty m_Property = "FloatParameter";
        public int knobNumber;
        public float min = 0;
        public float max = 1;
        public SmoothType smoothType = SmoothType.smootherstep;


        public override bool IsValid(VisualEffect component)
        {
            return component.HasFloat(m_Property);
        }

        public override void UpdateBinding(VisualEffect component)
        {
            var f = MidiMaster.GetKnob(knobNumber);
            f = f.Smooth0to1(smoothType);
            f = ExtensionMethods.RemapToNumberRange(f, 0, 1, min, max);
            component.SetFloat(m_Property, f);
        }

        public override string ToString()
        {
            return string.Format("Float : '{0}'", m_Property);
        } 
    }
}
