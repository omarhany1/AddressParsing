using System;
namespace Task111
{
    public class StringUtilities : IStringUtilities
    {
        public string[] triminput(string[] x)
        {
            for (int i = 0; i < x.Length; i += 1)
            {
                x[i] = x[i].Trim();
            }
            return x;
        }
    }
}
