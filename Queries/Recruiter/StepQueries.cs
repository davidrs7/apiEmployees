
namespace Api.Queries
{
    public class StepQueries
    {
        public string Steps { get; } = "SELECT Id, Available, Name, Description FROM Step";
        public string StepById { get; } = "SELECT Id, Available, Name, Description FROM Step WHERE Id = @Id";
        public string Add { get; } = "INSERT INTO Step (Available, Name, Description) VALUES (@Available, @Name, @Description); SELECT LAST_INSERT_ID();;";
        public string Edit { get; } = "UPDATE Step SET Available = @Available, Name = @Name, Description = @Description WHERE Id = @Id";
        public string Fields { get; } = "SELECT Id, Available, Name, FieldType, IsRequired, Config FROM Step_Field";
        public string FieldById { get; } = "SELECT Id, Available, Name, FieldType, IsRequired, Config FROM Step_Field WHERE Id = @Id";
        public string FieldsByStep { get; } = "SELECT SF.Id, SFR.StepId, SF.Available, SF.Name, SF.FieldType, SF.IsRequired, SF.Config, SFR.Active, SFR.Weight FROM Step_Field SF INNER JOIN Step_Field_Rel SFR ON SF.Id = SFR.StepFieldId WHERE SFR.StepId = @StepId ORDER BY SFR.Weight";
        public string FieldsByStepRel { get; } = "SELECT SF.Id, SFR.StepId, PVR.VacantId, PVR.PostulateId, SF.Available, SF.Name, SF.FieldType, SF.IsRequired, SF.Config, SFR.Active, SFR.Weight, PVSFR.FieldValue FROM Step_Field SF INNER JOIN Step_Field_Rel SFR ON SF.Id = SFR.StepFieldId LEFT JOIN Vacant_Step_Rel VSR ON (VSR.StepId = SFR.StepId AND VSR.VacantId = @VacantId) LEFT JOIN Postulate_Vacant_Rel PVR ON (VSR.VacantId = PVR.VacantId AND PVR.PostulateId = @PostulateId) LEFT JOIN Postulate_Vacant_Step_Field_Rel PVSFR ON (SFR.Id = PVSFR.StepFieldRelId AND PVR.Id = PVSFR.PostulateVacantRelId) WHERE SFR.StepId = @StepId ORDER BY SFR.Weight";
        public string FieldAdd { get; } = "INSERT INTO Step_Field (Available, Name, FieldType, IsRequired, Config) VALUES (@Available, @Name, @FieldType, @IsRequired, @Config)";
        public string FieldEdit { get; } = "UPDATE Step_Field SET Available = @Available, Name = @Name, FieldType = @FieldType, IsRequired = @IsRequired, Config = @Config WHERE Id = @Id";
        public string FieldRelAdd { get; } = "INSERT INTO Step_Field_Rel (StepId, StepFieldId, Active, Weight) VALUES (@StepId, @StepFieldId, @Active, @Weight)";
        public string FieldRelEdit { get; } = "UPDATE Step_Field_Rel SET Active = @Active, Weight = @Weight WHERE StepId = @StepId AND StepFieldId = @StepFieldId";
        public string FieldRelValueAdd { get; } = "INSERT INTO Postulate_Vacant_Step_Field_Rel (PostulateVacantRelId, StepFieldRelId, FieldValue) VALUES (@PostulateVacantRelId, @StepFieldRelId, @FieldValue)";
        public string FieldRelValueEdit { get; } = "UPDATE Postulate_Vacant_Step_Field_Rel SET FieldValue = @FieldValue WHERE PostulateVacantRelId = @PostulateVacantRelId AND StepFieldRelId = @StepFieldRelId";
        public string StepPostRelFindId { get; } = "SELECT Id FROM Postulate_Vacant_Rel WHERE VacantId = @VacantId AND PostulateId = @PostulateId";
        public string StepFieldRelFindId { get; } = "SELECT Id FROM Step_Field_Rel WHERE StepId = @StepId AND StepFieldId = @StepFieldId";
    }
}