namespace BancoOnBoarding.Infrastructure.Exceptions
{
    public class BancoOnBoardingException : Exception
    {
        public BancoOnBoardingException(string code) : base(code)
        {

        }
    }
}
