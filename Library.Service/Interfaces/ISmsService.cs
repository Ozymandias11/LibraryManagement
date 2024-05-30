﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service.Interfaces
{
    public interface ISmsService
    {
        Task<bool> SendSms(string phoneNumber, int verificationCode);
    }
}
