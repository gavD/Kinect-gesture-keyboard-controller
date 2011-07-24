// adapted from http://blog.aggregatedintelligence.com/2009/04/net-sending-keys-to-keyboard-buffer.html

using System.Runtime.InteropServices;
using System.Security.Principal;
using System;

namespace SkeletalViewer
{
    public class SendInputWin32
    {
        public const ushort INPUT_MOUSE = 0x000;
        public const ushort INPUT_KEYBOARD = 0x0001;
        public const ushort INPUT_HARDWARE = 0x0002;

        public const ushort KEYEVENTF_KEYUP = 0x0002;
        public const ushort KEYEVENTF_KEYDOWN = 0x0000;
        public const ushort KEYEVENTF_SCANCODE = 0x0008;
        public const ushort KEYEVENTF_UNICODE = 0x0004;

        public const ushort XBUTTON1 = 0x0001;
        public const ushort XBUTTON2 = 0x0002;
        public const ushort MOUSEEVENTF_MOVE = 0x0001;
        public const ushort MOUSEEVENTF_LEFTDOWN = 0x0002;
        public const ushort MOUSEEVENTF_LEFTUP = 0x0004;
        public const ushort MOUSEEVENTF_RIGHTDOWN = 0x0008;
        public const ushort MOUSEEVENTF_RIGHTUP = 0x0010;
        public const ushort MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        public const ushort MOUSEEVENTF_MIDDLEUP = 0x0040;
        public const ushort MOUSEEVENTF_XDOWN = 0x0080;
        public const ushort MOUSEEVENTF_XUP = 0x0100;
        public const ushort MOUSEEVENTF_WHEEL = 0x0800;
        public const ushort MOUSEEVENTF_VIRTUALDESK = 0x4000;
        public const ushort MOUSEEVENTF_ABSOLUTE = 0x8000;

        public enum VK : ushort
        {
            SHIFT = 0x10,
            CONTROL = 0x11, //ctrl key
            MENU = 0x12,    //alt key
            ESCAPE = 0x1B,
            BACK = 0x08,
            TAB = 0x09,
            RETURN = 0x0D,
            PRIOR = 0x21,
            NEXT = 0x22,
            END = 0x23,
            HOME = 0x24,
            LEFT = 0x25,
            UP = 0x26,
            RIGHT = 0x27,
            DOWN = 0x28,
            SELECT = 0x29,
            PRINT = 0x2A,
            EXECUTE = 0x2B,
            SNAPSHOT = 0x2C,
            INSERT = 0x2D,
            DELETE = 0x2E,
            HELP = 0x2F,
            NUMPAD0 = 0x60,
            NUMPAD1 = 0x61,
            NUMPAD2 = 0x62,
            NUMPAD3 = 0x63,
            NUMPAD4 = 0x64,
            NUMPAD5 = 0x65,
            NUMPAD6 = 0x66,
            NUMPAD7 = 0x67,
            NUMPAD8 = 0x68,
            NUMPAD9 = 0x69,
            MULTIPLY = 0x6A,
            ADD = 0x6B,
            SEPARATOR = 0x6C,
            SUBTRACT = 0x6D,
            DECIMAL = 0x6E,
            DIVIDE = 0x6F,
            F1 = 0x70,
            F2 = 0x71,
            F3 = 0x72,
            F4 = 0x73,
            F5 = 0x74,
            F6 = 0x75,
            F7 = 0x76,
            F8 = 0x77,
            F9 = 0x78,
            F10 = 0x79,
            F11 = 0x7A,
            F12 = 0x7B,
            OEM_1 = 0xBA,   // ',:' for US
            OEM_PLUS = 0xBB,   // '+' any country
            OEM_COMMA = 0xBC,   // ',' any country
            OEM_MINUS = 0xBD,   // '-' any country
            OEM_PERIOD = 0xBE,   // '.' any country
            OEM_2 = 0xBF,   // '/?' for US
            OEM_3 = 0xC0,   // '`~' for US
            MEDIA_NEXT_TRACK = 0xB0,
            MEDIA_PREV_TRACK = 0xB1,
            MEDIA_STOP = 0xB2,
            MEDIA_PLAY_PAUSE = 0xB3,
            LWIN = 0x5B,        //windows key
            RWIN = 0x5C         //windows key
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MOUSEINPUT
        {
            public int dx;
            public int dy;
            public uint mouseData;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct KEYBDINPUT
        {
            public ushort wVk;
            public ushort wScan;
            /// <summary>
            /// Set flags such as KEYEVENTF_KEYUP, UNICODE, etc
            /// Flags should be combined using bitwise |
            /// </summary>
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct HARDWAREINPUT
        {
            public uint uMsg;
            public ushort wParamL;
            public ushort wParamH;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct INPUT
        {
            [FieldOffset(0)]
            public int type;
            [FieldOffset(4)] //*
            public MOUSEINPUT mi;
            [FieldOffset(4)] //*
            public KEYBDINPUT ki;
            [FieldOffset(4)] //*
            public HARDWAREINPUT hi;
        }


        [DllImport("user32.dll")]
        public static extern uint SendInput(uint nInputs, ref INPUT pInputs, int cbSize);

        public static INPUT CreateNewINPUT(ushort type)
        {
            SendInputWin32.INPUT structInputKeyDown = new SendInputWin32.INPUT();
            structInputKeyDown.type = type;
            structInputKeyDown.ki.wScan = 0;
            structInputKeyDown.ki.time = 0;
            structInputKeyDown.ki.dwFlags = 0;
            structInputKeyDown.ki.dwExtraInfo = IntPtr.Zero;
            return structInputKeyDown;
        }
    }
}