namespace AutoShutDown.Backend
{
    public static class ByteHelper
    {
        private const string _suffixes = "Bytes,KB,MB,GB";

        public static string Fancy(this long value)
        {
            var fancyValue = GetValueFor(value, out string suffix);
            return $"{fancyValue} {suffix}";
        }

        private static long GetValueFor(long value, out string suffix)
        {
            var suffixArray = _suffixes.Split(',');
            for (int i = 0; i < suffixArray.Length - 1; i++)
            {
                if (value < 1024)
                {
                    suffix = suffixArray[i];
                    return value;
                }
                value /= 1024;
            }
            suffix = "GB";
            return value;
        }

        public static long DownloadLimitValue(this string argument)
        {
            if (!argument.Any(q => char.IsLetter(q))) return Convert.ToInt64(argument);

            var bPos = argument.IndexOf('B');
            if (bPos <= 1) throw new WrongByteFormatArgumentException(argument);
            var prevChar = argument[bPos - 1];
            var numericValue = Convert.ToInt64(argument[..(bPos - 1)]);
            return prevChar switch
            {
                'K' => numericValue * 1024,
                'M' => numericValue * 1024 * 1024,
                'G' => numericValue * 1024 * 1024 * 1024,
                _ => throw new WrongByteFormatArgumentException(argument),
            };
        }
    }
}