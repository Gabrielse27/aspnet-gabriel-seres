using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFitness.Application.Members.Inputs
{
    public class JoinMembershipInput
    {
        // ID för den inloggade användaren (hämtas oftast från Claims)
        public string UserId { get; set; } = null!;

        // ID för det medlemskap användaren valt (1 eller 2 i din databas)
        public int MembershipId { get; set; }
    }
}
