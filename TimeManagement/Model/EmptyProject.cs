using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace TimeManagement.Model
{
    class EmptyProject : Project
    {
        public override string getName()
        {
            return "All Projects";
        }

        public override void save()
        {

        }

        public override void delete()
        {
            
        }

        public override ObservableCollection<Ticket> getTickets()
        {
            return this.TicketResource.getAllTickets();
        }
    }
}
