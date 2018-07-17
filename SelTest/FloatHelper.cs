namespace SelTest
{
    public static class FloatHelper
    {
        public static float? ParseToNullable(string s)
        {
            float fRes;
            if (!float.TryParse(s, out fRes))
                return null;

            return fRes;
        }
    }
}
