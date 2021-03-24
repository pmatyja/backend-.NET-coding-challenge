namespace FizzBuzz.Rules
{
    public interface IRule
    {
        int MaxOutpuSize { get; }
        
        bool CanHandle(int value);
        string Handle(int value);
    }
}