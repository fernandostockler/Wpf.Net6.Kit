using System;
using System.Text;
using System.Windows.Input;

namespace Wpf.Net6.Kit.Controls.Shared
{
    /// <summary>
    /// This class represents a keyboard key and its modifiers.
    /// </summary>
    public class KioskExitKeyGesture
    {
        public ModifierKeys[] ModifierKeys { get; private set; } = Array.Empty<ModifierKeys>();

        public Key Key { get; private set; } = Key.None;

        public KioskExitKeyGesture() { }

        public KioskExitKeyGesture(Key key, ModifierKeys[] modifierKeys)
        {
            Key = key;
            ModifierKeys = modifierKeys;
        }

        public override string ToString()
        {
            StringBuilder sb = new();
            foreach (ModifierKeys modifier in ModifierKeys)
            {
                _ = sb.Append(modifier.ToString());
                _ = sb.Append(" + ");
            }
            _ = sb.Append(Key.ToString());
            return sb.ToString();
        }
    }
}