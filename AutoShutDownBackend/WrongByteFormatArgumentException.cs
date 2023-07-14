namespace AutoShutDown.Backend
{
    public class WrongByteFormatArgumentException : ArgumentException
    {
        public WrongByteFormatArgumentException(string value) : base($"Downloadspeed must be of Format '<number> xB' where x can be [KMG]. Received {value} instead")
        {
        }
    }
}