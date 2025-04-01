using DesignGenerator.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Application.Interfaces
{
    public interface IIllustartionRepository
    {
        public Task AddNewIllustration(Illustartion illustartion);
        public Task UpdateIllustration(string illustrationPath, string prompt);
    }
}
