﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Infrastructure.AICommunicators
{
    public interface ITextAICommunicator
    {
        public Task<string> GetTextAnswerAsync(string query);
    }
}
