using System;
using System.Runtime.InteropServices;

namespace Win32LowAPI {

    public static class LowLevelInput {

        [Flags]
        enum MouseEventFlags {
            LEFTDOWN = 0x00000002,
            LEFTUP = 0x00000004,
            MIDDLEDOWN = 0x00000020,
            MIDDLEUP = 0x00000040,
            MOVE = 0x00000001,
            ABSOLUTE = 0x00008000,
            RIGHTDOWN = 0x00000008,
            RIGHTUP = 0x00000010
        }

#if UNITY_STANDALONE_WIN
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, UIntPtr dwExtraInfo);
#endif
        public static void MouseMove(int x, int y) {
#if UNITY_STANDALONE_WIN
            mouse_event((uint)(MouseEventFlags.ABSOLUTE | MouseEventFlags.MOVE), (uint)x, (uint)y, 0, UIntPtr.Zero);
#endif
        }

    }

}