// using FluentAssertions;
// using FluentAssertions.Execution;
// using Xunit;
//
// namespace HMISimulator.API.Oven.Tests;
//
// public class OvenSimulatorTests
// {
//     [Fact]
//     public void Oven_Should_Start_At_Ambient_Temperature()
//     {
//         // Arrange
//         var oven = new OvenSimulator();
//         
//         // Act
//         var initialTemp = oven.CurrentTemperature;
//         
//         // Assert
//         initialTemp.Should().Be(25.0);  // Default ambient temperature
//     }
//     
//     [Fact]
//     public void Oven_Should_Heat_Up_When_HeatingElement_IsOn()
//     {
//         // Arrange
//         var oven = new OvenSimulator();
//         oven.StartHeating();
//         
//         // Act
//         oven.Update(10);  // Simulate 10 seconds of heating
//         
//         // Assert
//         oven.CurrentTemperature.Should().BeGreaterThan(25.0);  // It should heat up
//     }
//     
//     [Fact]
//     public void Oven_Should_Cool_Down_When_HeatingElement_IsOff()
//     {
//         // Arrange
//         var oven = new OvenSimulator();
//         oven.StartHeating();
//
//         // Simulate the oven heating
//         oven.Update(1);
//
//         // Now simulate cooldown
//         oven.StopHeating();
//
//         var tempBeforeCooldown = oven.CurrentTemperature;
//
//         // Act
//         oven.Update(1);  // Simulate 10 seconds of cooldown
//
//         // Assert
//         using (new AssertionScope())
//         {
//             oven.CurrentTemperature.Should().BeLessThan(tempBeforeCooldown);  // It should cool down
//             oven.CurrentTemperature.Should().BeGreaterThan(25.0);  // Should not go below ambient yet
//         }
//     }
//
//     [Fact]
//     public void Oven_Should_Not_Cool_Below_Ambient_Temperature()
//     {
//         // Arrange
//         var oven = new OvenSimulator();
//         
//         // Simulate heating and cooling beyond ambient temperature
//         oven.StopHeating();  // Directly start with cooldown
//
//         // Act
//         for (int i = 0; i < 1000; i++)  // Simulate a long time
//         {
//             oven.Update(1);
//         }
//
//         // Assert
//         oven.CurrentTemperature.Should().Be(25.0);  // Should not go below ambient temperature
//     }
// }