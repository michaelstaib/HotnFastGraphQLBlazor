﻿using System;
using System.Collections;
using System.Collections.Generic;
using StrawberryShake;

namespace Client
{
    [System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public class RecipientById
        : IRecipientById
    {
        public RecipientById(
            IRecipient personById)
        {
            PersonById = personById;
        }

        public IRecipient PersonById { get; }
    }
}
