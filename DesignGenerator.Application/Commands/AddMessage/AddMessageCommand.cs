using DesignGenerator.Application.Interfaces;

namespace DesignGenerator.Application.Commands.AddMessage
{
    public class AddMessageCommand : ICommand
    {
        public string Text { get; set; }
        public string Sender { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
