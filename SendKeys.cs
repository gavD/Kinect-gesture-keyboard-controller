// taken from http://blog.aggregatedintelligence.com/2009/04/net-sending-keys-to-keyboard-buffer.html

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace SkeletalViewer
{
    public class SendKeys
    {
        /// <summary>
        /// Sends the text to the keyboard buffer
        /// </summary>
        /// <param name="text"></param>
        public static void Send(string text)
        {
            if (string.IsNullOrEmpty(text))
                return;

            uint numCharsToSend = (uint)(text.Length * 2); //keydown keyup
            SendInputWin32.INPUT[] structInputArray = new SendInputWin32.INPUT[numCharsToSend];
            int index = 0;
            foreach (char key in text.ToCharArray())
            {
                //key down
                SendInputWin32.INPUT structInputKeyDown = SendInputWin32.CreateNewINPUT(SendInputWin32.INPUT_KEYBOARD);
                structInputKeyDown.ki.dwFlags = SendInputWin32.KEYEVENTF_UNICODE;
                structInputKeyDown.ki.wScan = (ushort)key;
                structInputArray[index++] = structInputKeyDown;

                //key up
                SendInputWin32.INPUT structInputKeyUp = SendInputWin32.CreateNewINPUT(SendInputWin32.INPUT_KEYBOARD);
                structInputKeyUp.ki.dwFlags = SendInputWin32.KEYEVENTF_UNICODE | SendInputWin32.KEYEVENTF_KEYUP;
                structInputKeyUp.ki.wScan = (ushort)key;
                structInputArray[index++] = structInputKeyUp;
            }
            int sizeOfINPUT = Marshal.SizeOf(structInputArray[0]);
            SendInputWin32.SendInput((numCharsToSend), ref structInputArray[0], sizeOfINPUT);
        }
    }
}
