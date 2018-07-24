namespace SelTest.Tokenizers
{
    public static class TokenizerHelper
    {
        public static string GetUnderToken(string age)
        {
            return "under_" + age;
        }

        internal static string GetUnitedToken()
        {
            return "united";
        }

        internal static string Concat(string current, string add)
        {
            if (string.IsNullOrEmpty(current))
            {
                return add;
            }

            return current + add;
        }
    }
}
