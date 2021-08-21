using System.Collections;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Server.Api.Entities;

namespace Server.Api.Services
{
    public interface ISurveyService
    {
        Task<Survey> Create(string name, ICollection<int> stateIds, ICollection<int> teamIds, DateTimeOffset start, DateTimeOffset end);
        Task<Survey> Get(int id);
        Task<IEnumerable<Survey>> GetAll();
    }
}