﻿using DesignGenerator.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Application.Commands
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceProvider _service;

        public CommandDispatcher(IServiceProvider service)
        {
            _service = service;
        }

        public void Send<T>(T command) where T : ICommand
        {
            var handler = _service.GetService(typeof(ICommandHandler<T>));
            if (handler != null)
                ((ICommandHandler<T>)handler).Handle(command);
            else
                throw new Exception($"Command doesn't have any handler {command.GetType().Name}");
        }
    }
}
