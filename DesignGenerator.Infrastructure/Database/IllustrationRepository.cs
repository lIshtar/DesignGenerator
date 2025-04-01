using DesignGenerator.Application.Interfaces;
using DesignGenerator.Domain;
using DesignGenerator.Infrastructure.DBEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Infrastructure.Database
{
    class IllustrationRepository : IIllustartionRepository
    {
        private ApplicationDbContext _context;
        public IllustrationRepository(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }
        public async Task UpdateIllustration(string illustrationPath, string prompt)
        {
            throw new NotImplementedException();
        }

        public async Task AddNewIllustration(Illustartion illustartion)
        {
            throw new NotImplementedException();
        }
    }
}
