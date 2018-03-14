using System.Collections;
using System.Windows.Forms;

namespace WindowsFormsApplication12
{
    internal class Input
    {
        private static Hashtable keyTable = new Hashtable();

        public static bool KeyPressed(Keys Key)
        {
            if(keyTable[Key] ==null)
            {
                return false;
            }
            return (bool) keyTable[Key];
        }

        public static void ChangeState(Keys key,bool state)
        {
            keyTable[key] = state;
        }
    }
}
