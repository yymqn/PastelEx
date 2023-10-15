using System.Collections;

namespace PastelExtended;

internal static class EnvironmentDetector
{
    private static readonly Func<string, string, bool>[] _environmentVariableDetectors = new Func<string, string, bool>[]
    {
        IsBitbucketEnvironmentVariableKey,
        IsTeamCityEnvironmentVariableKey,
        NoColor,
        IsGitHubAction,
        IsCI,
        IsJenkins,
        IsTeamCity
    };

    /// <summary>
    /// Returns true if the environment variables indicate that colors should be disabled.
    /// </summary>
    public static readonly bool ColorsDisabled = HasEnvironmentVariable((key, value) => _environmentVariableDetectors.Any(x => x(key, value)));

    private static bool IsBitbucketEnvironmentVariableKey(string key, string value)
    {
        return key.StartsWith("BITBUCKET_", StringComparison.OrdinalIgnoreCase);
    }

    private static bool IsTeamCityEnvironmentVariableKey(string key, string value)
    {
        return key.StartsWith("TEAMCITY_", StringComparison.OrdinalIgnoreCase);
    }

    // https://no-color.org/
    private static bool NoColor(string key, string value)
    {
        return key.Equals("NO_COLOR", StringComparison.OrdinalIgnoreCase);
    }

    // Set by GitHub Actions
    private static bool IsGitHubAction(string key, string value)
    {
        return key.StartsWith("GITHUB_ACTION", StringComparison.OrdinalIgnoreCase);
    }

    // Set by GitHub Actions and Travis CI
    private static bool IsCI(string key, string value)
    {
        return key.Equals("CI", StringComparison.OrdinalIgnoreCase)
            && (value.Equals("true", StringComparison.OrdinalIgnoreCase)
            || value.Equals("1", StringComparison.OrdinalIgnoreCase));
    }

    //isjenkins
    private static bool IsJenkins(string key, string value)
    {
        return key.StartsWith("JENKINS_URL", StringComparison.OrdinalIgnoreCase);
    }


    // is TEAMCITY_VERSION 
    private static bool IsTeamCity(string key, string value)
    {
        return key.StartsWith("TEAMCITY_VERSION", StringComparison.OrdinalIgnoreCase);
    }


    private static IEnumerable<(string key, string value)> GetBuildServerEnvironmentVariables()
    {
        return EnumerateEnvironmentVariables(EnvironmentVariableTarget.Process)
            .Where(x => IsBitbucketEnvironmentVariableKey(x.Key, x.Value) || IsTeamCityEnvironmentVariableKey(x.Key, x.Value));
    }

    private static bool HasEnvironmentVariable(Func<string, string, bool> predicate)
    {
        var processKeys = EnumerateEnvironmentVariables(EnvironmentVariableTarget.Process);
        var userKeys = EnumerateEnvironmentVariables(EnvironmentVariableTarget.User);
        var machineKeys = EnumerateEnvironmentVariables(EnvironmentVariableTarget.Machine);

        return processKeys
            .Concat(userKeys)
            .Concat(machineKeys)
            .Any(x => predicate(x.Key, x.Value));
    }

    private static IEnumerable<(string Key, string Value)> EnumerateEnvironmentVariables(EnvironmentVariableTarget target)
    {
        foreach (var entry in Environment.GetEnvironmentVariables(target).OfType<DictionaryEntry>())
        {
            yield return (entry.Key.ToString()!, entry.Value?.ToString() ?? string.Empty);
        }
    }
}