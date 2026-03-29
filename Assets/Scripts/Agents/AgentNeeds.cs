[System.Serializable]
public class AgentNeeds
{
    // All needs run 0-100. Higher is better. Zero triggers critical outcomes.
    public float hunger      = 100f;
    public float energy      = 100f;
    public float bladder     = 100f;
    public float social      = 100f;
    public float fun         = 100f;
    public float hygiene     = 100f;
    public float comfort     = 100f;
    public float environment = 100f;

    // Decay rates per in-game hour — will be driven by game time system later
    public const float HungerDecay     = 4f;
    public const float EnergyDecay     = 3f;
    public const float BladderDecay    = 6f;
    public const float SocialDecay     = 2f;
    public const float FunDecay        = 2f;
    public const float HygieneDecay    = 1.5f;
    public const float ComfortDecay    = 1f;
    public const float EnvironmentDecay = 0.5f;

    public bool IsCritical(float need) => need <= 0f;
    public bool IsLow(float need) => need < 25f;

    // Returns the name of the most urgently needed thing
    public string MostUrgentNeed()
    {
        var worst = float.MaxValue;
        var name = "none";

        Check("hunger",      hunger);
        Check("energy",      energy);
        Check("bladder",     bladder);
        Check("social",      social);
        Check("fun",         fun);
        Check("hygiene",     hygiene);
        Check("comfort",     comfort);
        Check("environment", environment);

        return name;

        void Check(string n, float v) { if (v < worst) { worst = v; name = n; } }
    }
}
