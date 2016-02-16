using System.Collections;
using System.Linq;

namespace Nancy.Core.Http
{
    public static class HttpUtility
    {
        private const string Digit = "0123456789";

        private const string Token = "!#$%&'*+-.^_`|~" + Digit + Alpha;

        private const string Alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

        public static bool IsValidToken(string value)
        {
            // TODO: Store BitArray in a static field.

            var minValue = Token.Min(x => x);

            var maxValue = Token.Max(x => x);

            var length = maxValue - minValue + 1;

            var bitArray = new BitArray(length: length);

            foreach (var tokenChar in Token)
            {
                bitArray.Set(tokenChar - minValue, true);
            }

            foreach (var @char in value)
            {
                var index = @char - minValue;

                if (index < 0 || index >= bitArray.Length)
                {
                    return false;
                }

                if (!bitArray[index])
                {
                    return false;
                }
            }

            return true;
        }
    }
}