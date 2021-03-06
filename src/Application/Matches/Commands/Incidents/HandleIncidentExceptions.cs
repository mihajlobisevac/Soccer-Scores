﻿using AutoMapper;
using SoccerScores.Application.Common.Exceptions;
using SoccerScores.Application.Common.Interfaces;
using SoccerScores.Domain.Entities;
using System.Threading.Tasks;

namespace SoccerScores.Application.Matches.Commands.Incidents
{
    public static class HandleIncidentExceptions
    {
        public static async Task<Incident> Handle(
            IncidentDto incidentDto, 
            IApplicationDbContext context,
            IMapper mapper)
        {
            var entity = mapper.Map<Incident>(incidentDto);

            entity.Match = await context.Matches.FindAsync(incidentDto.MatchId)
                    ?? throw new NotFoundException(nameof(Match), incidentDto.MatchId);

            if (incidentDto.PrimaryPlayerId is not null)
            {
                entity.PrimaryPlayer = await context.MatchPlayers.FindAsync(incidentDto.PrimaryPlayerId)
                    ?? throw new NotFoundException(nameof(Player), incidentDto.PrimaryPlayerId);
            }

            if (incidentDto.SecondaryPlayerId is not null)
            {
                entity.SecondaryPlayer = await context.MatchPlayers.FindAsync(incidentDto.SecondaryPlayerId)
                    ?? throw new NotFoundException(nameof(Player), incidentDto.SecondaryPlayerId);
            }

            return entity;
        }
    }
}
