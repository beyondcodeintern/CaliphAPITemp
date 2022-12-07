namespace Caliph.Library.Models
{
    public enum MasterEnum : long
    {
        SystemStatus = 1,
        LengthOfTimeKnown = 2,
        AnnualIncome = 3,
        Occupation = 4,
        MaritalStatus = 5,
        HowWellKnown = 6,
        HowOftenSeenInAYear = 7,
        CouldApproachOnBusiness = 8,
        AbilityToProvideReferrals = 9,
        Source = 10,
        ClientContact = 11,
        AgeRange = 12,
        Gender = 13,
        DealTitle = 14,
        ClientRelationship = 15,
        Activity = 16,
        AnnouncementType = 17,
        ActivityReviewType = 18,
        MonthlyBudgetType = 19,
        EducationBg = 20,
        AgentSimulatorType = 21,
        EventType = 22,
        EventHost = 23,
        EventChannel = 24,
        AttendantType = 25,
        Attendance = 26,
        QuizScore = 27,
        UserEventStatusId = 28,
        PaymentChannel = 29,
        PaymentStatus = 30
    }

    public enum MasterDataEnum : long
    {
        Status_Active = 1,
        Status_Inactive = 2,
        Status_Confirm = 3,
        Status_Potential = 4,
        Status_Closed = 5,
        Status_KIV = 6,
        Status_Lost = 7,
        Status_Missed = 8,
        Status_Done = 9,
        Status_Leads = 10,
        Status_ConvertedToClient = 11,
        Status_Archive = 12,
        Status_ConvertedToAgent = 13,
        UserEventStatusId_Pending = 167,
        UserEventStatusId_Going = 168
    }

    public enum Role : long
    {
        Role_SuperAdmin = 1,
        Role_Agent = 2,
        Role_Leader = 3
    }

    public enum AnnouncementType : long
    {
        all = 111,
        specified_user = 112
    }
}
