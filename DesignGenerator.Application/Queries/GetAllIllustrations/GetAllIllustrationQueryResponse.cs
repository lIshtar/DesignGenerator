﻿using DesignGenerator.Application.Interfaces;
using DesignGenerator.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Application.Queries.GetAllIllustrations
{
    public class GetAllIllustrationQueryResponse : IResult
    {
        public List<Illustration> Illustrations { get; set; }
    }
}
