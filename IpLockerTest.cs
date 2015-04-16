using System;
using System.Net;
using System.Threading;
using FluentAssertions;
using NUnit.Framework;

namespace IpAddressLocker
{


    [TestFixture]
    partial class IpLockerTest
    {
        [Test]
        public void DefederIsWorkingWithDifferentIps()
        {
            //arrange
            var defender = new IpAddressDefender(10);

            //act

            //assert
            defender.Scan(IPAddress.Parse("192.168.2.1")).Should().BeTrue();
            defender.Scan(IPAddress.Parse("192.168.2.3")).Should().BeTrue();
            defender.Scan(IPAddress.Parse("192.168.2.4")).Should().BeTrue();
        }


        [Test]
        public void DefederIsBlockingSameAddress()
        {
            //arrange
            var defender = new IpAddressDefender(10);

            //act

            //assert
            for (int i = 0; i < 9; i++)
            {
                defender.Scan(IPAddress.Parse("192.168.2.1")).Should().BeTrue();
            }
            defender.Scan(IPAddress.Parse("192.168.2.1")).Should().BeFalse();
        }


        [Test]
        public void DefederIsAllowingRequestsThatsExceedOneSecond()
        {
            //arrange
            var defender = new IpAddressDefender(10);

            //act

            //assert
            for (int i = 0; i < 10; i++)
            {
                defender.Scan(IPAddress.Parse("192.168.2.1")).Should().BeTrue();
                Thread.Sleep(110);
            }
        }


        [Test]
        public void InvalidDefenderInput()
        {
            //arrange
            
            //act

            //assert
            Assert.Throws<ArgumentException>(() => new IpAddressDefender(-10));
            Assert.Throws<ArgumentException>(() => new IpAddressDefender(int.MinValue));
            Assert.Throws<ArgumentException>(() => new IpAddressDefender(0));
            new IpAddressDefender(1);
            new IpAddressDefender(int.MaxValue);
        }

        [Test]
        public void InvalidIpAddressInput()
        {
            //arrange
            var defender = new IpAddressDefender(10);

            //act
            //assert
            Assert.Throws<ArgumentNullException>(() => defender.Scan(null));
            Assert.Throws<ArgumentNullException>(() => defender.Scan(new IPAddress(null)));
            Assert.Throws<ArgumentException>(() => defender.Scan(new IPAddress(new byte[] { })));
        }

        [Test]
        public void LongRunningProgramWithNumberOfIpAddressesAndRequests()
        {

            //IpAddressDefender can be pretty huge in long time running
            //and it could clear it's data. On the other hand it is not responsibility of IpAddressDefender. 
            //The aplication context can clear IpAddressDefender by 
            //creating new instance of IpAddressDefender or calling some Clear method of IpAddressDefender

        }
    }
}
