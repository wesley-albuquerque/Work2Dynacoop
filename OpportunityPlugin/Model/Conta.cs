using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services.Description;

namespace OpportunityPlugin.Model
{
    public class Conta
    {
        public string LogicalName = "account";
        Entity Account { get; set; }
        public IOrganizationService Service { get; set; }

        public EntityReference ContaId { get; set; }
        public Conta(IOrganizationService service) 
        {
            Service = service;
        }
        

        public void AtualizaConta(EntityReference contaId, bool preOrPost)
        {
            ColumnSet coluna = new ColumnSet("wes_valortotalopp");

            Account = Service.Retrieve("account", contaId.Id, coluna);
            int valTotalOpp = Account.Contains("wes_valortotalopp") ? (int)Account.Attributes["wes_valortotalopp"] : 0;

            if (preOrPost)
                valTotalOpp += 1;
            else
            {
                valTotalOpp -= 1;
                if (valTotalOpp < 0)
                    valTotalOpp = 0;

            }


            Account.Attributes["wes_valortotalopp"] = valTotalOpp;

            Service.Update(Account);

        }
    }
}
