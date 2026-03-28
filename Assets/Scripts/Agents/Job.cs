[System.Serializable]
public class Job
{
    public string jobTitle;
    public string careerTrack;
    public int careerLevel;
    public JobContractType contractType;
    public float salaryPerHour;
    public string workplaceId;

    // Work schedule — day indices 0=Mon to 6=Sun, hours in 24h float
    public int[] workDays;
    public float startHour;
    public float endHour;
}
