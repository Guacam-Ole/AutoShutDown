namespace AutoShutDown
{
    public class WrongByteFormatArgumentException : ArgumentException
    {
        public WrongByteFormatArgumentException(string value) : base($"downloadspeed must be of Format '<number> xB' where x can be [KMG]. Received {value} instead")
        {
        }
    }
}