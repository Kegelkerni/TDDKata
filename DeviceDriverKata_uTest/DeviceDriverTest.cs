using DeviceDriverKata;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace DeviceDriverKata_uTest
{
    [TestFixture]
    public sealed class DeviceDriverTest
    {
        [TestCase(0x00)]
        [TestCase(0xFF)]
        public void Read_ReadsFromFlashMemoryDevice_WithGivenAddress(long address)
        {
            var flashMemoryDevice = Substitute.For<IFlashMemoryDevice>();
            var classUnderTest = new DeviceDriver(flashMemoryDevice);

            classUnderTest.Read(address);

            flashMemoryDevice.Received(1).Read(address);
        }

        [TestCase(0xFF, 0b0)]
        [TestCase(0xFF, 0b11111111)]
        public void Read_ReturnsValueReturnedByFlashMemoryDevice(long address, byte expectedReadResult)
        {
            var flashMemoryDevice = Substitute.For<IFlashMemoryDevice>();
            flashMemoryDevice.Read(address).Returns(expectedReadResult);
            var classUnderTest = CreateClassUnderTest(flashMemoryDevice);

            byte readResult = classUnderTest.Read(address);

            Assert.AreEqual(expectedReadResult, readResult);
        }

        [Test]
        public void Write_ShouldBeginWritingOnFlashMemoryDevice_ByWritingTheProgramCommand0x40_ToAddress0x0()
        {
            var flashMemoryDevice = Substitute.For<IFlashMemoryDevice>();
            flashMemoryDevice.Read(0x01).Returns<byte>(0b111);
            var classUnderTest = CreateClassUnderTest(flashMemoryDevice);

            classUnderTest.Write(0x01, 0b111);

            flashMemoryDevice.ReceivedCalls().First().GetMethodInfo().Name.Should().Be(nameof(flashMemoryDevice.Write));
            flashMemoryDevice.ReceivedCalls().First().GetArguments().First().Should().Be(0x0);
            flashMemoryDevice.ReceivedCalls().First().GetArguments().Last().Should().Be(0x40);
        }

        [TestCase(0x01, 0b00000001)]
        [TestCase(0xFF, 0b11111111)]
        public void Write_CallsWriteOnFlashMemoryDevice_WithTheGivenDataAndTheGivenAddress(long address, byte data)
        {
            var flashMemoryDevice = Substitute.For<IFlashMemoryDevice>();
            flashMemoryDevice.Read(address).Returns(data);
            var classUnderTest = CreateClassUnderTest(flashMemoryDevice);

            classUnderTest.Write(address, data);

            flashMemoryDevice.Received(1).Write(address, data);
        }

        [Test]
        public void Write_ReadsTheDataAtTheGivenAddressAgainAfterWriting_AndSucceedsIfTheValueIsCorrect()
        {
            var flashMemoryDevice = Substitute.For<IFlashMemoryDevice>();
            flashMemoryDevice.Read(0x01).Returns<byte>(0b00000001);
            var classUnderTest = CreateClassUnderTest(flashMemoryDevice);

            Action act = () => classUnderTest.Write(0x01, 0b00000001);

            act.Should().NotThrow();
        }

        [Test]
        public void Write_ReadsTheDataAtTheGivenAddressAgainAfterWriting_AndThrowsIfTheValueIsIncorrect()
        {
            var flashMemoryDevice = Substitute.For<IFlashMemoryDevice>();
            flashMemoryDevice.Read(0x01).Returns<byte>(0b01010101);
            var classUnderTest = CreateClassUnderTest(flashMemoryDevice);

            Action act = () => classUnderTest.Write(0x01, 0b00000001);

            act.Should().Throw<Exception>();
        }

        private static DeviceDriver CreateClassUnderTest(IFlashMemoryDevice flashMemoryDevice)
        {
            return new DeviceDriver(flashMemoryDevice);
        }
    }
}