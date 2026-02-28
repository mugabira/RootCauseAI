using RootCauseAI.Models;

namespace RootCauseAI.Data
{
    public static class TicketStore
    {
        private static readonly List<Ticket> Tickets = new();

        public static void Add(Ticket ticket)
        {
            ticket.Id = Tickets.Count + 1;
            Tickets.Add(ticket);
        }

        public static List<Ticket> GetByTenant(string tenantId)
        {
            return Tickets.Where(t => t.TenantId == tenantId).ToList();
        }
    }
}