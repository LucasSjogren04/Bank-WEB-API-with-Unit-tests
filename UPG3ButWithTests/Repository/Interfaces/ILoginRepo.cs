using UPG3ButWithTests.Models;

namespace UPG3ButWithTests.Repository.Interfaces
{
    public interface ILoginRepo
    {
        Login Login(string loginKey);
    }
}
