﻿using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace SilentNotary.Cqrs.Nats.Adapters
{
    [DataContract]
    public class QueryNatsAdapter
    {
        [DataMember] public string Criterion { get; set; }
        [DataMember] public string CriterionType { get; set; }
        [DataMember] public string QueryResultType { get; set; }
        [DataMember] public string QueryResult { get; set; }

        public QueryNatsAdapter(object criterion, Type queryResult)
        {
            Criterion = JsonConvert.SerializeObject(criterion);
            CriterionType = criterion.GetType().ToString();
            QueryResultType = queryResult.ToString();
        }

        public QueryNatsAdapter()
        {
        }
    }
}