﻿using System;
using System.Collections;
using System.Collections.Generic;
using StrawberryShake;

namespace Client
{
    [System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public class SignIn
        : ISignIn
    {
        public SignIn(
            ILoginPayload login)
        {
            Login = login;
        }

        public ILoginPayload Login { get; }
    }
}
