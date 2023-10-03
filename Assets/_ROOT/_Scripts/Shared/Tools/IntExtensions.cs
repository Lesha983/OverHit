namespace Chillplay.Tools
{
    public static class IntExtensions
    {
        private static readonly string[] Postfixes = {"", "k", "M", "B", "T", "P", "E", "Z", "Y", "Aa", "Bb", "Cc", "Dd", "Ee", "Ff", "Gg"};
        
        public static string ToStringPostfix(this int value)
        {
            var postfix = "";
            var order = 0;
            var buffer = (double)value;

            if (buffer >= 1000)
            {
                while (buffer >= 1000)
                {
                    buffer /= 1000;
                    order++;
                }

                if (order >= Postfixes.Length)
                {
                    return "Infinity";
                }

                postfix = Postfixes[order];
            }

            if (order == 0)
            {
                return ((int)buffer).ToString();
            }

            return $"{(buffer * 100 / 100):F2}{postfix}";
        }
    }
}