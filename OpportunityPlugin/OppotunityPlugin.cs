using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using OpportunityPlugin.Controller;
using OpportunityPlugin.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpportunityPlugin
{
    public class OpportunityPlugin : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            IOrganizationServiceFactory serviceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);
            ContaController contaController = new ContaController(service);
            Entity opportunityPreImage = context.PreEntityImages["PreImage"] as Entity;
            EntityReference contaPre = opportunityPreImage.Contains("parentaccountid") ? (EntityReference)opportunityPreImage.Attributes["parentaccountid"] : null;

            if (context.MessageName.ToLower() == "update")
            {
                Entity opportnityPostImage = context.PostEntityImages["PostImage"] as Entity;
                EntityReference contaPost = opportnityPostImage.Contains("parentaccountid") ? (EntityReference)opportnityPostImage.Attributes["parentaccountid"] : null;

                if (contaPre != null)
                    contaController.AtualizaConta(contaPre, false);
                if (contaPost != null)
                    contaController.AtualizaConta(contaPost, true);

            }
            else if (context.MessageName.ToLower() == "delete")
            {
                contaController.AtualizaConta(contaPre, false);
            }
        }
    }
}
