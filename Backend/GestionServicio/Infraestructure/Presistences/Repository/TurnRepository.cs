using Domain.Entities;
using Infraestructure.Commons.Reponse;
using Infraestructure.Commons.Request;
using Infraestructure.Presistences.Contexts;
using Infraestructure.Presistences.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using Utility.Static;

namespace Infraestructure.Presistences.Repository
{
    internal class TurnRepository: GenericRepository<Turn>, ITurnRepository
    {
        private readonly GestionServicesContext _context;
        public TurnRepository(GestionServicesContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Turn> GetTurnByIdAsync(int id)
        {
            var response = await GetEntityQuery(turn => turn.Turnid == id)
                .AsNoTracking()
                .FirstOrDefaultAsync();
            return response!;
        }

        public async Task<DataResponse<TurnGestion>> GetListTurnsAsync(FiltersRequest request)
        {
            var response = new DataResponse<TurnGestion>();
            var turns = _context.Turns.AsNoTracking()
                .Include(turn => turn.CashCash)
                .Where(turn => turn.Date > DateTime.Now);
            var attention = _context.Attentions
                .Include(att => att.ClientClient)
                .AsNoTracking();
            var attentionType = _context.Attentiontypes
                .AsNoTracking();
            var attentionStatus = _context.Attentionstatuses
                .AsNoTracking();
            var users = _context.Users.AsNoTracking();

            var turnsGestion = (
                from turn in turns
                join user in users
                on turn.Usergestorid equals user.Userid
                join att in attention
                on turn.Turnid equals att.TurnTurnid into attGroup
                from att in attGroup.DefaultIfEmpty()
                join type in attentionType
                on att.AttentiontypeAttentiontypeid equals type.Attentiontypeid
                join state in attentionStatus
                on att.AttentionstatusStatusid equals state.Statusid
                where att.AttentionstatusStatusid != (int)StatuesBasic.Finalizado
                select new TurnGestion
                {
                    Turnid = turn.Turnid,
                    Description = turn.Description,
                    Cash = turn.CashCash.Cashdescription,
                    Date = turn.Date,
                    UserGestion = user.Username,
                    CLient = att != null ? $"{att.ClientClient.Name} {att.ClientClient.Lastname}" : null,
                    Identification = att != null ? att.ClientClient.Identification : null,
                    StatusAttention = state.Description,
                    TypeAttention = type.Description
                }
                ).AsNoTracking();

            request.Sort = string.IsNullOrEmpty(request.Sort) ? "Turnid" : request.Sort;
            if (request.NumFilter is not null && !string.IsNullOrEmpty(request.TextFilter))
            {
                switch (request.NumFilter)
                {
                    case 1:
                        turnsGestion = turnsGestion.Where(turn => turn.Description!.Contains(request.TextFilter));
                        break;
                }
            }
            if (!string.IsNullOrEmpty(request.StartDate) && !string.IsNullOrEmpty(request.EndDate))
            {
                var startDate = Convert.ToDateTime(request.StartDate);
                var endDate = Convert.ToDateTime(request.EndDate);
                turnsGestion = turnsGestion.Where(turn => turn.Date >= startDate && turn.Date <= endDate);
            }
            response.TotalRecords = turnsGestion.Count();
            response.Items = await Ordering(request, turnsGestion, true).ToListAsync();
            return response;
        }

        public async Task<TurnGestion> GetTrunById(int turnId)
        {
            var turns =_context.Turns.Where(tr => tr.Turnid.Equals(turnId)).Include(tr => tr.CashCash);
            var users = _context.Users;
            var turnsGestion = await (
                from turn in turns
                join user in users
                on turn.Usergestorid equals user.Userid
                select new TurnGestion
                {
                    Turnid = turn.Turnid,
                    Description = turn.Description,
                    Cash = turn.CashCash.Cashdescription,
                    Date = turn.Date,
                    UserGestion = user.Username
                }
                ).FirstOrDefaultAsync();
            return turnsGestion!;
        }

        public async Task<bool> CreateTurnAttention(TurnAttention request)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Crear y guardar la instancia de Turn
                var turn = new Turn
                {
                    Description = request.Description!,
                    Date = request.Date,
                    CashCashid = request.CashId,
                    Usergestorid = request.UsarAuthId,
                    Datecreation = DateTime.Now
                };

                _context.Turns.Add(turn);
                await _context.SaveChangesAsync();

                // Crear y guardar la instancia de Attention
                var attention = new Attention
                {
                    TurnTurnid = turn.Turnid,
                    ClientClientid = request.ClientId,
                    AttentiontypeAttentiontypeid = request.AttentionTypeId,
                    AttentionstatusStatusid = request.AttentionStateId,
                    Datecreation = DateTime.Now
                };

                _context.Attentions.Add(attention);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
                return true;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                return false;
            }
        }
    }
}
