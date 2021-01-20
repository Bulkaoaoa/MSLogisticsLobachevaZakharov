using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppMsLogisctics.Entites;

namespace WebAppMsLogisctics.Models
{
    public class ResponseRule
    {
        public ResponseRule(Rule rule)
        {
            Id = rule.Id;
            Name = rule.Name;
        }
        public int Id { get; set; }
        public string Name { get; set; }
    }
}