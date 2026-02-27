using RootCauseAI.Models;

namespace RootCauseAI.Data
{
    public static class TicketStore
    {
        private static readonly List<Ticket> Tickets = new();

        public static void Add(Ticket ticket) => Tickets.Add(ticket);

        public static List<Ticket> GetAll() => Tickets;
    }
}
