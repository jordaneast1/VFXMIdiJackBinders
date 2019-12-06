using UnityEngine.VFX;
using MidiJack;

namespace UnityEngine.VFX.Utility
{
    [AddComponentMenu("VFX/Property Binders/Midi Vec2 Binder")]
    [VFXBinder("Midi/Vec2")]
    class VFXMidiVec2Binder : VFXBinderBase
    {
        public string Property { get { return (string)m_Property; } set { m_Property = value; } }

        [VFXPropertyBinding("UnityEngine.Vector2"), SerializeField, UnityEngine.Serialization.FormerlySerializedAs("m_Parameter")]
        protected ExposedProperty m_Property = "Vector2Parameter";
        public int knobNumber1;
        public int knobNumber2;
        public float min = 0;
        public float max = 1;
        public SmoothType smoothType = SmoothType.smootherstep;


        public override bool IsValid(VisualEffect component)
        {
            return component.HasVector2(m_Property);
        }

        public override void UpdateBinding(VisualEffect component)
        {
            var f1 = MidiMaster.GetKnob(knobNumber1);
            f1 = f1.Smooth0to1(smoothType);
            f1 = ExtensionMethods.RemapToNumberRange(f1, 0, 1, min, max);
            var f2 = MidiMaster.GetKnob(knobNumber1);
            f2 = f2.Smooth0to1(smoothType);
            f2 = ExtensionMethods.RemapToNumberRange(f2, 0, 1, min, max);
            Vector2 v = new Vector2(f1, f2);
            component.SetVector2(m_Property, v);
        }

        public override string ToString()
        {
            return string.Format("Vector2 : '{0}'", m_Property);
        } 
    }
}
