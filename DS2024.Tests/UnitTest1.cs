namespace DS2024.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Divide_DivisibleIntegers_WholeNumber()
        {
            // Arrange
            int dividend = 10;
            int divisor = 5;
            decimal expectedQuotient = 2;

            // Act
            decimal actualQuotient = Division.Divide(dividend,divisor);

            // Assert
            Assert.Equal(expectedQuotient, actualQuotient);
        }
    }
}