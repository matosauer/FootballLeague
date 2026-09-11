using EntityFrameworkCore.ConsoleApp;

public interface IConsoleApp
{
    Task RunAsync();
}

public class ConsoleApp : IConsoleApp
{
    private readonly TeamExplorer _teamExplorer;

    public ConsoleApp(TeamExplorer teamExplorer)
    {
        _teamExplorer = teamExplorer;
    }



    public async Task RunAsync()
    {
        if (Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") == "Development")
        {
            await _teamExplorer.SeedAsync();
        }

        Console.WriteLine("Go: ------------------");

        //await _teamExplorer.PrintAllTeamsAsync();
        //await _teamExplorer.PrintTeamsByLeagueAsync("Premier League");
        //await _teamExplorer.PrintNPlusOne();
        //await _teamExplorer.QueryFiltersAsync();
        //await _teamExplorer.AdditionalExecutionAsync();
        //await _teamExplorer.AdditionalExecutionMethodsAsync();
        //await _teamExplorer.AddPremierTeamAsync();
        //await _teamExplorer.UpdatePremierTeamAsync();
        //await _teamExplorer.UpdatePremierLeagueTeamsAsync();
        //await _teamExplorer.UpdateTeamAsync();
        //await _teamExplorer.DeleteTeamAsync();

        //await _teamExplorer.AddNewTeamsWithLeagueAsync();
        //await _teamExplorer.AddNewTeamWithLeagueId();
        //await _teamExplorer.AddNewLeagueWithTeams();
        //await _teamExplorer.AddNewMatchesAsync();
        //await _teamExplorer.AddNewCoachAsync();

        //await _teamExplorer.QueryRelatedRecordsAsync();
        //await _teamExplorer.FilteringWithRelatedData();

        //await _teamExplorer.QueryViewAsync();
        //await _teamExplorer.RawSQLQueryAsync();
        //await _teamExplorer.ExecuteNonQueryCommandAsync();
        //await _teamExplorer.ExecStoredProcedureAsync();
        await _teamExplorer.ExecStoredProceInMemoryAsync();


    }
}