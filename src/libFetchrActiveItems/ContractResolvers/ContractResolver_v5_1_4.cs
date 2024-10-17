using Newtonsoft.Json.Serialization;
using System.Collections.Generic;

namespace libFetchrActiveItems.ContractResolvers
{
    internal class ContractResolver_v5_1_4 : DefaultContractResolver
    {
        private Dictionary<string, string> PropertyMappings { get; set; }

        public ContractResolver_v5_1_4()
        {
            PropertyMappings = new Dictionary<string, string>
            {
                {"ActiveCategories", "active_categories"},
                {"CommandArgument", "command_argument"},
                {"TotalItemWeight", "total_item_weight"},
                {"WeightDenom", "weight_denom"},
                {"WeightNom", "weight_nom"}
            };
        }

        protected override string ResolvePropertyName(string propertyName)
        {
            var resolved = PropertyMappings.TryGetValue(propertyName, out string resolvedName);
            return (resolved) ? resolvedName : base.ResolvePropertyName(propertyName);
        }
    }
}