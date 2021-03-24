namespace FizzBuzz.Rules
{
    public interface IRule
    {
        int MaxOutputSize { get; }
        
        bool CanHandle(int value);
        string Handle(int value);
    }
}