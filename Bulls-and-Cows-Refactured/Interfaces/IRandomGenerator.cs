namespace BullsAndCowsGame.Interfaces
{
    public interface IRandomGenerator
    {
        string Generate(int numberLenght);

        string GenerateNumberByDigits(int numberLenght, int digitMin, int digitMax);
    }
}
