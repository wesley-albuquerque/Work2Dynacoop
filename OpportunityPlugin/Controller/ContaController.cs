using Microsoft.Xrm.Sdk;
using OpportunityPlugin.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpportunityPlugin.Controller
{
    public class ContaController
    {
        public Conta Conta { get; set; }

        public ContaController(IOrganizationService service)
        {
            Conta = new Conta(service);
        }

        public void AtualizaConta(EntityReference contaId, bool preOrPost)
        {
            Conta.AtualizaConta(contaId, preOrPost);
        }
    }
}
