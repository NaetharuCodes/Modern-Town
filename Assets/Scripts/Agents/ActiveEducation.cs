[System.Serializable]
public class ActiveEducation
{
    public string institutionName;
    public EducationType type;
    public int yearOfStudy;
    public float currentGpa;

    // Schedule — mirrors Job for consistency
    public int[] attendanceDays;
    public float startHour;
    public float endHour;
}
