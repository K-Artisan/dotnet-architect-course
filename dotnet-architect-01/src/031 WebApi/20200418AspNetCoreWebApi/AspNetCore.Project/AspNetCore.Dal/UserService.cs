using System;

namespace AspNetCore.Dal
{
    public class UserService
    {

        public void GetString()
        {
            throw new Exception("Service层发生异常了");
        }
    }
}
