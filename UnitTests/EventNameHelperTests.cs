using SelTest;
using Xunit;

namespace UnitTests
{
    public class EventNameHelperTests
    {
        [Fact]
        public void DiceCoefficient()
        {
            Assert.Equal(1, EventNameHelper.DiceCoefficient("Брайтон & Хоув Альбион", "Брайтон энд Хоув Альбион"));
            //Assert.Equal(1, EventNameHelper.DiceCoefficient("Прима", "Примадонна")); //0.615384615384615
            //Assert.Equal(1, EventNameHelper.DiceCoefficient("Прима", "Прима"));
            //Assert.Equal(1, EventNameHelper.DiceCoefficient("Прима", "Прима"));
        }

        [Fact]
        public void Levenshtein()
        {
            var result = EventNameHelper.Levenshtein("Брайтон & Хоув Альбион", "Брайтон энд Хоув Альбион");
            Assert.Equal(0, result);
        }
    }
}
