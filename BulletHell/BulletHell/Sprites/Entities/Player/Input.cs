namespace BulletHell.Player
{
    using Microsoft.Xna.Framework.Input;

    // Allows for rebinding.
    internal class Input
    {
        private static Keys up = Keys.W;
        private static Keys down = Keys.S;
        private static Keys left = Keys.A;
        private static Keys right = Keys.D;
        private static Keys attack = Keys.Space;

        public static Keys Up { get => up; set => up = value; }

        public static Keys Down { get => down; set => down = value; }

        public static Keys Left { get => left; set => left = value; }

        public static Keys Right { get => right; set => right = value; }

        public static Keys Attack { get => attack; set => attack = value; }

        public static void SetKey(string keyNameToSet, Keys newKey)
        {
            switch (keyNameToSet)
            {
                case "Up":
                    Up = newKey;
                    break;
                case "Down":
                    Down = newKey;
                    break;
                case "Left":
                    Left = newKey;
                    break;
                case "Right":
                    Right = newKey;
                    break;
                case "Attack":
                    Attack = newKey;
                    break;
                default:
                    return;
            }
        }

        public static bool CheckIfAlreadyBinded(Keys key)
        {
            if (Up == key || Down == key || Left == key || Right == key || Attack == key)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
