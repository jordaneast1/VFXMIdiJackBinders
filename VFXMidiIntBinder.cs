using UnityEngine.VFX;
using MidiJack;

namespace UnityEngine.VFX.Utility
{
    [AddComponentMenu("VFX/Property Binders/Midi Int Binder")]
    [VFXBinder("Midi/Int")]
    class VFXMidiIntBinder : VFXBinderBase
    {
        public string Property { get { return (string)m_Property; } set { m_Property = value; } }

        [VFXPropertyBinding("System.Int32"), SerializeField, UnityEngine.Serialization.FormerlySerializedAs("m_Parameter")]
        protected ExposedProperty m_Property = "Int32Parameter";
        public int knobNumber;
        public int min = 0;
        public int max = 1;
        public SmoothType smoothType = SmoothType.smootherstep;


        public override bool IsValid(VisualEffect component)
        {
            return component.HasInt(m_Property);
        }

        public override void UpdateBinding(VisualEffect component)
        {
            var f = MidiMaster.GetKnob(knobNumber);
            f = f.Smooth0to1(smoothType);
            int i = Mathf.RoundToInt(ExtensionMethods.RemapToNumberRange(f, 0, 1, min, max));
            component.SetInt(m_Property, i);
        }

        public override string ToString()
        {
            return string.Format("Int : '{0}'", m_Property);
        } 
    }
}
