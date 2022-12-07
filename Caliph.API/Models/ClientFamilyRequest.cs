using Caliph.Library.Models;
using System;

namespace Caliph.API.Models
{
    public class ClientFamilyRequest : BaseEnt
    {
        public long ClientFamilyId { get; set; }
        public long ClientId { get; set; }
        public string Name { get; set; }
        public long StatusId { get; set; }
        public string StatusDesc { get; set; }
        public long RelationId { get; set; }
        public DateTime? DOB { get; set; }
        public long GenderId { get; set; }
        public string HobbyDesc { get; set; }
        public string HPDesc { get; set; }
        public string SchoolDesc { get; set; }
        public string Remarks { get; set; }
    }

    public class ClientFamilyFilterRequest
    {
        public long? StatusId { get; set; }
        public long? ClientId { get; set; }
        public long PageSize { get; set; }
        public long PageNumber { get; set; }
    }
}