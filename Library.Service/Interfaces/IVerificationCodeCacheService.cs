using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service.Interfaces
{
    public interface IVerificationCodeCacheService
    {
        void SetVerificationCode(string userId, int verificationCode);
        int? GetVerificationCode(string userId);


    }
}
