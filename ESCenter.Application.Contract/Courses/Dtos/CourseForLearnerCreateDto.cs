﻿namespace ESCenter.Application.Contract.Courses.Dtos;

public class  CourseForLearnerCreateDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string LearningMode { get; set; } = "Offline";
    public float Fee { get; set; }
    public string GenderRequirement { get; set; } = "None";
    public string AcademicLevelRequirement { get; set; } = "Optional";
    public string LearnerGender { get; set; } = "Male";
    public string LearnerName { get; set; } = "Male";
    public int NumberOfLearner { get; set; } = 1;
    public string ContactNumber { get; set; } = string.Empty;
    public Guid LearnerId { get; set; }
    public int MinutePerSession { get; set; } = 90;
    public int SessionPerWeek { get; set; } = 2;
    public string Address { get; set; } = string.Empty;
    public int SubjectId { get; set; } = 0;
    public string SubjectName { get; set; } = string.Empty;
}

