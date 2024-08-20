namespace test.Services
{
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using CsvHelper;
    using CsvHelper.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using test.ValueObjects;
    using test.Models;

    public class CsvProcessingService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly GlobalStateSingleton _globalStateSingleton;

        public CsvProcessingService(IServiceScopeFactory scopeFactory, GlobalStateSingleton globalStateSingleton)
        {
            _scopeFactory = scopeFactory;
            _globalStateSingleton = globalStateSingleton;
        }

        public void ProcessCsv(string csvFilePath, int batchSize)
        {
            using var reader = new StreamReader(csvFilePath);
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HeaderValidated = null,
                MissingFieldFound = null
            });

            int offset = _globalStateSingleton.LastCsvOffset;
            var records = csv.GetRecords<CsvState>()
                             .Skip(offset)
                             .Take(batchSize)
                             .ToList();

            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                using var transaction = dbContext.Database.BeginTransaction();
                try
                {
                    foreach (var record in records)
                    {
                        var state = GetOrCreateState(dbContext, record);
                        var municipality = GetOrCreateMunicipality(dbContext, record, state);
                        var parish = GetOrCreateParish(dbContext, record, municipality);
                        var center = GetOrCreateCenter(dbContext, record, parish);
                        CreatePollingStation(dbContext, record, center);
                    }

                    dbContext.SaveChanges();
                    transaction.Commit();

                    // Actualiza el offset global al final del procesamiento
                    _globalStateSingleton.LastCsvOffset += batchSize;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        private static State GetOrCreateState(ApplicationDbContext dbContext, CsvState record)
        {
            var state = dbContext.States.FirstOrDefault(e => e.Name == record.EDO);
            if (state == null)
            {
                state = new State
                {
                    Cod_State = record.COD_EDO,
                    Name = record.EDO
                };
                dbContext.States.Add(state);
            }
            return state;
        }

        private static Municipality GetOrCreateMunicipality(ApplicationDbContext dbContext, CsvState record, State state)
        {
            var municipality = dbContext.Municipalities.FirstOrDefault(m => m.Name == record.MUN);
            if (municipality == null)
            {
                municipality = new Municipality
                {
                    Cod_Municipality = record.COD_MUN,
                    Name = record.MUN,
                    State = state
                };
                dbContext.Municipalities.Add(municipality);
            }
            return municipality;
        }

        private static Parish GetOrCreateParish(ApplicationDbContext dbContext, CsvState record, Municipality municipality)
        {
            var parish = dbContext.Parishes.FirstOrDefault(p => p.Name == record.PAR);
            if (parish == null)
            {
                parish = new Parish
                {
                    Cod_Parish = record.COD_PAR,
                    Name = record.PAR,
                    Municipality = municipality
                };
                dbContext.Parishes.Add(parish);
            }
            return parish;
        }

        private static Center GetOrCreateCenter(ApplicationDbContext dbContext, CsvState record, Parish parish)
        {
            var center = dbContext.Centers.FirstOrDefault(c => c.Cod_Center == record.CENTRO);
            if (center == null)
            {
                center = new Center
                {
                    Cod_Center = record.CENTRO,
                    Parish = parish
                };
                dbContext.Centers.Add(center);
            }
            return center;
        }

        private static void CreatePollingStation(ApplicationDbContext dbContext, CsvState record, Center center)
        {
            var pollingStation = new PollingStation
            {
                Cod_PollingStation = record.MESA,
                ValidVotes = record.VOTOS_VALIDOS,
                NullVotes = record.VOTOS_NULOS,
                EG = record.EG,
                NM = record.NM,
                LB = record.LM,
                JABE = record.JABE,
                JOBR = record.JOBR,
                AE = record.AE,
                CF = record.CF,
                DC = record.DC,
                EM = record.EM,
                BERA = record.BERA,
                Record = record.URL,
                Center = center
            };

            dbContext.PollingStations.Add(pollingStation);
        }
    }
}