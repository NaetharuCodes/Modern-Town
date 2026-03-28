using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    [Header("Identity")]
    public string firstName;
    public string lastName;
    public AgentType agentType;
    public int age;

    [Header("Position")]
    public Vector3Int gridPosition;

    [Header("Needs")]
    public AgentNeeds needs = new AgentNeeds();

    [Header("Active Commitments")]
    public Job currentJob;
    public ActiveEducation activeEducation;
    public List<ClubMembership> clubs = new();
    public List<AdHocActivity> upcomingActivities = new();

    [Header("Inventory")]
    public List<string> inventory = new(); // stub — item IDs for now

    [Header("Life History")]
    public List<EducationRecord> educationHistory = new();
    public List<CareerRecord> careerHistory = new();
    public List<Achievement> achievements = new();

    public string FullName => $"{firstName} {lastName}";

    // Helpers for querying history — useful for events, relationships, job offers etc.
    public bool HasGraduated(EducationType type) =>
        educationHistory.Exists(e => e.type == type && e.graduated);

    public bool HasWorkedAt(string companyName) =>
        careerHistory.Exists(c => c.companyName == companyName) ||
        currentJob?.workplaceId == companyName;

    public int HighestCareerLevel(string track)
    {
        int best = 0;
        foreach (var record in careerHistory)
            if (record.careerTrack == track && record.highestLevelReached > best)
                best = record.highestLevelReached;
        if (currentJob?.careerTrack == track && currentJob.careerLevel > best)
            best = currentJob.careerLevel;
        return best;
    }
}
