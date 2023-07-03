namespace DeviceDriverKata
{
    public class DeviceDriver
    {
        private readonly IFlashMemoryDevice _flashMemoryDevice;

        public DeviceDriver(IFlashMemoryDevice flashMemoryDevice)
        {
            _flashMemoryDevice = flashMemoryDevice;
        }

        public byte Read(long address)
        {
            return _flashMemoryDevice.Read(address);
        }

        public void Write(long address, byte data)
        {
            _flashMemoryDevice.Write(0x0, 0x40);
            _flashMemoryDevice.Write(address, data);

            if (_flashMemoryDevice.Read(address) != data)
            {
                throw new Exception();
            }
        }
    }
}