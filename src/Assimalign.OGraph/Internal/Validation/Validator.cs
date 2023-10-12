using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal.Validation
{
    public class Test
    {
        public Test()
        {
            
        }
    }

    internal abstract class Validator
    {
        private readonly IList<Tuple<string, string, Action<ValidatorContext>>> rules;

        public Validator()
        {
            rules = new List<Tuple<string, string, Action<ValidatorContext>>>();

            Configure(new ValidatorBuilder(rules));
        }

        protected abstract void Configure(ValidatorBuilder builder);


        public ValidatorError Validate(ValidatorContext context)
        {
            return default;
        }


    }

    internal class ValidatorContext
    {
        public IOGraph Graph { get; init; }
    }
    internal class ValidatorError
    {

    }

    internal class ValidatorBuilder
    {
        private readonly IList<Tuple<string, string, Action<ValidatorContext>>> rules;

        public ValidatorBuilder(IList<Tuple<string, string, Action<ValidatorContext>>> rules)
        {
            this.rules = rules;
        }
        public ValidatorBuilder Rule(string code, string title, Action<ValidatorContext> rule)
        {
            rules.Add(new (code, title, rule));
            return this;
        }
    }

    internal class TestValidator : Validator
    {
        protected override void Configure(ValidatorBuilder builder)
        {
            builder.Rule("", "", context =>
            {

            });
        }
    }
}
